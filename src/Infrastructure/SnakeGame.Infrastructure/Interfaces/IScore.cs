namespace SnakeGame.Infrastructure.Interfaces
{
    public interface IScore
    {
        string PlayerName { get; set; }
        IColor Color { get; set; }
        long Points { get; set; }
    }
}