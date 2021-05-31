using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : Enemy
{
    private void OnCollisionEnter2D(Collision2D other) {
    if(other.gameObject.tag == "Player")
    {
        if(transform.position.x - other.transform.position.x >= 0)
        ForceDirection = -1;
        else
        ForceDirection = 1;
    
        other.rigidbody.AddForce(new Vector2(4*ForceDirection,4), ForceMode2D.Impulse);    
    }
    }
}
