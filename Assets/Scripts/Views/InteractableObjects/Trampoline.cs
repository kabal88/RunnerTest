using Interfaces;
using UnityEngine;
using Views;

public class Trampoline : SimpleInteractableObject, IOwner
{
    [SerializeField] private Collider _collider;
    public int CurrentNumber { get; }
    public void Die()
    {
        _collider.enabled = false;
    }
}
