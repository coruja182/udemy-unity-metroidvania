using UnityEngine;

public enum SFX
{
    BOSS_DEATH = 0,
    BOSS_IMPACT = 1,
    BOSS_SHOT = 2,
    BULLET_IMPACT = 3,
    ENEMY_EXPLODE = 4,
    PICKUP_GEM = 5,
    PLAYER_BALL = 6,
    PLAYER_DASH = 7,
    PLAYER_DEATH = 8,
    PLAYER_DOUBLE_JUMP = 9,
    PLAYER_FROM_BALL = 10,
    PLAYER_HURT = 11,
    PLAYER_JUMP = 12,
    PLAYER_MINE = 13,
    PLAYER_SHOOT = 14,
}

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; set; }

    [SerializeField] private AudioSource m_mainMenuMusic, m_levelMusic, m_bossMusic;
    [SerializeField] private AudioSource[] m_sfx;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMainMenuMusic()
    {
        if (!m_mainMenuMusic.isPlaying)
        {
            m_levelMusic.Stop();
            m_bossMusic.Stop();
            m_mainMenuMusic.Play();
        }
    }

    public void PlayLevelMusic()
    {
        if (!m_levelMusic.isPlaying)
        {
            m_bossMusic.Stop();
            m_mainMenuMusic.Stop();
            m_levelMusic.Play();
        }
    }

    public void PlayBossMusic()
    {
        if (!m_bossMusic.isPlaying)
        {
            m_levelMusic.Stop();
            m_bossMusic.Play();
        }
    }

    public void PlaySFX(SFX sfxToPlay)
    {
        m_sfx[(int)sfxToPlay].Stop();
        m_sfx[(int)sfxToPlay].Play();
    }

    public void PlaySFXAdjusted(SFX sfxToAdjust)
    {
        m_sfx[(int)sfxToAdjust].pitch = Random.Range(.8f, 1.2f);
        PlaySFX(sfxToAdjust);
    }
}
