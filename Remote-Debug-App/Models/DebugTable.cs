using System;


namespace Remote_Debug_App.Models
{
    public class DebugTable
    {
        public int Id { get; set; }
        public string RandomGuid { get; set; }
        public DateTime DateTime { get; set; }
        public string SerializedObject { get; set; }
    }
}
