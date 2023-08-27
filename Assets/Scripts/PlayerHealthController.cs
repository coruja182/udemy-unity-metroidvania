using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance { get; private set; }

    [SerializeField] private int m_maxHealth;

    public int CurrentHealth { get; set; }

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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CurrentHealth = m_maxHealth;
        UIController.Instance.UpdateHealth(CurrentHealth, m_maxHealth);
    }

    public void DamagePlayer(int damageAmount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damageAmount, 0);
        if (CurrentHealth == 0)
        {
            gameObject.SetActive(false);
        }

        UIController.Instance.UpdateHealth(CurrentHealth, m_maxHealth);
    }
}
