using Models;

namespace Interfaces
{
    public interface IInputActionsDescription :IDescription
    {
        InputActionsModel Model { get; }
    }
}