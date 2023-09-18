using UnityEngine;

public class MapCameraController : MonoBehaviour
{
    private Vector3 m_offset;

    // Start is called before the first frame update
    void Start()
    {
        m_offset.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerHealthController.Instance.transform.position + m_offset;
    }
}
