using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager
{
    private const string KEY_CONTINUE_LEVEL = "ContinueLevel";
    private const string KEY_POS_X = "PosX";
    private const string KEY_POS_Y = "PosY";
    private const string KEY_POS_Z = "PosZ";

    public static bool HasSave
    {
        get { return PlayerPrefs.HasKey(KEY_CONTINUE_LEVEL); }
    }

    public static string SceneToLoad
    {
        get
        {
            return PlayerPrefs.GetString(KEY_CONTINUE_LEVEL);
        }

        private set
        {
            PlayerPrefs.SetString(KEY_CONTINUE_LEVEL, value);
        }
    }

    public static Vector3 PlayerPosition
    {
        get
        {
            return new Vector3(PlayerPrefs.GetFloat(KEY_POS_X), PlayerPrefs.GetFloat(KEY_POS_Y), PlayerPrefs.GetFloat(KEY_POS_Z));
        }

        private set
        {
            PlayerPrefs.SetFloat(KEY_POS_X, value.x);
            PlayerPrefs.SetFloat(KEY_POS_Y, value.y);
            PlayerPrefs.SetFloat(KEY_POS_Z, value.z);
        }
    }

    public static void Save(string sceneName, Vector3 position)
    {
        Debug.Log("Saving  scene: " + sceneName + " position " + position);
        SceneToLoad = sceneName;
        PlayerPosition = position;
    }

    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void LoadSave(GameObject player)
    {
        player.SetActive(true);
        player.transform.position = PlayerPosition;
        SceneManager.LoadScene(SceneToLoad);
    }
}