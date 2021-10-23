using System;

namespace BluePaw.Administration.Models
{
    [Serializable]
    public class Message
    {
        public string Value { get; }

        public Message(string value)
        {
            Value = value;
        }
    }
}
