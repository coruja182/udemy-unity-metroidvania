using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private string m_newGameScene;
    [SerializeField] private GameObject m_continueButton;
    [SerializeField] private PlayerAbilityTracker m_playerReference;


    private void Start()
    {
        if (SaveManager.HasSave)
        {
            m_continueButton.SetActive(true);
        }

        AudioManager.Instance.PlayMainMenuMusic();
    }

    public void NewGame()
    {
        SaveManager.Clear();
        SceneManager.LoadScene(m_newGameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Continue()
    {
        LoadPlayerAbilitiesFromSave();
        SaveManager.LoadSave(m_playerReference.gameObject);
    }

    private void LoadPlayerAbilitiesFromSave()
    {
        foreach (AbilityType ability in Enum.GetValues(typeof(AbilityType)))
        {
            m_playerReference.SetAbility(ability, SaveManager.HasAbility(ability));
        }
    }
}
