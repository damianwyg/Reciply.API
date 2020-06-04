using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciply.API.Dtos
{
    public class CommentForRecipeDetailsDto
    {
        public string Message { get; set; }
        public DateTime DateAdded { get; set; }
        public int UserId { get; set; }
    }
}
