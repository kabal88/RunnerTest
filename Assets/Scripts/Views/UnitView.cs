using UnityEngine;

namespace Views
{
    public class UnitView : MonoBehaviour
    {
        public UnitHolderMonoComponent Holder { get; private set; }
    
 
        public Vector3 Position => transform.position;

        public void Init()
        {
            Holder = GetComponentInParent<UnitHolderMonoComponent>();
        }
    }
}