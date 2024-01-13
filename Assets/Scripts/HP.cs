using UnityEngine;
using UniRx;

public class HP : MonoBehaviour
{
    public readonly int _maxHP = 10;

    public IReadOnlyReactiveProperty<int> Health => _currentHP;
    IntReactiveProperty _currentHP = new(10);

    private void Start()
    {
        _currentHP.Value = _maxHP;
    }

    public void Damage(int value)
    {
        _currentHP.Value -= value;
    }
}
