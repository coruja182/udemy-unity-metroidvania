using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance { get; set; }

    // Fields
    [SerializeField] private int m_maxHealth;
    [SerializeField] private float m_invincibilityLength;
    [SerializeField] private float m_flashLength;
    [SerializeField] private SpriteRenderer[] m_playerSprites;

    public int CurrentHealth { get; set; }
    // countdown in time
    private float m_invincibilityCounter, m_flashCounter;

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

    // Start is called before the first frame update
    private void Start()
    {
        CurrentHealth = m_maxHealth;
        UpdateUI();
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_invincibilityCounter > 0)
        {
            m_invincibilityCounter -= Time.deltaTime;
            m_flashCounter -= Time.deltaTime;


            if (m_flashCounter <= 0)
            {
                // turn the sprites On and Off to trigger the damage effect
                foreach (SpriteRenderer sprite in m_playerSprites)
                {
                    sprite.enabled = !sprite.enabled;
                }
                m_flashCounter = m_flashLength;
            }

            // if we reach the end of our invincibility we should re-enable the sprites back and stop flashing
            if (m_invincibilityCounter <= 0)
            {
                foreach (SpriteRenderer sprite in m_playerSprites)
                {
                    sprite.enabled = true;
                }
                m_flashCounter = 0f;
            }
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        // check if player is invincible
        if (m_invincibilityCounter <= 0)
        {
            // player takes the damage
            CurrentHealth = Mathf.Max(CurrentHealth - damageAmount, 0);
            if (CurrentHealth == 0)
            {
                // player dies
                // gameObject.SetActive(false);
                RespawnController.Instance.Respawn();
            }
            else
            {
                // player gets invincible for a short period
                m_invincibilityCounter = m_invincibilityLength;
            }

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        UIController.Instance.UpdateHealth(CurrentHealth, m_maxHealth);
    }

    public void FillHealth()
    {
        FillHealth(m_maxHealth);
    }

    public void FillHealth(int healAmount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + healAmount, m_maxHealth);
        UpdateUI();
    }
}
