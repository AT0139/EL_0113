using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{
    [SerializeField]
    ParticleSystem PunchEffect;

    public bool isDamaged;

    private void Start()
    {
        isDamaged = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamaged) return;

        if (collision.CompareTag("Enemy"))
        {
            isDamaged = true;

            Vector2 Dir = collision.transform.position - transform.position;

            Dir.Normalize();

            Dir = Dir * new Vector2(3f, 10f);

            PunchEffect.Play();

            collision.transform.GetComponent<Rigidbody2D>().velocity = Dir;

            collision.gameObject.GetComponent<Animator>().SetTrigger("Damage");

            collision.GetComponent<HP>().Damage(1);

            collision.GetComponent<EnemyScript>().Damaged();

            SoundManager.Instance.PlaySE(SoundManager.SE.EVI_HIT);
        }
    }
}
