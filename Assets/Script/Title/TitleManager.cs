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

        // 横方向のサイズ
        _toGameRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);
        // 縦方向のサイズ
        _toGameRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
        }
    }
}
