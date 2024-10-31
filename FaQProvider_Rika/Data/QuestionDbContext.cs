using FaQProvider_Rika.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaQProvider_Rika.Data
{
    public class QuestionDbContext : DbContext
    {
        public QuestionDbContext(DbContextOptions<QuestionDbContext> options) : base(options) { }
        public DbSet<Question> Questions { get; set; }
    }
}
