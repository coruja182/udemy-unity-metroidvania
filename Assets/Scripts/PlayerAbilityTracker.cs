using UnityEngine;

public class PlayerAbilityTracker : MonoBehaviour
{
    [SerializeField] private bool m_doubleJumpUnlocked, m_dashUnlocked, m_ballUnlocked, m_bombUnlocked;

    public bool DoubleJumpUnlocked { get => m_doubleJumpUnlocked; set => m_doubleJumpUnlocked = value; }
    public bool DashUnlocked { get => m_dashUnlocked; set => m_dashUnlocked = value; }
    public bool BallUnlocked { get => m_ballUnlocked; set => m_ballUnlocked = value; }
    public bool BombUnlocked { get => m_bombUnlocked; set => m_bombUnlocked = value; }

    public void SetAbility(AbilityType type, bool value)
    {
        switch (type)
        {
            case AbilityType.DoubleJump:
                DoubleJumpUnlocked = value; break;
            case AbilityType.Dash:
                DashUnlocked = value; break;
            case AbilityType.Ball:
                BallUnlocked = value; break;
            case AbilityType.Bomb:
                BombUnlocked = value; break;
        }
    }
}
