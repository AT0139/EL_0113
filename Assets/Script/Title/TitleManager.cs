using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private RectTransform _toGameRect;
    [SerializeField] private RectTransform _exitRect;

    void Start()
    {
        
    }

    private void ButtonSizeChange()
    {
        var size = _toGameRect.sizeDelta;

        // �������̃T�C�Y
        _toGameRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);
        // �c�����̃T�C�Y
        _toGameRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
        }
    }
}
