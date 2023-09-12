using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    [SerializeField] private AudioManager m_audioManager;

    private void Awake()
    {
        if (AudioManager.Instance == null)
        {
            AudioManager newAudioManager = Instantiate(m_audioManager);
            DontDestroyOnLoad(newAudioManager);
            AudioManager.Instance = newAudioManager;
        }
    }
}
