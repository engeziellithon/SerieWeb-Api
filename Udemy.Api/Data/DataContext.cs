using Microsoft.EntityFrameworkCore;
using Udemy.Api.Models;

namespace Udemy.Api.Data
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options): base(options){}

        public DbSet<Value> Values {get;set;}
        public DbSet<User> Users {get;set;}
        
    }
}