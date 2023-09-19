using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RespawnController : MonoBehaviour, Singleton
{
    public static RespawnController Instance { get; set; }

    [SerializeField] private float m_respawnWaitTime;
    [SerializeField] private GameObject m_deathEffect;

    public Vector3 SpawnPoint { get; set; }

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

    // Start is called before the first frame update
    void Start()
    {
        SpawnPoint = PlayerHealthController.Instance.transform.position;
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
        PlayerHealthController.Instance.gameObject.SetActive(false);
        if (m_deathEffect != null)
        {
            Instantiate(m_deathEffect, PlayerHealthController.Instance.transform.position, PlayerHealthController.Instance.transform.rotation);
        }

        // wait for an amount of time
        yield return new WaitForSeconds(m_respawnWaitTime);

        PlayerHealthController.Instance.transform.position = SpawnPoint;
        PlayerHealthController.Instance.FillHealth();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerHealthController.Instance.gameObject.SetActive(true);
    }

    public void DestroyThyself()
    {
        Destroy(gameObject);
        Instance = null;
    }
}
