using UnityEngine;

public class TestHP : MonoBehaviour
{
    public int _currentHP;

    int _maxHP = 100;

    private void Start()
    {
        _currentHP = _maxHP;
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
        _currentHP -= value;
    }
}
