using Microsoft.EntityFrameworkCore;
using udemy.Models;

namespace udemy.Data
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options): base(options){}

        public DbSet<Value> Values {get;set;}
        
    }
}