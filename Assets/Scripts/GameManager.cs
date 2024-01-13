using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class GameManager : MonoBehaviour
{
    [SerializeField] string _result = "Result";

    [SerializeField] HP _cat;
    [SerializeField] HP _shrimp;
    DontDestroyData _dontDestroyData;

    private void Start()
    {
        _dontDestroyData = GameObject.Find("DontDestroyData").GetComponent<DontDestroyData>();

        _cat.Health.
            Subscribe(x =>
            {
                if (x <= 0)
                {
                    WinProcess(false);
                }

            }).AddTo(this);

        _shrimp.Health.
            Subscribe(x =>
            {
                if (x <= 0)
                {
                    WinProcess(true);
                }
            }).AddTo(this);
    }

    void SceneChange()
    {
        SceneManager.LoadScene(_result);
    }

    void WinProcess(bool isCatWon)
    {
        _dontDestroyData.isCatWon = isCatWon;

        SceneChange();
    }

}