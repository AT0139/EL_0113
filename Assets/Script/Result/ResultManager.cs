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

    void Start()
    {
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
