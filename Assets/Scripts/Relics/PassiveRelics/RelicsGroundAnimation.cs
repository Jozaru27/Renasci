using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicsGroundAnimation : MonoBehaviour
{
    [SerializeField] bool isActive;

    GameObject relic; 
    float maxDistanceY = 3;
    float lowestDistanceY = -3;
    float counter = 2;
    float timePass = 0;
    bool itGot = false;
    Vector3 originalPos;
    //float finalPos;
    Vector3 finalPos1;
    float middlePoint;
    public AnimationCurve curve;

    void Start()
    {
        relic = this.gameObject;
        originalPos = relic.transform.position;
        //finalPos = originalPos.y + 1;
        finalPos1 = new Vector3(originalPos.x, originalPos.y + 1, originalPos.z);
        middlePoint = originalPos.y / finalPos1.y;
    }

    void Update()
    {
        
        //cambia la direccion de arriba a abajo segun el valor de itGot
        if (itGot == false)
        {
            timePass += Time.deltaTime;
            //relic.transform.position += new Vector3(0, 0.002f, 0);
            //float t = Mathf.PingPong(Time.time / counter, 1);
            relic.transform.position = Vector3.Lerp(originalPos,finalPos1,curve.Evaluate(timePass/counter));
            if(timePass >=counter)
            {
                itGot = true;
                timePass = 0;
            }
        }else if(itGot == true)
        {
            timePass += Time.deltaTime;
            //relic.transform.position -= new Vector3(0, 0.002f, 0);
            //float t = Mathf.PingPong(Time.time / counter, 1);
            relic.transform.position = Vector3.Lerp(finalPos1,originalPos, curve.Evaluate(timePass / counter));
            if (timePass >= counter)
            {
                itGot = false;
                timePass = 0;
            }
        }

        if (!isActive)
            relic.transform.rotation *= Quaternion.Euler(0,0.5f,0);
    }
}
