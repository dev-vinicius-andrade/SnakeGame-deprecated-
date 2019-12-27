using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Infrastructure.Interfaces
{
    public interface IRoom<TChar,TFood>
    where  TChar:IChar
    where  TFood:BaseFood,IPositionObject
    
    {
       RoomModel<TChar,TFood> Model { get; }
    }
}