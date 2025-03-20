using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarriorFollow : SkeletonWarriorStates
{
    
    bool warriorNearPlayer=false;
    public SkeletonWarriorFollow(SkeletonWarrior _skeletonWarrior) : base()
    {
        Debug.Log("FOLLOWING");
        name = STATES.FOLLOW;
        skeletonWarrior=_skeletonWarrior;
        iniateVariables(skeletonWarrior);
    }

    public override void Entry()
    {
        skeletonWarrior.skeletonWarriorAnimator.SetBool("Run",true);
        skeletonWarrior.skeletonWarriorAnimator.SetBool("Idle",false);
        base.Entry();
    }

    public override void Updating()
    {
        //NavMeshAgent skeletonWarriorNav=skeletonWarrior.skeletonWarriorObject.GetComponent<NavMeshAgent>();
        NavMeshAgent skeletonWarriorNav = skeletonWarrior.gameObject.GetComponent<NavMeshAgent>();

        float distanceToPlayer=Vector3.Distance(skeletonWarrior.skeletonWarriorObject.transform.position,skeletonWarrior.playerObject.transform.position);

        skeletonWarriorNav.destination=skeletonWarrior.playerObject.transform.position;

       

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
