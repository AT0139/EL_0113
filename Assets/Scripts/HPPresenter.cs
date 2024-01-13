using UnityEngine;
using UniRx;

public class HPPresenter : MonoBehaviour
{
    [SerializeField] HPBar _catHPBar;
    [SerializeField] HPBar _shrimpHPBar;

    [SerializeField] HP _cat;
    [SerializeField] HP _shrimp;

    private void Start()
    {
        _cat.Health
            .Subscribe(x =>
            {
                _catHPBar.SetValue((float)x / (float)_cat._maxHP);
            }).AddTo(this);

        _shrimp.Health
          .Subscribe(x =>
          {
              _shrimpHPBar.SetValue((float)x / (float)_shrimp._maxHP);
          }).AddTo(this);


    }
}
