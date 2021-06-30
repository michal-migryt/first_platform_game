using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject Player;
    
    public Camera MainCamera;
    public Transform RespawnPoint;
    public Transform MainCameraRespawnPoint;
    public GameObject[] CameraChangeTriggers;
    public GameObject[] Platforms;
    private PlayerStats _playerStats;
    private Rigidbody2D _rigidbody2d;
    private CameraSwitch[] cameraSwitchArray;
    private FollowPath[] followPathsArray;

    // Start is called before the first frame update
    void Start()
    {
        _playerStats = Player.GetComponent<PlayerStats>();
        _rigidbody2d = Player.GetComponent<Rigidbody2D>();
        cameraSwitchArray = new CameraSwitch[CameraChangeTriggers.Length];
        followPathsArray = new FollowPath[Platforms.Length];
        for(int i=0; i<cameraSwitchArray.Length;i++)
        {
            cameraSwitchArray[i] = CameraChangeTriggers[i].GetComponent<CameraSwitch>();
        }
        for(int i=0; i<followPathsArray.Length;i++)
        {
            followPathsArray[i] = Platforms[i].GetComponent<FollowPath>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerStats.GetRespawnNeed() || _playerStats.GetCurrentHalfHearts() <= 0)
        {
            Respawn();
            _playerStats.Respawned();
        }
        PlatformCameraHandler();
    }

    private void Respawn()
    {
        _playerStats.Death();
        Player.transform.position = RespawnPoint.position;
        MainCamera.transform.position = MainCameraRespawnPoint.transform.position;
        _rigidbody2d.velocity = Vector2.zero;
    }
    private void PlatformCameraHandler()
    {
        bool cameraMoves = false;
        if(cameraSwitchArray[0].IsCameraMoving() && cameraSwitchArray[1].IsCameraMoving())
                cameraMoves = true;
        
        if(cameraMoves)
        {
            for(int i=0;i<followPathsArray.Length;i++)
            {
                followPathsArray[i].enabled = false;
            }
        }
        else
        {
            for(int i=0;i<followPathsArray.Length;i++)
            {
                followPathsArray[i].enabled = true;
            }
        }
    }
}
