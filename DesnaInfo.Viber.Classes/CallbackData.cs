using System;

namespace DesnaInfo.Viber.Classes
{

    public class CallbackData
    {
        public string Event { get; set; }
        public long Timestamp { get; set; }
        public long Message_Token { get; set; }
        public Sender Sender { get; set; }
        public Message Message { get; set; }
        public User User { get; set; }
        public string User_Id { get; set; }
    }
}
