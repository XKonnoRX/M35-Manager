using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace M35_Manager
{
    public class SimSlot
    {
        public Button button { get; set; }
        public Color StateColor { get; set; }
        public InfoPage infoPage { get; set; }
        public ManagePanel managePanel { get; set; }
        public SerialPort serialPort { get; set; }
        public SlotData SlotData { get; set; }
        public Task TaskScan { get; set; }
        public bool scanKey = true;
        public bool sendMessage = false;
        public SimSlot(Button button, SlotData slotData)
        {
            this.button = button;
            this.SlotData = slotData;

        }
        public async void RestartScan()
        {
            this.scanKey = false;
            TaskScan.Wait();
            scanKey = true;
            StartScan();
        }
        public void StartScan()
        {
            IProgress<(string log, SMS sms, string state)> progress = new Progress<(string log, SMS sms, string state)>(report =>
            {
                managePanel.ConsoleWindow.AppendText(report.log + "\r\n");
                managePanel.ConsoleWindow.SelectionStart = managePanel.ConsoleWindow.Text.Length;
                managePanel.ConsoleWindow.ScrollToCaret();
                switch (report.state)
                {
                    case "working":
                        if (StateColor != Color.SkyBlue)
                            StateColor = Color.Transparent;
                        break;
                    case "waitingNumber":
                        StateColor = Color.BlanchedAlmond;
                        break;
                    case "waitingSim":
                        StateColor = Color.Honeydew;
                        break;
                    case "fuck":
                        StateColor = Color.Salmon;
                        break;
                    case "badPort":
                        StateColor = Color.Salmon;
                        break;
                    case "badControl":
                        StateColor = Color.Salmon;
                        break;
                    case "newSMS":
                        StateColor = Color.SkyBlue;
                        break;
                    default:
                        break;
                }
                if (button.BackColor != SystemColors.ActiveCaption)
                    button.BackColor = StateColor;
                if (report.sms != null)
                {
                    var source = string.IsNullOrEmpty(report.sms.ReffererName) ? report.sms.Number : report.sms.ReffererName;
                    var mess = $"{report.sms.Date} \r\n " +
                    $"from: {source}\r\n " +
                    $"{report.sms.Message}";

                    managePanel.CreateMessage(mess, false);
                    scanKey = true;
                    var users = DB.Select<UserData>(s => s.role == "admin");
                    foreach (var user in users)
                    {
                        try
                        {
                            MainForm.botClient.SendTextMessageAsync(chatId: user.tg_id,
                                text: $"<b>{report.sms.Date}</b>\n" +
                                $"<b>SIM</b>: {SlotData.CurrentNumber}\n" +
                                $"<b>Port:</b> {SlotData.HubId}-{SlotData.LocalId}\n" +
                                $"<b>From:</b> {source}\n" +
                                $"{report.sms.Message}",
                                parseMode: ParseMode.Html);
                        }
                        catch
                        {
                        }
                    }
                }
            });
            TaskScan = Task.Run(() => Scan(progress));
        }
        public async Task Scan(IProgress<(string log, SMS sms, string state)> progress)
        {

            progress.Report(("Начал скан сообщений", null, "start"));
            while (scanKey)
            {
                try
                {
                    if (serialPort == null)
                    {
                        progress.Report(("Порт не найден", null, "badPort"));
                        Thread.Sleep(10000);
                        serialPort = AtCommander.OpenPort(SlotData.SerialPort);
                        continue;
                    }
                    var status = AtCommander.GetPortStatus(serialPort);
                    if (!status.Contains("QINISTAT"))
                    {
                        progress.Report(("SIM-карта не вставлена. Ожидаю...", null, "waitingSim"));
                        SlotData.CurrentNumber = null;
                        DB.Update<SlotData>(s => s.Id == SlotData.Id, s => s.CurrentNumber = null);
                        Thread.Sleep(5000);
                        continue;
                    }
                    var number = AtCommander.GetSimNumber(serialPort);
                    if (number == null)
                    {
                        if (sendMessage)
                        {
                            progress.Report(("Жду установку номера", null, "waitingNumber"));
                            continue;
                        }
                        progress.Report(("Номер не найден", null, "waitingNumber"));
                        var index = MainForm.Slots.IndexOf(this);
                        for (int i = index - 1; i >= 0; i--)
                            if (!string.IsNullOrEmpty(MainForm.Slots[i].SlotData.CurrentNumber))
                            {
                                AtCommander.InitSettings(serialPort);
                                var res = AtCommander.SendSMS(serialPort, MainForm.Slots[i].SlotData.CurrentNumber, $"COM:{SlotData.ServerId}-{SlotData.HubId}-{SlotData.Id}");
                                progress.Report(($"Отправил смс на контрольный номер: {res}", null, "waitingNumber"));
                                if (res.Contains("ERROR"))
                                    continue;
                                sendMessage = true;
                                Thread.Sleep(1000);
                                break;
                            }
                        continue;
                    }
                    if (number != SlotData.CurrentNumber)
                    {
                        sendMessage = false;
                        progress.Report(($"Новый номер: {number}", null, "working"));
                        AtCommander.InitSettings(serialPort);
                        SlotData.CurrentNumber = number;
                        DB.Update<SlotData>(s => s.Id == SlotData.Id, s => s.CurrentNumber = number);
                    }
                    progress.Report(("Поиск сообщений...", null, "working"));
                    var smsList = AtCommander.ReadAllSms(serialPort);
                    List<Task> tasks = [];
                    foreach (var sms in smsList)
                    {
                        if (sms.Message.Contains("COM:"))
                        {
                            var split = sms.Message.Replace("COM:", "").Split("-");
                            if (split.Length < 2)
                                continue;
                            tasks.Add(Task.Run(() =>
                            {
                                var slot = MainForm.Slots.Find(s => s.SlotData.Id == int.Parse(split[2].Split("+")[0]));
                                if (string.IsNullOrEmpty(sms.Number))
                                {
                                    progress.Report(($"Номер не обнаружен. Source: {sms.Source}", null, "fuck"));
                                    return;
                                }
                                var res = AtCommander.WriteMSISDN(slot.serialPort, sms.Number);
                                progress.Report(($"Записал номер {sms.Number} в порт {slot.SlotData.Port}: {res}", null, "working"));
                            }));
                            Task.WaitAll([.. tasks]);
                        }
                        progress.Report(($"Новое сообщение: {sms.Message}", sms, "newSMS"));
                        DB.Insert(new SmsData() { SlotId = SlotData.Id, SourceNumber = sms.Number, DestNumber = SlotData.CurrentNumber, Status = sms.Status, DateTime = sms.Date, Message = sms.Message, Name = sms.ReffererName });
                    }
                    Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                    progress.Report(($"{ex.Message} {ex.StackTrace}", null, "fuck"));
                    if(serialPort!=null && serialPort.IsOpen)
                        serialPort.Close();
                    try
                    {
                        serialPort = AtCommander.OpenPort(SlotData.SerialPort);
                    }
                    catch(Exception ex2)
                    {
                        try
                        {
                            await MainForm.botClient.SendTextMessageAsync(chatId: 5720444317,
                                text: $"<b>Port:</b> {SlotData.HubId}-{SlotData.LocalId} is broken\n" +
                                $"ex1: {ex.Message} {ex.StackTrace}\n\n" +
                                $"ex2: {ex2.Message} {ex2.StackTrace}\n",
                                parseMode: ParseMode.Html);
                        }
                        catch
                        {
                        }
                        return;
                    }
                }
            }
        }
    }
}
