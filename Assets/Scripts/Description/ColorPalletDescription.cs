using System;
using Data;
using Interfaces;
using Models;
using UnityEngine;

namespace Descriptions
{
    [Serializable]
    public class ColorPalletDescription : IColorPalletDescription
    {
        [SerializeField] private ColorsPalletIdentifier _id;
        [SerializeField] private PalletContainer[] _pallet;

        public int Id => _id.Id;
        public ColorPalletModel Model => new(GetColorData(_pallet));

        private ColorData[] GetColorData(PalletContainer[] pallet)
        {
            var array = new ColorData[pallet.Length];
            for (int i = 0; i < _pallet.Length; i++)
            {
                array[i] = new ColorData()
                {
                    Id = pallet[i].Id.Id,
                    NumberColor = pallet[i].NumberColor
                };
            }

            return array;
        }
    }

    [Serializable]
    public struct PalletContainer
    {
        public ColorIdentifier Id;
        public NumberColor NumberColor;
    }
}