using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private Transform m_cameraPosition;
    [SerializeField] private float m_cameraSpeed;

    private CameraController m_cameraController;

    // Start is called before the first frame update
    void Start()
    {
        m_cameraController = FindObjectOfType<CameraController>();
        m_cameraController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_cameraController.transform.position = Vector3.MoveTowards(m_cameraController.transform.position, m_cameraPosition.position, m_cameraSpeed * Time.deltaTime);
    }
}
