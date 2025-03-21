using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : MonoBehaviour
{
    SkeletonWarriorStates FSM;
    public GameObject playerObject;
    public GameObject skeletonWarriorObject;
    public Animator skeletonWarriorAnimator;

    void Start()
    {
       playerObject = GameObject.Find("Player");
        skeletonWarriorObject = this.gameObject;
        skeletonWarriorAnimator=skeletonWarriorObject.GetComponent<Animator>();
      
       FSM = new SkeletonWarriorIdle(this);
    }

    void Update()
    {
        FSM = FSM.Process();
    }

    
}
