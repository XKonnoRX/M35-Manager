using System.IO.Ports;
using System.Text.RegularExpressions;
internal class AtCommander
{
    /// <summary>
    /// Получает IMEI подключенного устройства через SerialPort.
    /// </summary>
    /// <param name="portName">Имя порта. К примеру COM3</param>
    /// <returns>Строка, содержащая IMEI устройства.</returns>
    static public string GetImei(SerialPort serialPort)
    {
        string command = "AT+CGSN";
        return GetResponse(serialPort, command);
    }
    /// <summary>
    /// Получает статус порта устройства через SerialPort.
    /// </summary>
    /// <param name="portName">Имя порта. К примеру COM3</param>
    /// <returns>Строка, содержащая статус порта.</returns>
    /// <remarks>
    /// Использует AT-команду AT+QINISTAT, специфичную для некоторых устройств, 
    /// для получения статуса инициализации порта.
    /// </remarks>
    static public string GetPortStatus(SerialPort serialPort)
    {
        string command = "AT+QINISTAT";
        return GetResponse(serialPort, command);
    }
    /// <summary>
    /// Изменяет IMEI устройства через SerialPort.
    /// </summary>
    /// <param name="portName">Имя порта. К примеру COM3</param>
    /// <param name="imei">Новый IMEI номер для установки на устройстве.</param>
    /// <returns>Ответ устройства на команду изменения IMEI.</returns>
    /// <remarks>
    /// Внимание: Изменение IMEI может быть незаконным в некоторых юрисдикциях и может нарушать 
    /// условия использования устройства.
    /// </remarks>
    static public string ChangeImei(string imei, SerialPort serialPort)
    {
        string command = $"AT+EGMR=1,7,\"{imei}\"";
        return GetResponse(serialPort, command);
    }
    /// <summary>
    /// Удаляет SMS сообщение с заданным индексом из памяти GSM модуля.
    /// </summary>
    /// <param name="portName">Имя порта. К примеру COM3</param>
    /// <param name="smsId">Строка, указывающая индекс SMS сообщения, которое необходимо удалить. Индекс определяется при чтении SMS сообщений командой AT+CMGL.</param>
    /// <returns>Ответ устройства на команду удаления.</returns>
    /// <remarks>
    /// Метод отправляет AT-команду AT+CMGD={smsId} для удаления SMS сообщения с указанным индексом из памяти устройства.
    /// Индекс SMS сообщения уникален в пределах сохраненных в устройстве сообщений и может быть получен с помощью команды чтения SMS.
    /// Удаление сообщения помогает управлять памятью устройства, освобождая место для новых сообщений.
    /// Важно: после удаления сообщение не может быть восстановлено.
    /// </remarks>
    static public string DeleteSMS(SerialPort serialPort, string smsId)
    {
        string command = $"AT+CMGD={smsId}";
        return GetResponse(serialPort, command);
    }

