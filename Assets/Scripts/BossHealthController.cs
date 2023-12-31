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
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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
        else
        {
            AudioManager.Instance.PlaySFX(SFX.BOSS_IMPACT);
        }

        m_bossHealthSlider.value = CurrentHealth;
    }
}
