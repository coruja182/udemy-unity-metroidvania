using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    [SerializeField] private int m_healAmount;
    [SerializeField] private GameObject m_pickupEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.Instance.FillHealth(m_healAmount);

            if (m_pickupEffect != null)
            {
                Instantiate(m_pickupEffect, transform.position, Quaternion.identity);
            }

            // DisplayText();
            Destroy(gameObject);
        }
    }
}
