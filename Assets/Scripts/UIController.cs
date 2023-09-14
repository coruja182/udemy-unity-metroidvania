using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour, Singleton
{
    public static UIController Instance { get; private set; }

    [SerializeField] private Slider m_healthSlider;
    [SerializeField] private Image m_fadeScreen;
    [SerializeField] private Animator m_uiAnimator;
    [SerializeField] private string m_mainMenuScene;
    [SerializeField] GameObject m_pauseScreen;

    private string m_sceneToLoad;

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
            GetComponent<Canvas>().enabled = true;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        // UpdateHealth(PlayerHealthController.Instance.CurrentHealth, PlayerHealthController.Instance.MaxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        m_healthSlider.maxValue = maxHealth;
        m_healthSlider.value = currentHealth;
    }

    public void FadeOutAndLoadScene(string sceneName)
    {
        m_sceneToLoad = sceneName;
        m_uiAnimator.SetTrigger("FadeOut");
    }

    // Event Triggered From Animation
    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(m_sceneToLoad);
    }

    public void PauseToggle()
    {
        if (!m_pauseScreen.activeSelf)
        {
            m_pauseScreen.SetActive(true);

            // time within the game won't flow anymore
            Time.timeScale = 0f;
        }
        else
        {
            m_pauseScreen.SetActive(false);

            Time.timeScale = 1f;
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        PlayerHealthController.Instance.DestroyThyself();
        RespawnController.Instance.DestroyThyself();
        MapController.Instance.DestroyThyself();

        Instance = null;
        Destroy(gameObject);

        SceneManager.LoadScene(m_mainMenuScene);

    }

    public void DestroyThyself()
    {
        Destroy(gameObject);
        Instance = null;
    }
}
