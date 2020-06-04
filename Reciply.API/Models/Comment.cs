using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciply.API.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Message { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
