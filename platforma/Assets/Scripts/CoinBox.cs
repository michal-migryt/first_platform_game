using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour
{
    public int AmountOfCoins;
    private Animator _animator;
    private BoxCollider2D _boxcollider2d;
    private bool isPlayerDown;
    private float colliderThickness = 0.005f;
    private float animationTime;
    private Vector3 raycastBottomLeft;
    [SerializeField]
    private GameObject CoinPrefab;
    private GameObject SpawnedCoin;
    private PlayerStats _playerStats;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _boxcollider2d = GetComponent<BoxCollider2D>();
        calculateRayCastOrigin();
    }
    private void Update() {
        if(SpawnedCoin != null)
            SpawnedCoin.transform.Translate(Vector3.up*Time.deltaTime*1.5f);

        animationTime -= Time.deltaTime;
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player" && AmountOfCoins > 0 && animationTime <= 0)
        {
            _playerStats = other.gameObject.GetComponent<PlayerStats>();
            isPlayerDown = false;

            for(int i = 0; i < 3; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(raycastBottomLeft +new Vector3(i*_boxcollider2d.size.x/2,0,0), Vector2.down, 0.05f);
                if(hit)
                isPlayerDown = true;
            }

            if(isPlayerDown)
            {
                _animator.SetBool("PlayerHit", true);
                StartCoroutine(AnimationTime());
                AmountOfCoins--;
                _playerStats.AddCoin();
            }
        }
        
        if(AmountOfCoins == 0)
        {
            StartCoroutine(EndOfCoins(0.01f));
        }
    }
    private void calculateRayCastOrigin()
    {
        raycastBottomLeft = new Vector2(_boxcollider2d.bounds.center.x-(_boxcollider2d.size.x/2f+colliderThickness), _boxcollider2d.bounds.center.y -(_boxcollider2d.size.y/2f+colliderThickness));    
    }
    private IEnumerator AnimationTime()
    {
        Transform CoinSpawn = transform; 
        CoinSpawn.position += new Vector3(_boxcollider2d.size.x/2,_boxcollider2d.size.y,0);
        SpawnedCoin = GameObject.Instantiate(CoinPrefab,CoinSpawn.position, Quaternion.identity);
        AnimatorStateInfo animationState = _animator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] animatorClip = _animator.GetCurrentAnimatorClipInfo(0);
        animationTime = animatorClip[0].clip.length * animationState.length *2;
        Debug.Log(animationTime);
        yield return new WaitForSeconds(animationTime);
        animationTime += 0.5f; // to prevent player from bugging spawned coins and box
        _animator.SetBool("PlayerHit", false);
        yield return new WaitForSeconds(animationTime);
        Destroy(SpawnedCoin);
    }
    private IEnumerator EndOfCoins(float time)
    {
        yield return new WaitForSeconds(time);
        _animator.SetBool("HasCoins", false); 
        tag = "Untagged";
    }
}
