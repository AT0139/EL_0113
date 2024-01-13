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
    [SerializeField] private Image _anykeyImg;
    [SerializeField] private Image _resultTextImg;
    [SerializeField] private Image _resultImg;
    [SerializeField] private Sprite[] _resultchar = new Sprite[2];
    [SerializeField] private Sprite[] _resulttext = new Sprite[2];

    void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.BGM.RESULT,false);

        var _dontDestroyData = GameObject.Find("DontDestroyData").GetComponent<DontDestroyData>();

        if (_dontDestroyData.isCatWon)
        {
            _resultTextImg.sprite = _resulttext[0];
            _resultImg.sprite = _resultchar[0];
        }
        else
        {
            _resultTextImg.sprite = _resulttext[1];
            _resultImg.sprite = _resultchar[1];
        }

        _anykeyImg.gameObject.SetActive(false);

        Observable.Timer(TimeSpan.FromSeconds(5)).Subscribe(_ =>
        {
            _anykeyImg.gameObject.SetActive(true);

            this.UpdateAsObservable()
            .Where(_ => Input.anyKeyDown)
            .Subscribe(_ =>
            {
                SoundManager.Instance.PlaySE(SoundManager.SE.ENTER);
                SceneManager.LoadScene("Title");
            });

        }).AddTo(this);
    }
}
