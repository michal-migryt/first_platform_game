using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFromFalling : MonoBehaviour
{
    private PlayerStats ps;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            ps = other.GetComponent<PlayerStats>();
            ps.setRespawnNeed();
        }

    }
}
