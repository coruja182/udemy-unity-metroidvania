using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private Slider m_healthSlider;
    [SerializeField] private Image m_fadeScreen;
    [SerializeField] private Animator m_uiAnimator;

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
}
