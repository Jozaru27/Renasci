using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarriorFollow : SkeletonWarriorStates
{
    
    bool warriorNearPlayer=false;
    public SkeletonWarriorFollow(SkeletonWarrior _skeletonWarrior) : base()
    {
        //Debug.Log("FOLLOWING");
        name = STATES.FOLLOW;
        skeletonWarrior=_skeletonWarrior;
        iniateVariables(skeletonWarrior);
    }

    public override void Entry()
    {
        //NavMeshAgent skeletonWarriorNav = skeletonWarrior.gameObject.GetComponent<NavMeshAgent>();
        //skeletonWarriorNav.isStopped = false;

        if (!skeletonWarrior.dead)
        {
            skeletonWarrior.skeletonWarriorAgent.isStopped = false;
            skeletonWarrior.startBlock = false;
            skeletonWarrior.warriorAttackFinish = false;
        }

        skeletonWarrior.isBlocking = false;

        base.Entry();
    }

    public override void Updating()
    {
        //NavMeshAgent skeletonWarriorNav=skeletonWarrior.skeletonWarriorObject.GetComponent<NavMeshAgent>();
        
        float distanceToPlayer=Vector3.Distance(skeletonWarrior.skeletonWarriorObject.transform.position,skeletonWarrior.playerObject.transform.position);

        NavMeshPath path = new NavMeshPath();
        bool pathExists = skeletonWarrior.skeletonWarriorAgent.CalculatePath(skeletonWarrior.playerObject.transform.position, path) && path.status == NavMeshPathStatus.PathComplete;

        if (!pathExists || distanceToPlayer >= skeletonWarrior.stats.detectionDistance)
        {
            nextState = new SkeletonWarriorIdle(skeletonWarrior);
            actualPhase = EVENTS.EXIT;
            return;
        }

        //skeletonWarrior.skeletonWarriorAgent.destination=skeletonWarrior.playerObject.transform.position;
        skeletonWarrior.skeletonWarriorAgent.destination = skeletonWarrior.playerObject.transform.position;

        skeletonWarrior.skeletonWarriorObject.GetComponent<SkeletonWarriorAnimation>().Run();

        if (distanceToPlayer <= skeletonWarrior.stats.detectionDistance - 4)
        {
            warriorNearPlayer = true;
        }
        else
        {
            warriorNearPlayer = false;
        }

        if (warriorNearPlayer)
        {
            nextState = new SkeletonWarriorBlock(skeletonWarrior);
            actualPhase = EVENTS.EXIT;
        }

        if (skeletonWarrior.goToIdle)
        {
            nextState = new SkeletonWarriorIdle(skeletonWarrior);
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
