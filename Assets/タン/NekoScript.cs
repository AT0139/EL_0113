using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoScript : MonoBehaviour
{
    [SerializeField]
    Animator m_NekoAnimator;

    [SerializeField]
    PunchScript m_PunchScript;

    private float m_Hp;

    bool m_isAttacking;

    private void Start()
    {
        m_Hp = 10f;

        m_isAttacking = false;
    }

    private void Update()
    {
        InputAction();
    }

    private void InputAction()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            if (!m_isAttacking)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-30f, 0f),ForceMode2D.Force);

                transform.rotation = Quaternion.identity;

                transform.Rotate(new Vector3(0f, 1f, 0f), 180f);
            }
        }

        if (Input.GetKey(KeyCode.X))
        {
            if (!m_isAttacking)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(30f, 0f), ForceMode2D.Force);

                transform.rotation = Quaternion.identity;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!m_isAttacking)
            {
                SoundManager.Instance.PlaySECat(SoundManager.SE.CAT_ATTACK);

                StartCoroutine(PunchAtk());
            }
        }
    }

    private IEnumerator PunchAtk()
    {
        m_isAttacking = true;
        m_NekoAnimator.SetTrigger("Punch");

        yield return new WaitForSeconds(0.5f);

        m_isAttacking = false;

        m_PunchScript.isDamaged = false;
    }
}
