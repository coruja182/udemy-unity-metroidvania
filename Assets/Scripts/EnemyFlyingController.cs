using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingController : MonoBehaviour
{

    [SerializeField] private float m_rangeToStartChase, m_moveSpeed, m_turnSpeed;
    [SerializeField] private Animator m_enemyAnimator;
    private bool m_isChasing;
    private Transform m_playerRef;

    // Start is called before the first frame update
    void Start()
    {
        m_playerRef = PlayerHealthController.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthController.Instance.gameObject.activeSelf)
        {
            if (!m_isChasing)
            {
                // should the enemy chase?
                if (Vector3.Distance(transform.position, m_playerRef.position) < m_rangeToStartChase)
                {
                    m_isChasing = true;
                    m_enemyAnimator.SetBool("isChasing", m_isChasing);
                }
            }
            else
            {
                LookTorwardsPlayer();
                MoveTorwardsPlayer();
            }
        }
        
    }

    private void MoveTorwardsPlayer()
    {
        transform.position += -transform.right * m_moveSpeed * Time.deltaTime;
    }

    private void LookTorwardsPlayer()
    {
        // chase player
        Vector3 directionToPlayer = transform.position - m_playerRef.position;
        Debug.Log("Direction to player: " + directionToPlayer);
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angleToPlayer, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_turnSpeed * Time.deltaTime);
    }
}
