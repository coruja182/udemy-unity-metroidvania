using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D characterRigidbody;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator m_ballModeAnimator;
    [SerializeField] private BulletController shotToFire;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float m_dashSpeed, m_dashTime;
    [SerializeField] private SpriteRenderer m_playerRenderer, m_afterImageRenderer;
    [SerializeField] private float m_afterImageLifetime, m_timeBetweenAfterImages;
    [SerializeField] private Color m_afterImageColor;
    [SerializeField] private float m_dashCooldown;
    [SerializeField] private GameObject m_standingModeGameObject, m_ballModeGameObject;
    [SerializeField] private float m_waitToSwitchBall;
    [SerializeField] private Transform m_bombPoint;
    [SerializeField] private GameObject m_bombGameObject;

    private float m_waitToSwitchToBallCounter;

    private float m_dashRechargeCounter;
    private bool facingRight = true;
    private bool isOnGround;
    private bool canDoubleJump;
    private float dashCounter;
    private float m_afterImageCounter;
    private PlayerAbilityTracker m_abilities;

    // Start is called before the first frame update
    void Start()
    {
        m_abilities = GetComponent<PlayerAbilityTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        // whether we the player is touching the ground
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        if (m_dashRechargeCounter > 0)
        {
            m_dashRechargeCounter -= Time.deltaTime;
        }
        else
        {
            if (m_abilities.DashUnlocked && Input.GetButtonDown("Fire2") && m_standingModeGameObject.activeSelf)
            {
                // the start of the dash
                dashCounter = m_dashTime;

                ShowAfterImage();
            }
        }

        if (dashCounter > 0)
        {
            // Time.deltaTime = the interval in seconds from the last frame to this current one
            dashCounter -= Time.deltaTime;
            characterRigidbody.velocity = new Vector2(m_dashSpeed * transform.localScale.x, characterRigidbody.velocity.y);
            // countdown
            m_afterImageCounter -= Time.deltaTime;

            if (m_afterImageCounter <= 0)
            {
                ShowAfterImage();
            }
            m_dashRechargeCounter = m_dashCooldown;
        }
        else
        {
            // move sideways
            // the teacher prefers GetAxisRaw instead of GetAxis for platforming 2D games
            float moveInput = Input.GetAxisRaw("Horizontal");
            characterRigidbody.velocity = new Vector2(moveInput * moveSpeed, characterRigidbody.velocity.y); ;

            // Flip the character's velocity for movement
            if (moveInput > 0 && !facingRight || moveInput < 0 && facingRight)
            {
                FlipCharacter();
            }
        }

        // Jumping and Double Jump
        if (Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump && m_abilities.DoubleJumpUnlocked)))
        {
            canDoubleJump = isOnGround;
            if (!canDoubleJump)
            {
                anim.SetTrigger("doubleJump");
            }

            characterRigidbody.velocity = new Vector2(characterRigidbody.velocity.x, jumpForce);
        }

        // shooting
        if (Input.GetButtonDown("Fire1"))
        {
            if (m_standingModeGameObject.activeSelf)
            {
                Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).MoveDirection = new Vector2(transform.localScale.x, 0f);
                anim.SetTrigger("shotFired");
            }
            // ball bomb
            else if (m_abilities.BombUnlocked && m_ballModeGameObject.activeSelf)
            {
                Instantiate(m_bombGameObject, m_bombPoint.position, m_bombPoint.rotation);
            }
        }

        // switch to ball mode
        if (!m_ballModeGameObject.activeSelf)
        {
            if (m_abilities.BallUnlocked && Input.GetAxisRaw("Vertical") < -.9f)
            {
                m_waitToSwitchToBallCounter -= Time.deltaTime;
                if (m_waitToSwitchToBallCounter <= 0)
                {
                    m_ballModeGameObject.SetActive(true);
                    m_standingModeGameObject.SetActive(false);
                }
            }
            else
            {
                m_waitToSwitchToBallCounter = m_waitToSwitchBall;
            }
        }
        else
        {
            if (Input.GetAxisRaw("Vertical") > .9f)
            {
                m_waitToSwitchToBallCounter -= Time.deltaTime;
                if (m_waitToSwitchToBallCounter <= 0)
                {
                    m_ballModeGameObject.SetActive(false);
                    m_standingModeGameObject.SetActive(true);
                }
            }
            else
            {
                m_waitToSwitchToBallCounter = m_waitToSwitchBall;
            }
        }

        if (m_standingModeGameObject.activeSelf)
        {
            anim.SetBool("isOnGround", isOnGround);
            anim.SetFloat("speed", Mathf.Abs(characterRigidbody.velocity.x));
        }
        else if (m_ballModeGameObject.activeSelf)
        {
            m_ballModeAnimator.SetFloat("speed", Mathf.Abs(characterRigidbody.velocity.x));
        }
    }

    private void FlipCharacter()
    {
        // Toggle the chracter's facing direction
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    public void ShowAfterImage()
    {
        SpriteRenderer image = Instantiate(m_afterImageRenderer, transform.position, transform.rotation);
        image.sprite = m_playerRenderer.sprite;
        image.transform.localScale = transform.localScale;
        image.color = m_afterImageColor;

        Destroy(image.gameObject, m_afterImageLifetime);
        m_afterImageCounter = m_timeBetweenAfterImages;
    }
}
