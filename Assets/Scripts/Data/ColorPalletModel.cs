using Data;

namespace Models
{
    public class ColorPalletModel
    {
        private ColorData[] _pallet { get; }

        public ColorPalletModel(ColorData[] pallet)
        {
            this._pallet = pallet;
        }
        
        public bool TryGetNumberColor(int id, out NumberColor numberColor)
        {
            foreach (var c in _pallet)
            {
                if (c.Id == id)
                {
                    numberColor = c.NumberColor;
                    return true;
                }
            }

            numberColor = default;
            return false;
        }
    }
}