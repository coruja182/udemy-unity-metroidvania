using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            switch (m_ability)
            {
                case AbilityType.DoubleJump:
                    playerAbilityTracker.DoubleJumpUnlocked = true;
                    break;
                case AbilityType.Dash:
                    playerAbilityTracker.DashUnlocked = true;
                    break;
                case AbilityType.Ball:
                    playerAbilityTracker.BallUnlocked = true;
                    break;
                case AbilityType.Bomb:
                    playerAbilityTracker.BombUnlocked = true;
                    break;
            }

            if (m_pickupEffect != null)
            {
                Instantiate(m_pickupEffect, transform.position, Quaternion.identity);
            }

            DisplayText();
            Destroy(gameObject);
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
