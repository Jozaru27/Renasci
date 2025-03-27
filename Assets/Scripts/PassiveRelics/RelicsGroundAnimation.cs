using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicsGroundAnimation : MonoBehaviour
{
    GameObject relic;
    float maxDistanceY = 3;
    float lowestDistanceY = -3;
    // Start is called before the first frame update
    void Start()
    {
        relic = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        relic.transform.rotation *= Quaternion.Euler(0,1,0);
    }
}
