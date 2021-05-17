using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreSlimesActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Slime;
    [SerializeField]
    private CameraSwitch _cameraSwitch;
    private SlimeEnemy[] _slimeEnemy = new SlimeEnemy[3];
    private bool Activate = false;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Slime.Length; i++)
        {
            _slimeEnemy[i] = Slime[i].GetComponent<SlimeEnemy>();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(_cameraSwitch != null)
        {
            for(int i = 0; i < _slimeEnemy.Length; i++)
            {
            if(Activate == true && _slimeEnemy[i].enabled == false && !_cameraSwitch.IsCameraMoving())
                _slimeEnemy[i].enabled = true;
            
            if((Activate == false| _cameraSwitch.IsCameraMoving()) && _slimeEnemy[i].enabled == true)
                _slimeEnemy[i].enabled = false;
            }
            
        }
        else
        {
        for(int i = 0; i < _slimeEnemy.Length; i++)
        {
            if(Activate == true && _slimeEnemy[i].enabled == false)
                _slimeEnemy[i].enabled = true;
        }
        }

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
            Activate = true;
    }
}
