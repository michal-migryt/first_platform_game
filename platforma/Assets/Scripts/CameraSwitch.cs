using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    private enum WhereIsTargetCamera { LEFT, RIGHT };
    private enum CameraMovementDirection { ORIGIN, TARGET};
    private WhereIsTargetCamera where;
    private CameraMovementDirection CameraMovDir;
    private int i = 1;
    [SerializeField]
    private Transform CameraTransform;
    [SerializeField]
    private Transform CameraOrigin;
    [SerializeField]
    private Transform CameraTarget;
    private float CameraChangeDelay = 1f;
    private bool TriggerCameraChange = false;
    private Vector2 PlayerVelocity;
    private void Start() {
        CameraMovDir = CameraMovementDirection.TARGET;
        TargetCameraDirection();
    }
    private void Update() {
        if(!IsCameraMoving())
            TriggerCameraChange = false;
        if(TriggerCameraChange && CameraMovDir == CameraMovementDirection.TARGET)
        {
            CameraTransform.position = Vector3.MoveTowards(CameraTransform.position, CameraTarget.position, 0.025f);
        }
        
        if(TriggerCameraChange && CameraMovDir == CameraMovementDirection.ORIGIN)
        {    
            CameraTransform.position = Vector3.MoveTowards(CameraTransform.position, CameraOrigin.position, 0.025f);
        }
        //Debug.Log(IsCameraMoving());
        
    }
    public Transform GetCameraTarget()
    {
        return CameraTarget;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            PlayerVelocity = other.attachedRigidbody.velocity;
            if((isFacingTargetCamera(other) && other.attachedRigidbody.velocity.x  == 0) || isForcedToTarget(other))
            {
                CameraMovDir = CameraMovementDirection.TARGET;
                TriggerCameraChange = true;
            }
            else if((!isFacingTargetCamera(other) && other.attachedRigidbody.velocity.x  == 0) || isForcedToOrigin(other))
            {
                CameraMovDir = CameraMovementDirection.ORIGIN;
                TriggerCameraChange = true;
            }
            
            
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {   // PLAYER VELOCITY MOZE BYC ZAPISANE W ONTRIGGERENTER2D A ZEROWANE W METODZIE ISCAMERAMOVING NA SAMYM DOLE PRZED OBA RETURN FALSE
            if((isFacingTargetCamera(other) && PlayerVelocity.x == 0) || isForcedToTarget(other))
            {
                TriggerCameraChange = true;
                CameraMovDir = CameraMovementDirection.TARGET;
                
            }

            else if((!isFacingTargetCamera(other) && PlayerVelocity.x == 0) || isForcedToOrigin(other))
            {
                TriggerCameraChange = true;
                CameraMovDir = CameraMovementDirection.ORIGIN;
                
            }
           
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            while(IsCameraMoving()) // in case player gets pushed out while camera is moving
            {
                if(other.attachedRigidbody.velocity.x > 0 && where == WhereIsTargetCamera.RIGHT)
                    CameraMovDir = CameraMovementDirection.TARGET;
                else if(other.attachedRigidbody.velocity.x > 0 && where == WhereIsTargetCamera.LEFT)
                    CameraMovDir = CameraMovementDirection.ORIGIN;
                else if(other.attachedRigidbody.velocity.x < 0 && where == WhereIsTargetCamera.RIGHT)
                    CameraMovDir = CameraMovementDirection.ORIGIN;
                else
                    CameraMovDir = CameraMovementDirection.TARGET;

                if(TriggerCameraChange && CameraMovDir == CameraMovementDirection.TARGET)
                    CameraTransform.position = Vector3.MoveTowards(CameraTransform.position, CameraTarget.position, 0.025f);
        
                if(TriggerCameraChange && CameraMovDir == CameraMovementDirection.ORIGIN)  
                    CameraTransform.position = Vector3.MoveTowards(CameraTransform.position, CameraOrigin.position, 0.025f);
                
            }
            PlayerVelocity = Vector2.zero;
        }
    }
   
    
    private void TargetCameraDirection()
    {
        if((CameraOrigin.position.x > 0 && CameraTarget.position.x < 0) || (CameraOrigin.position.x - CameraTarget.position.x > 0 && (CameraOrigin.position.x > 0 && CameraTarget.position.x > 0) ) )
        where = WhereIsTargetCamera.LEFT;
        else
        where = WhereIsTargetCamera.RIGHT;
    }
    private bool isFacingTargetCamera(Collider2D other)
    {
        
        if(where == WhereIsTargetCamera.RIGHT && other.gameObject.transform.localScale.x > 0)
        {
            return true;
        }
        else if(where == WhereIsTargetCamera.LEFT && other.gameObject.transform.localScale.x < 0)
        {
            return true;
        }
        else
        return false;
    }
    private bool isForcedToOrigin(Collider2D other)
    {
        if(where == WhereIsTargetCamera.RIGHT && other.attachedRigidbody.velocity.x < 0)
            return true;
        if(where == WhereIsTargetCamera.LEFT && other.attachedRigidbody.velocity.x > 0)
            return true;
        return false;
    }
    private bool isForcedToTarget(Collider2D other)
    {
        if(where == WhereIsTargetCamera.RIGHT && PlayerVelocity.x > 0)
            return true;
        else if(where == WhereIsTargetCamera.LEFT && PlayerVelocity.x < 0)
            return true;
        else
            return false;
    }
    
    public bool IsCameraMoving()
    {
        if(CameraMovDir == CameraMovementDirection.TARGET && Mathf.Abs(CameraTransform.position.x - CameraTarget.position.x) <= 0.1f)
        return false;
        if(CameraMovDir == CameraMovementDirection.ORIGIN && Mathf.Abs(CameraTransform.position.x - CameraOrigin.position.x) <= 0.1f)
        return false;
        return true;
    }
    public Transform test()
    {
        return CameraTransform;
    }  
    public void SetSavedPlayerVelocity(Vector2 velocity)
    {
        PlayerVelocity = velocity;
    }
}
