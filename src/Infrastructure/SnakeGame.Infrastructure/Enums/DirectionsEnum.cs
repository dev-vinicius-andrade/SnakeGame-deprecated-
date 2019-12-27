using System.ComponentModel;

namespace SnakeGame.Infrastructure.Enums
{
    public enum DirectionsEnum
    {
        [Description("Left")] Left=0,
        [Description("Up")]Up=1,
        [Description("Right")]Right=2,
        [Description("Down")]Down=3,
        [Description("Angular")]Angular=4
    }
}
