using TMPro;
using UnityEngine;

public enum AbilityType
{
    DoubleJump,
    Dash,
    Ball,
    Bomb
}

public class AbilityUnlock : MonoBehaviour
{
    [SerializeField] private AbilityType m_ability;
    [SerializeField] private GameObject m_pickupEffect;
    [SerializeField] private string m_unlockMessage;
    [SerializeField] private TMP_Text m_unlockText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // GetComponentInParent explanation: Player (with abilityTracker) -> Collider2D
            PlayerAbilityTracker playerAbilityTracker = other.GetComponentInParent<PlayerAbilityTracker>();
            playerAbilityTracker.SetAbility(m_ability, true);
            SaveManager.SaveAbility(m_ability, true);

            if (m_pickupEffect)
            {
                Instantiate(m_pickupEffect, transform.position, Quaternion.identity);
            }

            DisplayText();
            Destroy(gameObject);

            AudioManager.Instance.PlaySFX(SFX.PICKUP_GEM);
        }
    }

    private void DisplayText()
    {
        m_unlockText.text = m_unlockMessage;
        m_unlockText.gameObject.SetActive(true);
        // setting canvas parent to null, so it doesn't get destroyed
        m_unlockText.transform.parent.SetParent(null);
        m_unlockText.transform.parent.position = transform.position;
        Destroy(m_unlockText.gameObject, 2f);
    }
}
