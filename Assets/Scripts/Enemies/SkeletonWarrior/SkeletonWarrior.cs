using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : MonoBehaviour
{
    SkeletonWarriorStates FSM;
    public GameObject playerObject;
    public GameObject skeletonWarriorObject;

    void Start()
    {
       playerObject = GameObject.Find("Player");
        skeletonWarriorObject = GameObject.Find("SkeletonWarrior");
      
       FSM = new SkeletonWarriorFollow(this);
    }

    void Update()
    {
        FSM = FSM.Process();
    }
}