    /// <summary>
    /// Принимает входящий звонок на GSM модуле.
    /// </summary>
    /// <param name="portName">Имя порта. К примеру COM3</param>
    /// <returns>Ответ устройства на команду принятия звонка.</returns>
    /// <remarks>
    /// Метод отправляет AT-команду ATA для принятия входящего звонка.
    /// Это стандартная команда для управления звонками в GSM модулях и телефонах.
    /// </remarks>
    static public string AnswerCall(SerialPort serialPort)
    {
        string command = "ATA";
        return GetResponse(serialPort, command);
    }
    /// <summary>
    /// Отклоняет входящий звонок на GSM модуле.
    /// </summary>
    /// <param name="portName">Имя порта. К примеру COM3</param>
    /// <returns>Ответ устройства на команду отклонения звонка.</returns>
    /// <remarks>
    /// Метод отправляет AT-команду ATH для отклонения входящего звонка.
    /// ATH - стандартная команда для завершения активного звонка или отклонения входящего звонка.
    /// </remarks>
    static public string DeclineCall(SerialPort serialPort)
    {
        string command = "ATH";
        return GetResponse(serialPort, command);
    }
    /// <summary>
    /// Проверяет идентификатор входящего звонка на GSM модуле.
    /// </summary>
    /// <param name="portName">Имя порта. К примеру COM3</param>
    /// <returns>Строка с информацией о входящем звонке, включая номер звонящего.</returns>
    /// <remarks>
    /// Метод отправляет AT-команду AT+CLCC, которая возвращает список текущих звонков с детальной информацией,
    /// включая статус звонка и номер звонящего.
    /// Пример ответа: +CLIP: "+79659613894",145,"",,"",0 - номер звонящего и дополнительная информация.
    /// Важно: для работы этой функции необходимо, чтобы Caller ID был активирован на устройстве.
    /// </remarks>
    static public string CheckWhoCall(SerialPort serialPort)
    {
        string command = "AT+CLCC";
        return GetResponse(serialPort, command);

    }
    /// <summary>
    /// Генерирует строку, представляющую собой IMEI (International Mobile Equipment Identity).
    /// </summary>
    /// <param name="tag">Первые цифры IMEI, определяющие Reporting Body Identifier (идентификатор органа, выдавшего код) и тип устройства.
    /// Например, "86508605", где "86" обычно указывает на код страны или организацию, а последующие цифры — на уникальный номер производителя.</param>
    /// <returns>Строка, представляющая собой полный номер IMEI.</returns>
    /// <remarks>
    /// Метод генерирует IMEI, состоящий из указанных начальных цифр (tag) и случайной последовательности чисел для формирования уникального идентификатора устройства.
    /// В соответствии с алгоритмом Luhn (алгоритм модуля 10), для вычисления контрольной цифры IMEI используется суммирование отдельных цифр числа,
    /// при этом каждая вторая цифра удваивается; если результат умножения больше 9, к сумме добавляется результат умножения минус 9.
    /// Контрольная цифра добавляется к концу IMEI для обеспечения его валидности по алгоритму Luhn.
    /// </remarks>
    public static string GenerateIMEI(string tag)
    {

        var rand = new Random();
        var code = tag + rand.Next(100000, 999999);
        int summ = 0;
        var digits = code.ToCharArray();
        for (int i = 0; i < digits.Length; i++)
        {
            var d = int.Parse(digits[i].ToString());
            if (i % 2 != 0)
                if (d * 2 > 9)
                    summ += d * 2 - 9;
                else
                    summ += d * 2;
            else
                summ += d;
        }
        if (summ % 10 == 0)
            code += 0;
        else
            code += 10 - summ % 10;
        return code;
    }
    /// <summary>
    /// Читает новые SMS сообщения, сохраненные в памяти GSM модема, подключенного через SerialPort.
    /// </summary>
    /// <param name="portName">Имя порта. К примеру COM3</param>
    /// <returns>Список строк, каждая из которых содержит данные одного SMS сообщения в формате ответа на AT-команду +CMGL.</returns>
    /// <remarks>
    /// Каждый элемент списка представляет собой строку в формате:
    /// +CMGL: index,"status","sender","","date and time",,,,"recipient",length, message, content       
    /// Например:
    /// +CMGL: 6,"REC READ","+79952407500","","2024/04/02 15:28:56+12",145,4,0,8,"+79168960442",145,7
    /// 0415044904510020044004300437
    /// Прежде чем использовать метод ReadNewMessages, необходимо убедиться, что GSM модем настроен на чтение SMS из памяти устройства.
    /// Для этого используйте метод SetReadFromMemoryM35(), который должен быть вызван до обращения к ReadNewMessages.
    /// В противном случае, метод может не найти новые сообщения, если они сохранены не в той памяти, с которой производится чтение.
    /// </remarks>
    static public List<string> ReadNewMessages(SerialPort serialPort)
    {
        string command = "AT+CMGL=\"ALL\"";
        string response = GetResponse(serialPort, command, sleep: 5000);
        List<string> messages = new List<string>();
        string pattern = @"\+CMGL:.*?(?=\+CMGL:|\Z)";
        foreach (Match match in Regex.Matches(response, pattern, RegexOptions.Singleline))
        {
            string message = match.Value.Trim().Replace("\r\n\r\nOK", "").Replace("+CMGL: ", "");
            messages.Add(message);
        }
        return messages;
    }
    /// <summary>
    /// Настраивает хаб GSM модуля Quectel M35 на чтение SMS сообщений из памяти устройства, а не с SIM-карты.
    /// </summary>
    /// <param name="portName">Имя порта. К примеру COM3</param>
    /// <returns>Ответ устройства на команду настройки.</returns>
    /// <remarks>
    /// Этот метод отправляет AT-команду AT+CPMS="ME","ME","ME", которая настраивает устройство 
    /// на использование внутренней памяти (ME) для хранения, чтения и отправки SMS сообщений.
    /// Параметры "ME" указывают, что для всех операций с SMS (хранение, чтение, отправка) должна использоваться 
    /// внутренняя память устройства. Это может быть полезно, если вы хотите сохранять SMS в устройстве, 
    /// даже когда SIM-карта заполнена или её нет в устройстве.
    /// </remarks>
    static public string SetReadFromMemoryM35(SerialPort serialPort)
    {
        string command = "AT+CPMS=\"ME\",\"ME\",\"ME\"";
        return GetResponse(serialPort, command);

    }
    static public string DeleteAllMessage(SerialPort serialPort)
    {
        string command = "AT+QMGDA=\"DEL ALL\"";
        return GetResponse(serialPort, command);
    }
    static public void InitSettings(SerialPort serialPort)
    {
        SetTextMode(serialPort);
        SetReadFromMemoryM35(serialPort);
    }
    static public string SetTextMode(SerialPort serialPort)
    {
        string command = "AT+CMGF=1"; //выставляет "text-mode"
        return GetResponse(serialPort, command);
    }
    static public string WriteMSISDN(SerialPort serialPort, string number)
    {
        ChangePhoneBookStorage(serialPort, "ON");
        string command = $"AT+CPBW=,\"{number}\",,\"My number\"";
        GetResponse(serialPort, command);
        return ChangePhoneBookStorage(serialPort, "SM");
    }
    /// <summary>
    /// Есть два основных параметра.
    /// 1. SM - SIM phonebook
    /// 2. ON - SIM own numbers (MSISDNs) list 
    /// </summary>
    /// <param name="serialPort"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    static public string ChangePhoneBookStorage(SerialPort serialPort, string type)
    {
        string command = $"AT+CPBS=\"{type}\"";
        return GetResponse(serialPort, command);
    }
    static public string SetSimMemory(SerialPort serialPort)
    {
        string command = "AT+CPMS=\"SM\",\"SM\",\"SM\" ";
        return GetResponse(serialPort, command);
    }
    static public string GetSimNumber(SerialPort serialPort)
    {
        string command = "AT+CNUM";
        var split = GetResponse(serialPort, command).Split("\"");
        if (split.Length < 4)
            return null;
        return split[3];
    }
    static public string SendSMS(SerialPort serialPort, string number, string message)
    {
        //отредачить
        string command = $"AT+CMGS=\"{number}\"";
        GetResponse(serialPort, command);
        command = message + char.ConvertFromUtf32(26);
        return GetResponse(serialPort, command);
    }
    static public string GetResponse(SerialPort serialPort, string command, int readTimeout = 1000, int sleep = 2000)
    {
        serialPort.WriteLine(command);
        serialPort.ReadTimeout = readTimeout;
        Thread.Sleep(sleep);
        string response;
        try
        {
            return response = ClearResponse(serialPort.ReadExisting(), command);
        }
        catch (TimeoutException)
        {
            return null;
        }
    }
    static public SerialPort OpenPort(string portName, int bpRate = 115200, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
    {
        SerialPort serialPort = new SerialPort(portName);
        serialPort.BaudRate = bpRate; // Установите нужную скорость передачи данных
        serialPort.Parity = parity;
        serialPort.DataBits = dataBits;
        serialPort.StopBits = stopBits;
        serialPort.Open();
        return serialPort;
    }
    static public string ClearResponse(string response, string command)
    {
        try
        {
            response = response.Split(command)[1].Replace("OK", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");
            return response;
        }
        catch
        {
            return response.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace(command, "").Replace("OK", "");
        }

    }

    /// <summary>
    /// Переда запуском убедитесь, что вы инициализировали хаб как 
    /// AtCommander.InitSettings(port);
    /// </summary>
    /// <param name="serialPort"></param>
    /// <returns></returns>
    public static List<SMS> ReadAllSms(SerialPort serialPort)
    {
        List<string> messages = AtCommander.ReadNewMessages(serialPort);
        List<SMS> list = [];
        if (messages.Count == 0)
            return list;
        foreach (string message in messages)
        {
            SMS sms = new SMS();
            sms.Source = message;
            sms.Id = int.Parse(message.Split(',')[0]);
            sms.Status = message.Split("\"")[1];
            if (message.Split("\"")[3][0] == '+' || message.Split("\"")[3][0] == '8')
            {
                sms.Number = message.Split("\"")[3];
                sms.ReffererName = message.Split("\"")[5];
            }
            else
            {
                sms.Number = message.Split("\"")[5];
                sms.ReffererName = message.Split("\"")[3];
            }
            sms.Date = DateTime.Now;
            try { sms.Message = PDUEncoderDecoder.PDUUCS2Decode(message.Split("\"")[8].Replace("OK", "").Split("+")[0]); }
            catch { sms.Message = message.Split("\"")[8]; }
            list.Add(sms);
        }
        if (messages.Count > 0)
            DeleteAllMessage(serialPort);
        return list;
    }
}

public class SMS
{
    public int Id { get; set; }
    public string? Status { get; set; }
    public string? ReffererName { get; set; }
    public string? Number { get; set; }
    public DateTime Date { get; set; }
    public string? Message { get; set; }
    public string? Source { get; set; }
}