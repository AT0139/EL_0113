using UnityEngine.SceneManagement;
using UnityEngine;

public class DontDestroyData : MonoBehaviour
{
    static bool _created = false;
    public bool isCatWon;
    void Start()
    {
        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            _created = true;
        }
    }
}
