using Microsoft.EntityFrameworkCore;
using ToutiaoNews.Entity;

namespace ToutiaoNews.Data
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }

        private string _connection;
        public NewsDbContext(string connection)
        {
            _connection = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!string.IsNullOrEmpty(_connection))
                builder.UseSqlServer(_connection);
        }

        public DbSet<News> News { get; set; }
    }
}