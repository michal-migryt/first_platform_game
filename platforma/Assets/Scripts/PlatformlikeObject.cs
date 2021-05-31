using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformlikeObject : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        other.collider.transform.SetParent(transform);
    }
    public void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        other.collider.transform.SetParent(null);
    }
}
