using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeActivator : EnemyActivator
{
    // Start is called before the first frame update
    private SlimeEnemy SE;
    private CameraSwitch _cameraSwitch;
    void Start()
    {
        SE = Enemy.GetComponent<SlimeEnemy>();
        _cameraSwitch = GetComponent<CameraSwitch>();
        SE.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Activate == true)
        timeToActivate -= Time.deltaTime;
        if(_cameraSwitch != null)
        {
            if(timeToActivate <= 0 && SE.enabled == false && !_cameraSwitch.IsCameraMoving())
                SE.enabled = true;
            if(_cameraSwitch.IsCameraMoving() && SE.enabled == true)
                SE.enabled = false;
        }
        else
        if(timeToActivate <= 0 && SE.enabled == false)
            SE.enabled = true;

    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        Activate = true;
    }

}
