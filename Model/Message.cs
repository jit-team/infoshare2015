using System;

namespace Model
{
    public class Message
    {
        public DateTime ReceivedOn { get; set; }

        public string Content { get; set; }

        public string Sendee { get; set; }
    }
}