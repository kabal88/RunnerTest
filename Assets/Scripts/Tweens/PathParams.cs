using DG.Tweening;
using System;
using UnityEngine;


namespace Tweens
{
    [Serializable]
    public class PathParams : TweenParams
    {
        public PathType PathType;
        public Vector3[] PathPoints;
    }

}
