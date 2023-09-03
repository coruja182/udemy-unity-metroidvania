using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController Instance { get; private set; }

    [SerializeField] Slider m_bossHealthSlider;
    [SerializeField] private int m_currentHealth = 30;
    [SerializeField] private BossBattle m_bossBattle;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        m_bossHealthSlider.maxValue = m_currentHealth;
        m_bossHealthSlider.value = m_currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        m_currentHealth = Mathf.Max(m_currentHealth - damageAmount, 0);

        if (m_currentHealth == 0)
        {
            m_bossBattle.EndBattle();
        }

        m_bossHealthSlider.value = m_currentHealth;
    }
}
