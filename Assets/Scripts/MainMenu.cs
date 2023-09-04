using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private string m_newGameScene;

    private void Start()
    {

    }

    public void NewGame()
    {
        SceneManager.LoadScene(m_newGameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
