using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public AudioSource _audioSource;
    PlayerStats _playerStats;
    private void Start() {
        _playerStats = GetComponent<PlayerStats>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Coin")
        {
            _playerStats.AddCoin();
            Destroy(other.gameObject);
            _audioSource.Play();
        }
        if(other.gameObject.tag == "Heart")
        {
            if(_playerStats.GetCurrentHalfHearts()/2 != _playerStats.GetMaxHearts())
            {
                if(_playerStats.GetMaxHearts() - _playerStats.GetCurrentHalfHearts() == 0.5f)
                    _playerStats.Heal(1);
                else
                    _playerStats.Heal(2);
                Destroy(other.gameObject);
            }
        }
    }
}
