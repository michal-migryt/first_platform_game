using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCheckpoint : MonoBehaviour
{
    // Start is called before the first frame update
    public LevelController _levelController;
    private CameraSwitch _cameraSwitch;
    void Start()
    {
        _cameraSwitch = GetComponent<CameraSwitch>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            _levelController.MainCameraRespawnPoint = _cameraSwitch.GetCameraTarget();
            _levelController.RespawnPoint.position = transform.position;
        }
    }
}
