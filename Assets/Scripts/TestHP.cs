using UnityEngine;
using UniRx;

public class TestHP : MonoBehaviour
{
    public readonly int _maxHP = 100;

    public IReadOnlyReactiveProperty<int> Health => _currentHP;
    IntReactiveProperty _currentHP = new();

    private void Start()
    {
        _currentHP.Value = _maxHP;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int damage = Random.Range(1, 10);
            Damage(damage);
        }
    }

    void Damage(int value)
    {
        _currentHP.Value -= value;
    }
}
