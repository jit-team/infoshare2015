using System;

namespace Models
{
    public class Message
    {
        public DateTime Created { get; set; } = DateTime.Now;

        public Guid Guid { get; set; } = Guid.NewGuid();

        public string Content { get; set; }

        public string Sendee { get; set; }
    }
}