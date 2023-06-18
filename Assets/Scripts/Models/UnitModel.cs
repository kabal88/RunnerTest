using UnityEngine;

namespace Models
{
    public class UnitModel
    {
        public bool IsAlive { get; private set; }
        public bool IsActive { get; private set; }
        public int CurrentNumber { get; private set; }
        public Vector3 Position { get; private set; }
        

        public UnitModel(int currentNumber)
        {
            CurrentNumber = currentNumber;
        }
        
        public void SetPosition(Vector3 position)
        {
            Position = position;
        }

        public void SetCurrentNumber(int value)
        {
            CurrentNumber = value;
        }

        public void AddToCurrentNumber(int value)
        {
            CurrentNumber += value;
        }

        public void SetIsActive(bool isOn)
        {
            IsActive = isOn;
        }
    }
}