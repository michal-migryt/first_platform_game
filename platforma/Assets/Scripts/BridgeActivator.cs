using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject BridgeObject;
    private Animator _bridgeAnimator;
    private Animator _leverAnimator;
    private float time, timer, leverTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _bridgeAnimator = BridgeObject.GetComponent<Animator>();
        _leverAnimator = GetComponent<Animator>();
        RuntimeAnimatorController ac = _bridgeAnimator.runtimeAnimatorController;
        RuntimeAnimatorController lc = _leverAnimator.runtimeAnimatorController;
        
        for(int i = 0; i < ac.animationClips.Length; i++)                 //For all animations
        {
             if(ac.animationClips[i].name == "OpenBridge")        //If it has the same name as your clip
            {
                time = ac.animationClips[i].length;
            }
        }
        for(int i=0; i < lc.animationClips.Length;i++)
        {
            if(lc.animationClips[i].name == "LeverTransition")
            {
                leverTime = lc.animationClips[i].length;
            }
        }
    }
    private void Update() {
        if(timer > 0)
            timer -= Time.deltaTime;
    }
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other) {
        if(Input.GetKey(KeyCode.E) && timer <= 0)
        {
            StartCoroutine(AnimationTransition());
        }
    }
    private IEnumerator AnimationTransition()
    {
        timer = time;
        _leverAnimator.SetBool("MakeTransition", true);
        yield return new WaitForSeconds(leverTime-0.1f);
        _leverAnimator.SetBool("MakeTransition", false);
        _bridgeAnimator.SetBool("MakeTransition", true);
        yield return new WaitForSeconds(0.1f);
        
        _bridgeAnimator.SetBool("MakeTransition", false);
    }
}
