using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private bool m_isAttack;

    private bool m_isDamaged;

    [SerializeField]
    ParticleSystem PunchEffect;

    private float MoveValue;

    [SerializeField]
    private Vector2 AtkValue;

    [SerializeField]
    private Vector2 MoveSpd;


    void Start()
    {
        m_isAttack = false;

        m_isDamaged = false;
    }

    private void Update()
    {
        InputAction();   
    }

    private void InputAction()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            if(!m_isAttack)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-MoveSpd.x, 0f), ForceMode2D.Force);

                MoveValue = -1f;

                transform.rotation = Quaternion.identity;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!m_isAttack)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(MoveSpd.x, 0f), ForceMode2D.Force);

                MoveValue = 1f;

                transform.rotation = Quaternion.identity;

                transform.Rotate(new Vector3(0f, 1f, 0f), 180f);
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(!m_isAttack)
            {
                m_isAttack = true;
                GetComponent<Animator>().SetTrigger("Punch");

                SoundManager.Instance.PlaySE(SoundManager.SE.EVI_ATTACK);

                Vector2 Dir = new Vector2(AtkValue.x * MoveValue, AtkValue.y);

                GetComponent<Rigidbody2D>().velocity = Dir;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!m_isAttack) return;

        if(collision.collider.CompareTag("Player"))
        {
            if(!m_isDamaged)
            {
                m_isDamaged = true;

                Vector2 Dir = collision.transform.position - transform.position;

                Dir.Normalize();

                Dir = Dir * new Vector2(3f, 10f);

                PunchEffect.Play();

                collision.transform.GetComponent<Rigidbody2D>().velocity = Dir;

                Vector2 SelfDir = Dir * -0.75f;

                GetComponent<Rigidbody2D>().velocity = SelfDir;

                collision.gameObject.GetComponent<Animator>().SetTrigger("Damage");

                collision.transform.GetComponent<HP>().Damage(1);

                m_isAttack = false;
                GetComponent<Animator>().SetTrigger("Idle");

                m_isDamaged = false;

                SoundManager.Instance.PlaySECat(SoundManager.SE.CAT_HIT);
            }
        }

        if(collision.collider.CompareTag("Platform"))
        {
            m_isAttack = false;
            GetComponent<Animator>().SetTrigger("Idle");

            m_isDamaged = false;
        }
    }

    public void Damaged()
    {
        m_isAttack = false;

        m_isDamaged = false;
    }
}
