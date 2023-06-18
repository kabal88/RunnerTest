using UnityEngine;

namespace Models
{
    public class UnitModel
    {
        public bool IsAlive { get; private set; }
        public bool IsActive { get; private set; }
        public int CurrentNumber { get; private set; }
        public Vector3 Position { get; private set; }
        public float LeftBorder { get; private set; }
        public float RightBorder { get; private set; }
        public float Speed { get; private set; }
        

        public UnitModel(int currentNumber, float leftBorder, float rightBorder, float speed, bool isAlive = true, bool isActive = true)
        {
            CurrentNumber = currentNumber;
            LeftBorder = leftBorder;
            RightBorder = rightBorder;
            Speed = speed;
            IsAlive = isAlive;
            IsActive = isActive;
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