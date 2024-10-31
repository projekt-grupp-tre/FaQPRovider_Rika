using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaQProvider_Rika.Data.Entities
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string QuestionTitle { get; set; } = null!;
        public string QuestionAnswer { get; set; } = null!;
        public string? QuestionUrl { get; set; }
    }
}
