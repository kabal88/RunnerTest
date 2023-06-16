using System;
using UI;
using UnityEngine;

namespace Views
{
    public class GameUIView : MonoBehaviour
    {
        public void Init()
        {
        }

        public void SetActive(bool isOn)
        {
            gameObject.SetActive(isOn);
        }
    }
}