using System;
using System.Collections.Generic;
using System.Text;

namespace UtMobileApp.Models
{
    public class Event
    {
        public string EventName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public System.Drawing.Color Color { get; set; }
    }
}
