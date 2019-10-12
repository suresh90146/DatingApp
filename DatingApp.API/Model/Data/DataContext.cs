using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Model.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Value> values{get;set;}
       
    }
}