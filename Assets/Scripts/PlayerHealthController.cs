using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    private static PlayerHealthController instance;

    [SerializeField] private int m_maxHealth;

    private int m_currentHealth;

    public static PlayerHealthController Instance
    {
        get { return instance; }
        private set { instance = value; }
    }

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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CurrentHealth = m_maxHealth;
    }

    public void DamagePlayer(int damageAmount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damageAmount, 0);
        if (CurrentHealth == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
