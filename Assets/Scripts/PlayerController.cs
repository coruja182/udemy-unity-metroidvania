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
    [SerializeField] private BulletController shotToFire;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float m_dashSpeed, m_dashTime;
    // theSR, afterImage
    [SerializeField] private SpriteRenderer m_playerRenderer, m_afterImageRenderer;
    [SerializeField] private float m_afterImageLifetime, m_timeBetweenAfterImages;
    [SerializeField] private Color m_afterImageColor;


    private bool facingRight = true;
    private bool isOnGround;
    private bool canDoubleJump;
    private float dashCounter;
    private float m_afterImageCounter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // whether we the player is touching the ground
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        if (Input.GetButtonDown("Fire2"))
        {
            dashCounter = m_dashTime;

            ShowAfterImage();
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
        if (Input.GetButtonDown("Jump") && (isOnGround || canDoubleJump))
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
            Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).MoveDirection = new Vector2(transform.localScale.x, 0f);
            anim.SetTrigger("shotFired");
        }

        anim.SetBool("isOnGround", isOnGround);
        anim.SetFloat("speed", Mathf.Abs(characterRigidbody.velocity.x));

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

        Destroy(image, m_afterImageLifetime);
        m_afterImageCounter = m_timeBetweenAfterImages;
    }
}
