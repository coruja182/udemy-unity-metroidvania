using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private PlayerController player;

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
            Vector3 playerXYPosition = this.player.transform.position;
            playerXYPosition.z = this.transform.position.z;
            this.transform.position = playerXYPosition;
        }
    }
}
