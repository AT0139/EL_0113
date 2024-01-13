using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.SceneManagement;
using UniRx.Triggers;
using System;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private RectTransform _toGameRect;
    [SerializeField] private RectTransform _exitRect;

    private bool _isSelectToGame = true;
    private Vector2 _selectedSize = new Vector2();
    private Vector2 _noneSize = new Vector2();
    void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.BGM.TITLE);

        // ボタンサイズ取得
        _selectedSize = _toGameRect.sizeDelta;
        _noneSize = _exitRect.sizeDelta;

        // ボタン操作
        // 決定
        this.UpdateAsObservable()
        .Where(_ => Input.GetKey(KeyCode.Space))
        .ThrottleFirst(TimeSpan.FromMilliseconds(100))
        .Subscribe(_ =>
        {
            SoundManager.Instance.PlaySE(SoundManager.SE.ENTER);
            if (_isSelectToGame) SceneManager.LoadScene("Game");
            else
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
            }

        });

        // Ｇａｍｅシーンへ
        this.UpdateAsObservable()
        .Where(_ => Input.GetKey(KeyCode.Z))
        .ThrottleFirst(TimeSpan.FromMilliseconds(100))
        .Subscribe(_ =>
        {
            if (_isSelectToGame) return;

            SoundManager.Instance.PlaySE(SoundManager.SE.SELECT);
            _isSelectToGame = true;
            ButtonSizeChange(_selectedSize, _noneSize);

        });

        // 終了
        this.UpdateAsObservable()
        .Where(_ => Input.GetKey(KeyCode.X))
        .ThrottleFirst(TimeSpan.FromMilliseconds(100))
        .Subscribe(_ =>
        {
            if (!_isSelectToGame) return;

            SoundManager.Instance.PlaySE(SoundManager.SE.SELECT);
            _isSelectToGame = false;
            ButtonSizeChange(_noneSize,_selectedSize);

        });
    }


    private void ButtonSizeChange(Vector2 gamesize, Vector2 exitsize)
    {
        // ゲーム
        _toGameRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, gamesize.x);
        _toGameRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gamesize.y);

        // 終了
        _exitRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, exitsize.x);
        _exitRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, exitsize.y);
    }
}
