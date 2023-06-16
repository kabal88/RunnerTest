using Models;

namespace Interfaces
{
    public interface IColorPalletDescription : IDescription
    {
        ColorPalletModel Model { get; }
    }
}