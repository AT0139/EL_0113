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

    [SerializeField] private Transform _transitionParent;
    [SerializeField] private GameObject _transitionObj;
    [SerializeField] private int _transitionNum;
    [SerializeField] private float _transitionSpeed;

    private bool _isSelectToGame = true;
    private Vector2 _selectedSize = new Vector2();
    private Vector2 _noneSize = new Vector2();

    void Start()
    {
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
            if (_isSelectToGame)
            {
                TitleTransition();
            }
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

            _isSelectToGame = false;
            ButtonSizeChange(_noneSize, _selectedSize);

        });
    }

    // ボタンのサイズ変更
    private void ButtonSizeChange(Vector2 gamesize, Vector2 exitsize)
    {
        // ゲーム
        _toGameRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, gamesize.x);
        _toGameRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gamesize.y);

        // 終了
        _exitRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, exitsize.x);
        _exitRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, exitsize.y);
    }

    // トランジション
    private void TitleTransition()
    {
        for (int i = 0; i < _transitionNum; i++)
        {
            var obj = Instantiate(_transitionObj, _transitionParent);
            obj.transform.position = new Vector3(UnityEngine.Random.Range(-10, 10), obj.transform.position.y, 0.0f);

            var scale = UnityEngine.Random.Range(1, 5);
            obj.transform.localScale = new Vector3(scale, scale, scale);
        }

        Observable.Timer(TimeSpan.FromSeconds(0.8f)).Subscribe(_ =>
        {
            SceneManager.LoadScene("Game");
        }).AddTo(this);

        Observable.EveryUpdate().Subscribe(_ =>
        {
             _transitionParent.position += new Vector3(0.0f, _transitionSpeed, 0.0f);
        }).AddTo(this);
    }
}
