using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator m_doorAnimator;
    [SerializeField] private float m_distanceToOpen;
    private PlayerController m_playerReference;

    // Start is called before the first frame update
    void Start()
    {
        m_playerReference = PlayerHealthController.Instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, m_playerReference.transform.position) <= m_distanceToOpen)
        {
            m_doorAnimator.SetBool("doorOpen", true);
        }
    }
}
