using UnityEngine;

public class CameraController : MonoBehaviour
{

    private PlayerController playerController;
    private Vector3 cameraOffset = new Vector3(0f, 0f, -10f);
    [SerializeField] private BoxCollider2D boundsBox;
    private float halfHeight, halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        AudioManager.Instance.PlayLevelMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController != null)
        {
            // camera follow player
            Vector3 desiredCameraPosition = playerController.transform.position + cameraOffset;
            desiredCameraPosition.x = Mathf.Clamp(desiredCameraPosition.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth);
            desiredCameraPosition.y = Mathf.Clamp(desiredCameraPosition.y, boundsBox.bounds.min.y + halfHeight, boundsBox.bounds.max.y - halfHeight);
            transform.position = desiredCameraPosition;
        }
        else
        {
            playerController = FindObjectOfType<PlayerController>();
        }
    }
}
