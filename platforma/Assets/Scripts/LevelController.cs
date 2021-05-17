using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject Player;
    public Camera MainCamera;
    public Transform RespawnPoint;
    public Transform MainCameraRespawnPoint;
    private PlayerStats _playerStats;

    // Start is called before the first frame update
    void Start()
    {
        _playerStats = Player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerStats.GetRespawnNeed() || _playerStats.GetCurrentHalfHearts() <= 0)
        {
            Respawn();
            _playerStats.Respawned();
        }
    }

    private void Respawn()
    {
        _playerStats.Death();
        Player.transform.position = RespawnPoint.position;
        MainCamera.transform.position = MainCameraRespawnPoint.transform.position;
    }
}
