using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Pending bugs to solve:
 * [ ] - When the boss is shooting the other timers should be paused, right now, on Phase 2 the boss is fading without executing the vanish animation
 * [ ] - When the phanthon is vanishing, it should first disable its collider 
 * Other clarifications:
 * - I didn't quite understand the FadeTimer logic, maybe it gets better If I trigger events before and after the fade animation
 */
public class BossBattle : MonoBehaviour
{
    [SerializeField] private Transform m_cameraPosition;
    [SerializeField] private float m_cameraSpeed, m_activeTime, m_fadeOutTime, m_inactiveTime, m_moveSpeed, m_timeBetweenShots_1, m_timeBetweenShots_2;
    [SerializeField] private int m_threshold_1, m_threshold_2;
    [SerializeField] private Transform[] m_spawnPoints;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform m_bossObject;
    [SerializeField] private GameObject m_bullet;

    private CameraController m_cameraController;
    private float m_activeCounter, m_fadeCounter, m_inactiveCounter, m_shotCounter;
    private Transform m_targetPoint;
    private bool m_isFacingLeft = true, m_isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        m_cameraController = FindObjectOfType<CameraController>();
        m_cameraController.enabled = false;
        m_activeCounter = m_activeTime;
        m_bossObject.position = m_spawnPoints[0].position;
        m_shotCounter = m_timeBetweenShots_1;
    }

    // Update is called once per frame
    void Update()
    {
        m_cameraController.transform.position = Vector3.MoveTowards(m_cameraController.transform.position, m_cameraPosition.position, m_cameraSpeed * Time.deltaTime);

        FlipSpriteIfNeeded();

        // Boss Phase 1
        if (IsPhase1())
        {
            if (IsActiveTime())
            {
                ShootOnTimer();
                VanishOnTimer();
            }
            else if (IsFadeTime())
            {
                FadeTimer();
            }
            else if (IsInactiveTime())
            {
                ReappearOnTimer();
            }
        }
        else
        {
            // Boss Phase 2
            if (m_targetPoint == null)
            {
                // Entering Phase 2
                m_targetPoint = m_bossObject;
                m_fadeCounter = m_fadeOutTime;
                animator.SetTrigger("vanish");
            }
            else
            {
                if (Vector3.Distance(m_bossObject.position, m_targetPoint.position) > .02f)
                {
                    ShootOnTimer();

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
                    FadeTimer();
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

    private void FadeTimer()
    {
        m_fadeCounter -= Time.deltaTime;
        if (m_fadeCounter <= 0f)
        {
            m_bossObject.gameObject.SetActive(false);
            m_inactiveCounter = m_inactiveTime;
        }
    }

    private void ReappearOnTimer()
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

    private bool IsInactiveTime()
    {
        return m_inactiveCounter > 0;
    }

    private bool IsFadeTime()
    {
        return m_fadeCounter > 0;
    }

    private bool IsActiveTime()
    {
        return m_activeCounter > 0f;
    }

    private void VanishOnTimer()
    {
        m_activeCounter -= Time.deltaTime;
        if (m_activeCounter <= 0f)
        {
            m_fadeCounter = m_fadeOutTime;
            animator.SetTrigger("vanish");
        }
    }

    private void ShootOnTimer()
    {
        m_shotCounter -= Time.deltaTime;
        if (m_shotCounter <= 0f)
        {
            m_shotCounter = IsPhase1() ? m_timeBetweenShots_1 : m_timeBetweenShots_2;
            Shoot();
        }
    }

    private void Shoot()
    {
        m_isShooting = true;
        animator.SetTrigger("shoot");
    }

    private bool IsPhase1()
    {
        return BossHealthController.Instance.CurrentHealth > m_threshold_1;
    }

    public void EndBattle()
    {
        gameObject.SetActive(false);
    }

    public void OnShoot()
    {
        Instantiate(m_bullet, m_bossObject.transform.position, Quaternion.identity);
    }

    public void OnShootEnd()
    {
        m_isShooting = false;
    }

    private bool IsPlayerOnTheRight()
    {
        return PlayerHealthController.Instance.transform.position.x > m_bossObject.transform.position.x;
    }

    private void FlipSprite()
    {
        m_isFacingLeft = !m_isFacingLeft;
        Vector3 theScale = m_bossObject.transform.localScale;
        theScale.x *= -1;
        m_bossObject.transform.localScale = theScale;
    }

    private void FlipSpriteIfNeeded()
    {
        if (m_isFacingLeft && IsPlayerOnTheRight() || !m_isFacingLeft && !IsPlayerOnTheRight())
        {
            FlipSprite();
        }
    }
}
