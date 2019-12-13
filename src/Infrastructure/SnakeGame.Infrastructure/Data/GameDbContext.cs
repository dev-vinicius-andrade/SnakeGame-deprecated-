using Microsoft.EntityFrameworkCore;
using SnakeGame.Infrastructure.Data.Models;

namespace SnakeGame.Infrastructure.Data
{
    public class GameDbContext:DbContext
    {

        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        {
        }

        public DbSet<RoomModel> Rooms { get; set; }
    }
}
