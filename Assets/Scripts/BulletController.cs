using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D bulletRigidBody2D;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private int m_damageAmount = 1;

    public Vector2 MoveDirection
    {
        set { moveDirection = value; }
        get { return moveDirection; }
    }

    // Update is called once per frame
    void Update()
    {
        // move the bullet
        bulletRigidBody2D.velocity = moveDirection * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealthController>().DamageEnemy(m_damageAmount);
        }

        if (other.CompareTag("Boss"))
        {
            BossHealthController.Instance.TakeDamage(m_damageAmount);
        }

        if (impactEffect)
        {
            // Quaternion.identity = "no rotation"
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        AudioManager.Instance.PlaySFXAdjusted(SFX.BULLET_IMPACT);

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
