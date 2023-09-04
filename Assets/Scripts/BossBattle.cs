using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private Transform m_cameraPosition;
    [SerializeField] private float m_cameraSpeed, m_activeTime, m_fadeOutTime, m_inactiveTime, m_moveSpeed;
    [SerializeField] private int m_threshold_1, m_threshold_2;
    [SerializeField] private Transform[] m_spawnPoints;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform m_bossObject;

    private CameraController m_cameraController;
    private float m_activeCounter, m_fadeCounter, m_inactiveCounter;

    private Transform m_targetPoint;

    // Start is called before the first frame update
    void Start()
    {
        m_cameraController = FindObjectOfType<CameraController>();
        m_cameraController.enabled = false;
        m_activeCounter = m_activeTime;
        m_bossObject.position = m_spawnPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        m_cameraController.transform.position = Vector3.MoveTowards(m_cameraController.transform.position, m_cameraPosition.position, m_cameraSpeed * Time.deltaTime);

        // Boss Phase 1
        if (BossHealthController.Instance.CurrentHealth > m_threshold_1)
        {
            if (m_activeCounter > 0f)
            {
                m_activeCounter -= Time.deltaTime;
                if (m_activeCounter <= 0f)
                {
                    m_fadeCounter = m_fadeOutTime;
                    animator.SetTrigger("vanish");
                }
            }
            else if (m_fadeCounter > 0)
            {
                m_fadeCounter -= Time.deltaTime;
                if (m_fadeCounter <= 0f)
                {
                    m_bossObject.gameObject.SetActive(false);
                    m_inactiveCounter = m_inactiveTime;
                }
            }
            else if (m_inactiveCounter > 0)
            {
                m_inactiveCounter -= Time.deltaTime;
                if (m_inactiveCounter <= 0f)
                {
                    // boss reappears
                    m_bossObject.position = m_spawnPoints[Random.Range(0, m_spawnPoints.Length)].position;

                    // activates the boss
                    m_bossObject.gameObject.SetActive(true);

                    // resets the active acounter
                    m_activeCounter = m_activeTime;
                }
            }
        }
        else
        {
            // Boss Phase 2

            if (m_targetPoint == null)
            {
                m_targetPoint = m_bossObject;
                m_fadeCounter = m_fadeOutTime;
                animator.SetTrigger("vanish");
            }
            else
            {
                if (Vector3.Distance(m_bossObject.position, m_targetPoint.position) > .02f)
                {
                    // move the boss towards the target position
                    m_bossObject.position = Vector3.MoveTowards(m_bossObject.position, m_targetPoint.position, m_moveSpeed * Time.deltaTime);

                    if (Vector3.Distance(m_bossObject.position, m_targetPoint.position) <= .02f)
                    {
                        // boss fades when within range
                        m_fadeCounter = m_fadeOutTime;
                        animator.SetTrigger("vanish");
                    }
                }
                else if (m_fadeCounter > 0)
                {
                    m_fadeCounter -= Time.deltaTime;
                    if (m_fadeCounter <= 0f)
                    {
                        m_bossObject.gameObject.SetActive(false);
                        m_inactiveCounter = m_inactiveTime;
                    }
                }
                else if (m_inactiveCounter > 0)
                {
                    m_inactiveCounter -= Time.deltaTime;
                    if (m_inactiveCounter <= 0f)
                    {
                        // boss reappears
                        m_bossObject.position = m_spawnPoints[Random.Range(0, m_spawnPoints.Length)].position;

                        m_targetPoint = m_spawnPoints[Random.Range(0, m_spawnPoints.Length)];

                        int whileBreaker = 0;
                        while (m_targetPoint.position == m_bossObject.position && whileBreaker < 100)
                        {
                            // repick a new target point in case the target point is the same 
                            m_targetPoint = m_spawnPoints[Random.Range(0, m_spawnPoints.Length)];
                            whileBreaker++;
                        }

                        // activates the boss
                        m_bossObject.gameObject.SetActive(true);

                        // resets the active acounter
                        m_activeCounter = m_activeTime;
                    }
                }
            }
        }
    }

    public void EndBattle()
    {
        gameObject.SetActive(false);
    }
}
