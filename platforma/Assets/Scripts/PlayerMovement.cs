using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 4f;
    public float jumpForce = 6f;
    private float direction;

    public LayerMask _layerMask;
    private Rigidbody2D _rigidbody2d;
    private BoxCollider2D _boxcollider;
    private PlayerStats _playerStats;
    private Animator _animator;
    private CameraSwitch _cameraswitch;

    private Vector2 PlayerPositionSave;
    private Vector2 PlayerVelocitySave;
    private float hurtTime = 0f;
    private bool cameraMoves;
    private bool stoppedMoving = true;
    

    // Raycast variables
    [SerializeField]
    private float rayLength = 0.02f;
    private float colliderThickness = 0.005f;
    [SerializeField]
    private float horizontalRays = 4f;
    [SerializeField]
    private float verticalRays = 3f;
    
    private float distanceBetweenHorizontalRays;
    private float distanceBetweenVerticalRays;
    private Vector2 raycastBottomLeft;
    private Vector2 raycastBottomRight;
    private Vector2 rayCastTopLeft;
    private Vector2 rayCastTopRight;
    private Vector2 rayCastOrigin;

    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _boxcollider = GetComponent<BoxCollider2D>();
        _playerStats = GetComponent<PlayerStats>();
        _animator = GetComponent<Animator>();
        distanceBetweenHorizontalRays = _boxcollider.size.y/(horizontalRays-1);
        distanceBetweenVerticalRays = _boxcollider.size.x/(verticalRays-1);
        calculateRayCastOrigin();

    }
    // Update is called once per frame
    void Update()
    {
        calculateRayCastOrigin();
        CheckInput();
        AnimatorSetter();
        
        Debug.Log(PlayerVelocitySave);
        Debug.Log(_rigidbody2d.IsAwake());     
    }
    private void FixedUpdate() {
        FreezeOnCameraChange();
    }

    private void CheckInput()
    {
        if(_cameraswitch == null)
            cameraMoves = false;
        else
            cameraMoves = _cameraswitch.IsCameraMoving();
        if(hurtTime <= 0 && !cameraMoves)
        {
            direction = Input.GetAxis("Horizontal");
            _animator.SetFloat("Speed", Mathf.Abs(direction));
            if((direction > 0 && !isFacingRight()) || (direction<0 && isFacingRight()))
                Flip();
            if(canMoveHorizontal())
                transform.position += new Vector3(direction, 0, 0) *Time.deltaTime * movementSpeed;
            if(Input.GetButtonDown("Jump") && canJump())
            {
                _rigidbody2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            
        }
    }
    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    }

    private bool canJump()
    {
        for(int i = 0; i < verticalRays; i++)
        {
        RaycastHit2D hit = Physics2D.Raycast(rayCastOrigin + new Vector2(distanceBetweenVerticalRays*i,colliderThickness)*(-transform.localScale), Vector2.down, rayLength*2, _layerMask);
        Debug.DrawRay(rayCastOrigin + new Vector2(distanceBetweenVerticalRays*i,colliderThickness)*(-transform.localScale), Vector2.down*rayLength, Color.green);
        if(hit)
        {
            _rigidbody2d.velocity = Vector2.zero;
            return true;
        }
        }
        return false;
        
    }
    private bool canMoveHorizontal()
    {
        
        for(int i = 0; i < horizontalRays; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayCastOrigin+new Vector2(0,distanceBetweenHorizontalRays*i), Vector2.right*transform.localScale, rayLength, _layerMask);
            Debug.DrawRay(rayCastOrigin+ new Vector2(0, distanceBetweenHorizontalRays*i),Vector2.right*transform.localScale*rayLength,Color.red);
            if(hit)
            {
            return false;
            }
        }
        return true;
        
        /*rayCastHorizontal = new Vector3(_boxcollider.bounds.center.x +((_boxcollider.size.x/2+colliderThickness)*transform.localScale.x), _boxcollider.bounds.center.y);
        RaycastHit2D hit = Physics2D.Raycast(rayCastHorizontal, Vector2.right*transform.localScale.x,rayLength);
        return !hit;
        */
    }
    private bool isFacingRight()
    {
        if(transform.localScale.x > 0)
        return true;
        else
        return false;
    }
    private void calculateRayCastOrigin()
    {
        raycastBottomLeft = new Vector2(_boxcollider.bounds.center.x-(_boxcollider.size.x/2f-colliderThickness), _boxcollider.bounds.center.y -(_boxcollider.size.y/2f-colliderThickness));
        if(isFacingRight())
        {
            rayCastOrigin = raycastBottomLeft + new Vector2(_boxcollider.size.x,0);
        }
        else
        {
            rayCastOrigin = raycastBottomLeft;
        }
    }

    private void FreezeOnCameraChange()
    {
        
        if(_cameraswitch != null && _cameraswitch.IsCameraMoving() && stoppedMoving == false)
        {
            
            transform.position = PlayerPositionSave;
            _rigidbody2d.velocity = Vector2.zero;
        }
        if(_cameraswitch != null && stoppedMoving == false && !_cameraswitch.IsCameraMoving())
        {
            stoppedMoving = true;
            _rigidbody2d.velocity = PlayerVelocitySave; // giving back Player his speed he had when he jumped into CameraChangeTrigger collider
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            if(hurtTime <= 0)
            {
                _playerStats.TakeDamage(1);
                _animator.SetBool("IsHurt", true);
                hurtTime = 1f;
            }
        }
        /*if(other.gameObject.tag == "CoinBox")
        {
            bool CanGetCoin = false;
            for(int i = 0; i < verticalRays; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayCastOrigin + new Vector2(distanceBetweenVerticalRays*i,colliderThickness+_boxcollider.size.y)*(-transform.localScale), Vector2.up, rayLength*4, _layerMask);
            Debug.DrawRay(rayCastOrigin + new Vector2(distanceBetweenVerticalRays*i,colliderThickness+_boxcollider.size.y)*(-transform.localScale), Vector2.up*rayLength, Color.green);
            if(hit)
            CanGetCoin = true;
        }
        //if(CanGetCoin)
          //  _playerStats.AddCoin();
        }*/
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Coin")
        {
            _playerStats.AddCoin();
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "CameraChange")
        {
            _cameraswitch = other.gameObject.GetComponent<CameraSwitch>();
            PlayerPositionSave = transform.position;
            PlayerVelocitySave = _rigidbody2d.velocity;
            
            _rigidbody2d.velocity = Vector2.zero;
            stoppedMoving = false;
            _rigidbody2d.Sleep();
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "CameraChange")
        {
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "CameraChange")
        {
            _cameraswitch = null;
        }
    }
    private void AnimatorSetter()
    {
        if(Mathf.Abs(_rigidbody2d.velocity.y) != 0.0)
            _animator.SetBool("IsJumping", true);
        else
            _animator.SetBool("IsJumping", false);
        if(_animator.GetBool("IsHurt") == true && hurtTime <= 0f)
            {
            _animator.SetBool("IsHurt", false);
            }
        if(hurtTime > 0)
            hurtTime -= Time.deltaTime;

    }
}
