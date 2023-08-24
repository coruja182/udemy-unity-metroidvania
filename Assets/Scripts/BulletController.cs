using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D bulletRigidBody2D;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private GameObject impactEffect;

    public Vector2 MoveDirection
    {
        set { moveDirection = value; }
        get { return moveDirection; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // move the bullet
        bulletRigidBody2D.velocity = moveDirection * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet using layer " + LayerMask.LayerToName(gameObject.layer) + " collided with " + other.gameObject.name + " using layer " + LayerMask.LayerToName(other.gameObject.layer));
        if (impactEffect != null)
        {
            // Quaternion.identity = "no rotation"
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
