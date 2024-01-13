using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Text _resultText;
    [SerializeField] private Image _resultImg;
    [SerializeField] private Sprite[] _resultchar = new Sprite[2];

    void Start()
    {
       var _dontDestroyData = GameObject.Find("DontDestroyData").GetComponent<DontDestroyData>();

        if (_dontDestroyData.isCatWon)
        {
            _resultText.text = "Cat WIN";
            _resultImg.sprite = _resultchar[0];
        }
        else
        {
            _resultText.text = "SHRIMP WIN";
            _resultImg.sprite = _resultchar[1];
        }

        _text.gameObject.SetActive(false);

        Observable.Timer(TimeSpan.FromSeconds(3)).Subscribe(_ =>
        {
            _text.gameObject.SetActive(true);

            this.UpdateAsObservable()
            .Where(_ => Input.anyKeyDown)
            .Subscribe(_ =>
            {
                SceneManager.LoadScene("Title");
            });

        }).AddTo(this);
    }
}
