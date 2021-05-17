using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public bool movementStart = true;
    [SerializeField]
    private float direction;
    private float ForceDirection;
    private CapsuleCollider2D cc;
    [SerializeField]
    private LayerMask _layerMask;
    private Vector2 ColliderBottom;
    private void OnCollisionEnter2D(Collision2D other) {
    if(other.gameObject.tag == "Player")
    {
        if(transform.position.x - other.transform.position.x >= 0)
        ForceDirection = -1;
        else
        ForceDirection = 1;
    other.rigidbody.velocity = Vector2.zero;
    other.rigidbody.AddForce(new Vector2(4*ForceDirection,4), ForceMode2D.Impulse);    
    }
    }
    // Start is called before the first frame update
    private void Start() {
        cc = GetComponent<CapsuleCollider2D>();
        ColliderBottom = new Vector2(cc.bounds.center.x, cc.bounds.center.y - cc.size.y/2);
    }
    private void Update() {
        EnemyMovement();
        
    }
    // Update is called once per frame


    void EnemyMovement()
    {
          // 1 equals right, -1 equals left
        ColliderBottom = new Vector2(cc.bounds.center.x, cc.bounds.center.y - cc.size.y/2);
        transform.position += new Vector3(direction, 0, 0) * Time.deltaTime *2;
        for(int i = 0;i<3;i++)
        {
            Debug.DrawRay(ColliderBottom + new Vector2(cc.size.x/2*direction,0+(cc.size.y/2*i)), new Vector2(direction,0), Color.blue);
            if(Physics2D.Raycast(ColliderBottom + new Vector2(cc.size.x/2*direction,0+(cc.size.y/2*i)), new Vector2(direction, 0),0.1f, _layerMask))
            {
                direction *= -1;
                transform.localScale = new Vector3(transform.localScale.x*-1, 1 ,1);
            }
        }

        
    }
}
