using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    [SerializeField] private Transform m_patrolPointsContainer;
    [SerializeField] private Transform[] m_patrolPoints;
    [SerializeField] private float m_moveSpeed, m_waitTime, m_jumpForce;
    [SerializeField] private Rigidbody2D m_enemyRigidbody;
    [SerializeField] private Transform m_groundCheckPoint;
    [SerializeField] private Transform m_frontCheckPoint;
    [SerializeField] private LayerMask m_whatIsGround;
    [SerializeField] private Animator m_enemyAnimator;
    private int m_currentPatrolPoint;
    private float m_waitCounter;
    private bool m_isFacingLeft = true; // the sprite selected is initially facing left

    private Transform CurrentPatrolPoint
    {
        get => m_patrolPoints[m_currentPatrolPoint];
    }

    private bool IsOnGround
    {
        get => Physics2D.OverlapCircle(m_groundCheckPoint.position, .2f, m_whatIsGround);
    }

    private bool IsHittingWall
    {
        get => Physics2D.OverlapCircle(m_frontCheckPoint.position, .2f, m_whatIsGround);
    }

    // Start is called before the first frame update
    void Start()
    {
        // initializes the waitCounter 
        m_waitCounter = m_waitTime;
        m_patrolPointsContainer.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - CurrentPatrolPoint.position.x) > .2f)
        {
            MoveTowardsToControlPoint();
        }
        else
        {
            WaitAndTargetNextControlPoint();
        }
        m_enemyAnimator.SetFloat("speed", MathF.Abs(m_enemyRigidbody.velocity.x));
    }

    private void WaitAndTargetNextControlPoint()
    {
        // Wait at point (decrease wait conter until reaches 0)
        m_enemyRigidbody.velocity = new Vector2(0f, m_enemyRigidbody.velocity.y);
        m_waitCounter -= Time.deltaTime;

        // Waiting timer is over, go to the next patrol point
        if (m_waitCounter <= 0f)
        {
            m_waitCounter = m_waitTime;

            // target next control point
            m_currentPatrolPoint = m_currentPatrolPoint >= m_patrolPoints.Length - 1 ? 0 : m_currentPatrolPoint + 1;
        }
    }

    private void MoveTowardsToControlPoint()
    {
        // Jump if Needed
        if (IsHittingWall && transform.position.y < CurrentPatrolPoint.position.y - .5f)
        {
            Jump();
        }

        if (transform.position.x < CurrentPatrolPoint.position.x)
        {
            // enemy is left of the Patrol Point
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    private void Jump()
    {
        if (IsOnGround)
        {
            m_enemyRigidbody.velocity = new Vector2(m_enemyRigidbody.velocity.x, m_jumpForce);
        }
    }

    private void MoveRight()
    {
        if (m_isFacingLeft)
        {
            FlipCharacter();
        }
        m_enemyRigidbody.velocity = new Vector2(m_moveSpeed, m_enemyRigidbody.velocity.y);
    }

    private void MoveLeft()
    {
        if (!m_isFacingLeft)
        {
            FlipCharacter();
        }
        m_enemyRigidbody.velocity = new Vector2(-m_moveSpeed, m_enemyRigidbody.velocity.y);
    }

    private void FlipCharacter()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
        m_isFacingLeft = !m_isFacingLeft;
    }
}
