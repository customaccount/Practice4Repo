using System;
using System.Collections.Generic;

#nullable disable

namespace Remote_Debug_App.Models
{
    public partial class DebugTable
    {
        public int Id { get; set; }
        public string RandomGuid { get; set; }
        public DateTime DateTime { get; set; }
        public string SerializedObject { get; set; }
    }
}
