using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicsGroundAnimation : MonoBehaviour
{
    GameObject relic; 
    float maxDistanceY = 3;
    float lowestDistanceY = -3;
    float counter = 0;
    bool itGot = false;
    Vector3 originalPos;
    float finalPos;
    // Start is called before the first frame update
    void Start()
    {
        relic = this.gameObject;
        originalPos = relic.transform.position;
        finalPos = originalPos.y + 1;
    }

    // Update is called once per frame
    void Update()
    {
        //cambia la direccion de arriba a abajo segun el valor de itGot
        if (itGot == false)
        {
            relic.transform.position += new Vector3(0, 0.002f, 0);
            if(relic.transform.position.y >= finalPos)
            {
                itGot = true;
            }
        }else if(itGot == true)
        {
            relic.transform.position -= new Vector3(0, 0.002f, 0);
            if(relic.transform.position == originalPos)
            {
                itGot = false;
            }
        }

       
            
       

       
        
        relic.transform.rotation *= Quaternion.Euler(0,0.5f,0);
    }
}
