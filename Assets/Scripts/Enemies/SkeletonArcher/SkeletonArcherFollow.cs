using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonArcherFollow : SkeletonArcherStates
{
    
    bool warriorNearPlayer=false;
    public SkeletonArcherFollow(SkeletonArcher _skeletonArcher) : base()
    {
        //Debug.Log("FOLLOWING");
        name = STATES.FOLLOW;
        skeletonArcher=_skeletonArcher;
        iniateVariables(skeletonArcher);
    }

    public override void Entry()
    {
        //NavMeshAgent skeletonArcherNav = skeletonArcher.gameObject.GetComponent<NavMeshAgent>();
        //skeletonArcherNav.isStopped = false;
        skeletonArcher.skeletonArcherAgent.isStopped = false;
        skeletonArcher.startBlock = false;
        skeletonArcher.warriorAttackFinish = false;
        base.Entry();
    }

    public override void Updating()
    {
        //NavMeshAgent skeletonArcherNav=skeletonArcher.skeletonArcherObject.GetComponent<NavMeshAgent>();
        
        float distanceToPlayer=Vector3.Distance(skeletonArcher.skeletonArcherObject.transform.position,skeletonArcher.playerObject.transform.position);

        NavMeshPath path = new NavMeshPath();
        bool pathExists = skeletonArcher.skeletonArcherAgent.CalculatePath(skeletonArcher.playerObject.transform.position, path) && path.status == NavMeshPathStatus.PathComplete;

        if (!pathExists || distanceToPlayer >= skeletonArcher.stats.detectionDistance)
        {
            nextState = new SkeletonArcherIdle(skeletonArcher);
            actualPhase = EVENTS.EXIT;
            return;
        }

        //skeletonArcher.skeletonArcherAgent.destination=skeletonArcher.playerObject.transform.position;
        skeletonArcher.skeletonArcherAgent.destination = skeletonArcher.playerObject.transform.position;

        //skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Run();

        if (distanceToPlayer <= skeletonArcher.stats.detectionDistance - 4)
        {
            warriorNearPlayer = true;
        }
        else
        {
            warriorNearPlayer = false;
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

    //public bool BeginAttack(){
    //if(warriorNearPlayer==true){
    //    return true;
    //}else{
    //    return false;
    //}
    //}
}
