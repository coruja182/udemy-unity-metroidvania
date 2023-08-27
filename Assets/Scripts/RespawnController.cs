using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RespawnController : MonoBehaviour
{
    public static RespawnController Instance { get; private set; }

    [SerializeField] private float m_respawnWaitTime;

    private Vector3 m_respawnPoint;
    private GameObject m_playerRef;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_playerRef = PlayerHealthController.Instance.gameObject;
        m_respawnPoint = m_playerRef.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        // hides the player
        m_playerRef.SetActive(false);

        // wait for an amount of time
        yield return new WaitForSeconds(m_respawnWaitTime);

        // reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // place the player back to its initial position
        m_playerRef.transform.position = m_respawnPoint;
        m_playerRef.SetActive(true);
        PlayerHealthController.Instance.FillHealth();
    }
}
