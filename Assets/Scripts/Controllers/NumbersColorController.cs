using System;
using System.Collections.Generic;
using Interfaces;
using Models;
using Views;

namespace Controllers
{
    public class NumbersColorController : IDisposable
    {
        private List<IColorableNumber> _colorableNumbers;
        private ColorPalletModel _colorPalletModel;

        public NumbersColorController(ColorPalletModel colorPalletModel)
        {
            _colorPalletModel = colorPalletModel;
        }

        public void Init(List<IColorableNumber> colorableNumbers)
        {
            _colorableNumbers = colorableNumbers;
        }
        
        public void Init(List<IColorableNumber> colorableNumbers, int targetNumber)
        {
            _colorableNumbers = colorableNumbers;
            ColorNumbers(targetNumber);
        }

        public void ColorNumbers(int targetNumber)
        {
            foreach (var number in _colorableNumbers)
            {
                if (number.CurrentNumber <= targetNumber)
                {
                    if (_colorPalletModel.TryGetNumberColor(ColorIdentifierMap.PositiveColor, out var colorData))
                        number.SetColor(colorData);
                }
                else
                {
                    if (_colorPalletModel.TryGetNumberColor(ColorIdentifierMap.NegativeColor, out var colorData))
                        number.SetColor(colorData);
                }
            }
        }

        public void Dispose()
        {
            _colorableNumbers.Clear();
        }
    }
}