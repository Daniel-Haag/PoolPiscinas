using Microsoft.EntityFrameworkCore;

namespace PoolPiscinas.Models
{
    public class PoolPiscinasDbContext : DbContext
    {
        public PoolPiscinasDbContext(DbContextOptions<PoolPiscinasDbContext> options) : base(options)
        {
        }

        
    }
}
