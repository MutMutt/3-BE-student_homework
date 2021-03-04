using Microsoft.EntityFrameworkCore;
using BE_3_Student.Models;

namespace BE_3_Student.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Students> students { get; set; }
        public DbSet<Students_description> students_description { get; set; }

    }
}