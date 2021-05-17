using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPath : MonoBehaviour
{
    [SerializeField]
    private Transform[] Points;
    public IEnumerator <Transform> ReturnPoint()
    {
        if(Points == null || Points.Length < 1)
        {
            Debug.Log("Not enough points to form path");
            yield break;
        }

        int direction=1, i=0;

        while(true)
        {
            yield return Points[i];
            if(Points.Length == 1)
                continue;

            if(i <= 0)
                direction = 1;
            else if (i >= Points.Length-1)
                direction =- 1;

            i = i + direction;
        }
        

    }
}
