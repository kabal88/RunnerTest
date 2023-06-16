using UnityEngine;

namespace Models
{
    public class UnitModel
    {
        public bool IsAlive { get; private set; }
        public Vector3 Position { get; private set; }
        

        public UnitModel()
        {
        }
        
        public void SetPosition(Vector3 position)
        {
            Position = position;
        }
    }
}