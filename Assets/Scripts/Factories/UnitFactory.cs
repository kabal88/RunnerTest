using Controllers;
using Data;
using Interfaces;
using Libraries;
using Models;
using Unity.Mathematics;
using UnityEngine;

namespace Factories
{
    public class UnitFactory
    {
        private Library _library;

        public UnitFactory(Library library)
        {
            _library = library;
        }
        

        public UnitController CreateUnit(IUnitDescription description, ISpawnPoint spawnPoint)
        {
            return new UnitController();
        }
    }
}