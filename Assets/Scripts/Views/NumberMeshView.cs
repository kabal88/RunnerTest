using Data;
using TMPro;
using UnityEngine;

public class NumberMeshView : MonoBehaviour
{
    [SerializeField] private TextMeshPro[] _textMeshes;
    
    
    public void SetNumber(int number)
    {
        for (int i = 0; i < _textMeshes.Length; i++)
        {
            _textMeshes[i].text = number.ToString();
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
