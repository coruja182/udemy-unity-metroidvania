using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; }

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
}
