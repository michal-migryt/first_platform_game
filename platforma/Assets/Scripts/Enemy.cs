using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool movementStart = true;
    [SerializeField]
    protected float direction;
    protected float ForceDirection;
    protected CapsuleCollider2D cc;
    [SerializeField]
    protected LayerMask _layerMask;
    protected Vector2 ColliderBottom;
    
    // Start is called before the first frame update
    private void Start() {
        cc = GetComponent<CapsuleCollider2D>();
        ColliderBottom = new Vector2(cc.bounds.center.x, cc.bounds.center.y - cc.size.y/2);
    }
    private void Update() {
        EnemyMovement();
        
    }
    // Update is called once per frame


    virtual protected void EnemyMovement()
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
