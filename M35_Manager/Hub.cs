using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M35_Manager
{
    public class Hub
    {
        public HubData HubData { get; set; }
        public List<SimSlot> SimSlots { get; set; } = [];
        
    }
    
}
