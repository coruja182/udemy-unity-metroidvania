using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerControls : MonoBehaviour
{
    Vector2 m_Motion = Vector2.zero;
    PlayerController m_PlayerController;

    void Start()
    {
        m_PlayerController = GetComponent<PlayerController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_Motion = context.ReadValue<Vector2>();
        m_PlayerController.SetMotion(m_Motion);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        m_PlayerController.Jump();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        m_PlayerController.Attack();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        m_PlayerController.Dash();
    }

    public void ToggleFullscreenMap(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        UIController.Instance.ToggleFullscreenMap();
    }

    public void TogglePause(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        UIController.Instance.PauseToggle();
    }

}
