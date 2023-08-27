using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private int m_damageAmount = 1;
    [SerializeField] private bool m_destroyOnDamage;
    [SerializeField] private GameObject m_destroyEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    void DealDamage()
    {
        PlayerHealthController.Instance.DamagePlayer(m_damageAmount);

        if (m_destroyOnDamage)
        {
            if (m_destroyEffect != null)
            {
                Instantiate(m_destroyEffect, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
