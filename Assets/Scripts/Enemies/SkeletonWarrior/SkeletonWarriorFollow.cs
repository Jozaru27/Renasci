using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarriorFollow : SkeletonWarriorStates
{
    SkeletonWarrior skeletonWarrior;

    GameObject skeletonWarriorObject=GameObject.Find("SkeletonWarrior");
    GameObject playerObject=GameObject.Find("Player");
    
    bool warriorNearPlayer=false;
    public SkeletonWarriorFollow(SkeletonWarrior _skeletonWarrior) : base()
    {
        Debug.Log("FOLLOWING");
        name = STATES.FOLLOW;
        iniateVariables(skeletonWarrior);

        
    }

    public override void Entry()
    {
        base.Entry();
    }

    public override void Updating()
    {
        NavMeshAgent skeletonWarriorNav=skeletonWarriorObject.GetComponent<NavMeshAgent>();

        float distanceToPlayer=Vector3.Distance(skeletonWarriorObject.transform.position,playerObject.transform.position);

        skeletonWarriorNav.destination=playerObject.transform.position;

        if(distanceToPlayer<=2){
            warriorNearPlayer=true;
        }else{
            warriorNearPlayer=false;
        }
        if(BeginAttack()){
            nextState=new SkeletonWarriorAttack(skeletonWarrior);
            actualPhase=EVENTS.EXIT;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

    public bool BeginAttack(){
    if(warriorNearPlayer==true){
        return true;
    }else{
        return false;
    }
    }
}
