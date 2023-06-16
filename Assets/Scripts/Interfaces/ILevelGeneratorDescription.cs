using Models;

namespace Interfaces
{
    public interface ILevelGeneratorDescription : IDescription
    {
        LevelGeneratorModel Model { get; }
    }
}