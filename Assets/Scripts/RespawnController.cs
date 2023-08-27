using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RespawnController : MonoBehaviour
{
    public static RespawnController Instance { get; private set; }

    [SerializeField] private float m_respawnWaitTime;
    [SerializeField] private GameObject m_deathEffect;

    public Vector3 SpawnPoint { get; set; }
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
        SpawnPoint = m_playerRef.transform.position;
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
        // hides/kills the player
        m_playerRef.SetActive(false);
        if (m_deathEffect != null)
        {
            Instantiate(m_deathEffect, m_playerRef.transform.position, m_playerRef.transform.rotation);
        }

        // wait for an amount of time
        yield return new WaitForSeconds(m_respawnWaitTime);

        // reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // place the player back to its initial position
        m_playerRef.transform.position = SpawnPoint;
        m_playerRef.SetActive(true);
        PlayerHealthController.Instance.FillHealth();
    }
}
