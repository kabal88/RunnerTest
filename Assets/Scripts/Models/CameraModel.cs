using UnityEngine;

namespace Controllers
{
    public class CameraModel
    {
        public float Speed { get; private set; }
        public bool CanMove{ get; private set; }
        
        public Vector3 StartPosition { get; private set; }

        public CameraModel(float speed)
        {
            Speed = speed;
        }

        public void SetCanMove(bool value)
        {
            CanMove = value;
        }

        public void SetStartPosition(Vector3 value)
        {
            StartPosition = value;
        }
    }
}