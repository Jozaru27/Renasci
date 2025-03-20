using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarriorAttack : SkeletonWarriorStates
{
    
    
    bool warriorFarPlayer=false;
    public SkeletonWarriorAttack(SkeletonWarrior _skeletonWarrior) : base()
    {
        Debug.Log("ATTACKING");
        name = STATES.ATTACK;
        skeletonWarrior=_skeletonWarrior;
        iniateVariables(skeletonWarrior);
    }

    public override void Entry()
    {
        base.Entry();
    }

    public override void Updating()
    {
        float distanceToPlayer=Vector3.Distance(skeletonWarrior.skeletonWarriorObject.transform.position,skeletonWarrior.playerObject.transform.position);

        if(distanceToPlayer>2){
            warriorFarPlayer=true;
        }else{
            warriorFarPlayer=false;
        }

        if(blocking())
        {
            nextState=new SkeletonWarriorBlock(skeletonWarrior);
            actualPhase=EVENTS.EXIT;
        }

        if(playerFar()){
            nextState=new SkeletonWarriorFollow(skeletonWarrior);
            actualPhase=EVENTS.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
    public bool blocking(){
        return false;
    }
    public bool playerFar(){
    if(warriorFarPlayer==true){
        return true;
    }else{
        return false;
    }
    }
}
