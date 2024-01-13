using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class GameManager : MonoBehaviour
{
    [SerializeField] string _result = "Result";

    [SerializeField] TestHP _cat;
    [SerializeField] TestHP _shrimp;
    [SerializeField] DontDestroyData _dontDestroyData;

    private void Start()
    {
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