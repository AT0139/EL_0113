using UnityEngine;
using UnityEngine.UI;

public class TestResult : MonoBehaviour
{
    DontDestroyData _dontDestroyData;
    Text _text;

    void Start()
    {
        _dontDestroyData = GameObject.Find("DontDestroyData").GetComponent<DontDestroyData>();
        _text = GetComponent<Text>();

        if (_dontDestroyData.isCatWon)
        {
            _text.text = "Cat WIN";
        }
        else
        {
            _text.text = "SHRIMP WIN";

        }
    }



}
