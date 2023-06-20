using Data;
using Interfaces;
using TMPro;
using UnityEngine;

public class NumberMeshView : MonoBehaviour
{
    [SerializeField] private TextMeshPro[] _textMeshes;
    
    
    public void SetNumber(int number)
    {
        var numberString = number.ToString();
        
        for (var i = 0; i < _textMeshes.Length; i++)
        {
            _textMeshes[i].text = numberString;
        }
    }

    public void SetColor(NumberColor colors)
    {
        for (int i = 0; i < _textMeshes.Length; i++)
        {
            if (i==0)
            {
                _textMeshes[i].color = colors.MainColor;
                continue;
            }
            
            _textMeshes[i].color = colors.ShadowColor;
        }
    }
}
