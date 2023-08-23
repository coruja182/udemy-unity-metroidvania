using System.Collections;
using System.Collections.Generic;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
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
            Destroy(gameObject);
        }
    }
}
