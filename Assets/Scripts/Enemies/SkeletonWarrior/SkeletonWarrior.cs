using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : MonoBehaviour
{
    SkeletonWarriorStates FSM;

    void Start()
    {
       FSM = new SkeletonWarriorFollow(this);
       FSM.iniateVariables(this);
    }

    void Update()
    {
        FSM = FSM.Process();
    }
}
