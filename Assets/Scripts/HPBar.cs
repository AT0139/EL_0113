using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    [System.NonSerialized] public Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void SetValue(float value)
    {
        _slider.value = value;
    }
}
