using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] private GameObject m_bossToActivate;

    private void Awake()
    {
        BossBattle bossBattleRef = m_bossToActivate.GetComponent<BossBattle>();
        if (bossBattleRef && SaveManager.IsBossBeaten(bossBattleRef.BossId))
        {
            // disable the collider if the boss is already beaten
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_bossToActivate.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
