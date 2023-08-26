using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int m_totalHealth;
    [SerializeField] private GameObject m_deathEffect;

    public void DamageEnemy(int damageAmount)
    {
        m_totalHealth = Mathf.Max(m_totalHealth - damageAmount, 0);
        if (m_totalHealth == 0)
        {
            if (m_deathEffect != null)
            {
                Instantiate(m_deathEffect, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
