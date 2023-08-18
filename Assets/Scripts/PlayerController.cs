using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D characterRigidbody;

    [FormerlySerializedAs("moveSpeed")]
    private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Animator anim;
    [SerializeField] private BulletController shotToFire;
    [SerializeField] private Transform shotPoint;
    private bool facingRight = true;

    private bool isOnGround;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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

        // whether we the player is touching the ground
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        // jumping
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            characterRigidbody.velocity = new Vector2(characterRigidbody.velocity.x, jumpForce);
        }

        // shooting
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).MoveDirection = new Vector2(transform.localScale.x, 0f);
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
}
