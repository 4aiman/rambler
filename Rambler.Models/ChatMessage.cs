using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rambler.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string SourceMessageId { get; set; }
        public string SourceAuthorId { get; set; }

        [NotMapped] public string DisplayDate => DateTime.Now.DayOfYear != Date.ToLocalTime().DayOfYear ? Date.ToLocalTime().ToString("d") : "";
        [NotMapped] public string DisplayTime => Date.ToLocalTime().ToString("t");
    }
}