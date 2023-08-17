using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private PlayerController player;
    private Vector3 cameraOffset = new Vector3(0f, 0f, -10f);

    // Start is called before the first frame update
    void Start()
    {
        this.player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // camera follow player
            this.transform.position = this.player.transform.position + cameraOffset;
        }
    }
}
