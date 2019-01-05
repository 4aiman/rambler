using System;

namespace Rambler.Web.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
    }
}