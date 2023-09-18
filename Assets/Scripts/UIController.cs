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
    [SerializeField] GameObject m_fullscreenMap;

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
        if (!IsPaused())
        {
            m_pauseScreen.SetActive(true);
            // time within the game won't flow anymore
        }
        else
        {
            m_pauseScreen.SetActive(false);
        }
        UpdateTimeScale();
    }

    private bool IsPaused()
    {
        return m_pauseScreen.activeSelf;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        PlayerHealthController.Instance.DestroyThyself();
        RespawnController.Instance.DestroyThyself();
        MapController.Instance.DestroyThyself();
        FullMapCameraController.Instance.DestroyThyself();

        Instance = null;
        Destroy(gameObject);

        SceneManager.LoadScene(m_mainMenuScene);

    }

    public void DestroyThyself()
    {
        Destroy(gameObject);
        Instance = null;
    }

    public void ToggleFullscreenMap()
    {
        if (!IsPaused())
        {
            bool activate = !m_fullscreenMap.activeInHierarchy;
            m_fullscreenMap.SetActive(activate);
            MapController.Instance.FullMapCamera.SetActive(activate);
            UpdateTimeScale();
        }
    }

    private void UpdateTimeScale()
    {
        Time.timeScale = (m_fullscreenMap.activeInHierarchy || IsPaused()) ? Time.timeScale = 0f : Time.timeScale = 1f;
    }

    internal void MoveCamera(Vector2 motion)
    {
        FullMapCameraController.Instance.MoveMotion = motion;
    }

    internal void ZoomCamera(float zoom)
    {
        FullMapCameraController.Instance.ZoomMotion = zoom;
    }
}
