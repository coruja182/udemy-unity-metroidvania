using UnityEngine;

public class FullMapCameraController : MonoBehaviour, Singleton
{
    public static FullMapCameraController Instance { get; private set; }

    [SerializeField] private float m_zoomSpeed, m_maxZoom, m_minZoom;
    [SerializeField] private MapCameraController m_mapCameraController;

    public float ZoomMotion { get; set; }
    public Vector2 MoveMotion { get; set; } = Vector2.zero;

    private Camera m_camera;
    private float m_startSize;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_camera = GetComponent<Camera>();
        m_startSize = m_camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(MoveMotion.x, MoveMotion.y).normalized * m_camera.orthographicSize * Time.unscaledDeltaTime;
        m_camera.orthographicSize += m_zoomSpeed * ZoomMotion * Time.unscaledDeltaTime;
        m_camera.orthographicSize = Mathf.Clamp(m_camera.orthographicSize, m_minZoom, m_maxZoom);
    }

    public void DestroyThyself()
    {
        Destroy(gameObject);
        Instance = null;
    }

    private void OnEnable()
    {
        transform.position = m_mapCameraController.transform.position;
    }
}
