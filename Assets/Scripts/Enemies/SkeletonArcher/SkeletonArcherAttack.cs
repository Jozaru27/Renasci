using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonArcherAttack : SkeletonArcherStates
{
    
    //bool warriorFarPlayer=false;

    public SkeletonArcherAttack(SkeletonArcher _skeletonArcher) : base()
    {
        //Debug.Log("ATTACKING");
        name = STATES.ATTACK;
        skeletonArcher=_skeletonArcher;
        iniateVariables(skeletonArcher);
    }

    public override void Entry()
    {
        skeletonArcher.skeletonArcherAgent.isStopped = true;
        //skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Attack();
        //skeletonArcher.skeletonArcherObject.transform.LookAt(skeletonArcher.playerObject.transform.position);
        base.Entry();
    }

    public override void Updating()
    {
        //NavMeshAgent skeletonArcherNav = skeletonArcher.gameObject.GetComponent<NavMeshAgent>();
        //skeletonArcherNav.isStopped = true;
        //skeletonArcher.skeletonArcherObject.transform.LookAt(skeletonArcher.playerObject.transform.position);
        float distanceToPlayer=Vector3.Distance(skeletonArcher.skeletonArcherObject.transform.position,skeletonArcher.playerObject.transform.position);

        //skeletonArcher.skeletonArcherObject.transform.position = Vector3.Slerp(skeletonArcher.skeletonArcherObject.transform.position, skeletonArcher.playerObject.transform.position, 2 * Time.deltaTime);

        //skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Attack();
        /*
        if (distanceToPlayer>2){
            warriorFarPlayer=true;
        }else{
            warriorFarPlayer=false;
        }
        */

        if (playerFar())
        {
            nextState = new SkeletonArcherFollow(skeletonArcher);
            actualPhase = EVENTS.EXIT;
        }

        if (skeletonArcher.goToIdle)
        {
            nextState = new SkeletonArcherIdle(skeletonArcher);
            actualPhase = EVENTS.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public bool playerFar(){
        if(skeletonArcher.warriorAttackFinish==true)
        {
            return true;
        }else{
            return false;
        }
    }   
}