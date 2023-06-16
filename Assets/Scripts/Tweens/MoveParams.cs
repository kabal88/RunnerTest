using System;
using DG.Tweening;
using UnityEngine;

namespace Tweens
{
    [Serializable]
    public sealed class MoveParams : TweenParams
    {
        public Vector3 Target;
        public LoopType LoopType = LoopType.Restart;
    }
}
