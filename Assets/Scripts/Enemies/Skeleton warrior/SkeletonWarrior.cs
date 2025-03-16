using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : MonoBehaviour
{
    States FSM;
    // Start is called before the first frame update
    void Start()
    {
       FSM = new Follow();
       FSM.iniateVariables(this);
    }

    // Update is called once per frame
    void Update()
    {
        FSM=FSM.Process();
    }
}
