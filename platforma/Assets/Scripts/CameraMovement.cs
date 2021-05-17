using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    private Vector3 playerStartPosition;
    private Vector3 cameraStartPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerStartPosition = playerTransform.position;
        cameraStartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraStartPosition + (new Vector3(playerTransform.position.x - playerStartPosition.x,0));
    }
}
