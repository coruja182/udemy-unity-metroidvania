using UnityEngine;

public class BombController : MonoBehaviour
{

    [SerializeField] private float m_timeToExplode = .5f;
    [SerializeField] private GameObject m_explosionGameObject;
    [SerializeField] private float m_blastRange;
    [SerializeField] private LayerMask m_whatIsDestructible;
    [SerializeField] private LayerMask m_whatIsDamageable;
    [SerializeField] private int m_damageAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_timeToExplode -= Time.deltaTime;
        if (m_timeToExplode <= 0f)
        {
            if (m_explosionGameObject != null)
            {
                Instantiate(m_explosionGameObject, transform.position, transform.rotation);
            }
            Destroy(gameObject);

            Collider2D[] objectsToRemove = Physics2D.OverlapCircleAll(transform.position, m_blastRange, m_whatIsDestructible);
            if (objectsToRemove.Length > 0)
            {
                foreach (Collider2D objectToDamage in objectsToRemove)
                {
                    Destroy(objectToDamage.gameObject);
                }
            }

            Collider2D[] objetctsToDamage = Physics2D.OverlapCircleAll(transform.position, m_blastRange, m_whatIsDamageable);
            if (objetctsToDamage.Length > 0)
            {
                foreach (Collider2D objectsToDamage in objetctsToDamage)
                {
                    EnemyHealthController enemyHealth = objectsToDamage.GetComponent<EnemyHealthController>();
                    enemyHealth?.DamageEnemy(m_damageAmount);
                }
            }

            AudioManager.Instance.PlaySFXAdjusted(SFX.ENEMY_EXPLODE);
        }
    }
}
