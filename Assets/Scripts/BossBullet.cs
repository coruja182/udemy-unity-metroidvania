using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private Rigidbody2D m_theRigidbody;
    [SerializeField] private int m_damageAmount;
    [SerializeField] private GameObject m_impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = transform.position - PlayerHealthController.Instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        AudioManager.Instance.PlaySFXAdjusted(SFX.BOSS_SHOT);
    }

    // Update is called once per frame
    void Update()
    {
        m_theRigidbody.velocity = -transform.right * m_moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.Instance.DamagePlayer(m_damageAmount);
        }

        if (m_impactEffect != null)
        {
            Instantiate(m_impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        AudioManager.Instance.PlaySFXAdjusted(SFX.BULLET_IMPACT);
    }
}
