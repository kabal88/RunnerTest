using System;
using DG.Tweening;
using Interfaces;
using Tweens;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public class ThrowAbility : IAbility
    {
        [SerializeField] private JumpParams _throwParams;
        
        public void Execute(IOwner owner = null, ITarget target = null)
        {
            if (target is IUnitContext context)
            {
                context.HandleState(context.JumpState);
                context.View.transform.DOLocalJump(_throwParams.EndValue, _throwParams.JumpPower, _throwParams.NumJumps,
                        _throwParams.Duration)
                    .SetEase(_throwParams.Ease)
                    .OnComplete(() => context.HandleState(context.MovingState));
            }
        }
    }
}