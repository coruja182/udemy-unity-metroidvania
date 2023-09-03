using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] private GameObject m_bossToActivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_bossToActivate.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
