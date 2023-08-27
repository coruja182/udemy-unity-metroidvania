using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator m_doorAnimator;
    [SerializeField] private float m_distanceToOpen;
    [SerializeField] private Transform m_exitPoint;
    [SerializeField] private float m_movePlayerSpeed;
    [SerializeField] private string m_levelToLoad;

    private PlayerController m_playerReference;
    private bool m_playerExiting;

    // Start is called before the first frame update
    void Start()
    {
        m_playerReference = PlayerHealthController.Instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, m_playerReference.transform.position) <= m_distanceToOpen)
        {
            m_doorAnimator.SetBool("doorOpen", true);
        }
        else
        {
            m_doorAnimator.SetBool("doorOpen", false);
        }

        if (m_playerExiting)
        {
            m_playerReference.transform.position = Vector3.MoveTowards(m_playerReference.transform.position, m_exitPoint.transform.position, m_movePlayerSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!m_playerExiting)
            {
                m_playerReference.CanMove = false;
                StartCoroutine(UseDoorCoroutine());
            }
        }
    }

    IEnumerator UseDoorCoroutine()
    {
        m_playerExiting = true;

        // freezes player animation
        m_playerReference.getAnimator().enabled = false;

        yield return new WaitForSeconds(1.5f);

        RespawnController.Instance.SpawnPoint = m_exitPoint.position;
        m_playerReference.CanMove = true;
        m_playerReference.getAnimator().enabled = true;

        SceneManager.LoadScene(m_levelToLoad);
    }
}
