using Data;

namespace Interfaces
{
    public interface IColorableNumber
    {
        int CurrentNumber { get; }
        void SetColor(NumberColor colorData);
    }
}