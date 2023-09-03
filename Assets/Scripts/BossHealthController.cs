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

    public int CurrentHealth { get => m_currentHealth; set => m_currentHealth = value; }

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
        m_bossHealthSlider.maxValue = CurrentHealth;
        m_bossHealthSlider.value = CurrentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damageAmount, 0);

        if (CurrentHealth == 0)
        {
            m_bossBattle.EndBattle();
        }

        m_bossHealthSlider.value = CurrentHealth;
    }
}
