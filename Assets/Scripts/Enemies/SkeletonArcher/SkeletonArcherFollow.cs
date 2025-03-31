using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonArcherFollow : SkeletonArcherStates
{
    
    bool warriorNearPlayer=false;

    public SkeletonArcherFollow(SkeletonArcher _skeletonArcher) : base()
    {
        name = STATES.FOLLOW;
        skeletonArcher=_skeletonArcher;
        iniateVariables(skeletonArcher);
    }

    public override void Entry()
    {
        if (!skeletonArcher.dead)
        {
            //NavMeshAgent skeletonArcherNav = skeletonArcher.gameObject.GetComponent<NavMeshAgent>();
            //skeletonArcherNav.isStopped = false;
            skeletonArcher.skeletonArcherAgent.isStopped = false;
            skeletonArcher.archerAttackFinish = false;
            skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Run();
        }
        base.Entry();
    }

    public override void Updating()
    {
        //NavMeshAgent skeletonArcherNav=skeletonArcher.skeletonArcherObject.GetComponent<NavMeshAgent>();
        
        float distanceToPlayer=Vector3.Distance(skeletonArcher.skeletonArcherObject.transform.position,skeletonArcher.playerObject.transform.position);

        NavMeshPath path = new NavMeshPath();
        bool pathExists = skeletonArcher.skeletonArcherAgent.CalculatePath(skeletonArcher.playerObject.transform.position, path) && path.status == NavMeshPathStatus.PathComplete;

        if (!pathExists || distanceToPlayer >= 10f)
        {
            nextState = new SkeletonArcherIdle(skeletonArcher);
            actualPhase = EVENTS.EXIT;
            return;
        }

        
        if (distanceToPlayer > 6f)
        {
            skeletonArcher.skeletonArcherAgent.destination = skeletonArcher.playerObject.transform.position;
        }
        else if (distanceToPlayer < 3f) 
        {
            Vector3 dirToPlayer = (skeletonArcher.skeletonArcherObject.transform.position - skeletonArcher.playerObject.transform.position).normalized;
            Vector3 desiredPos = skeletonArcher.skeletonArcherObject.transform.position + dirToPlayer * 3f;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(desiredPos, out hit, 10f, NavMesh.AllAreas))
            {
                skeletonArcher.skeletonArcherAgent.SetDestination(hit.position);
            }
        }

        
        if (distanceToPlayer <= 6.5f && distanceToPlayer >= 3f)
        {
            nextState = new SkeletonArcherAttack(skeletonArcher);
            actualPhase = EVENTS.EXIT;
        }

        //skeletonArcher.skeletonArcherAgent.destination = skeletonArcher.playerObject.transform.position;

        //skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Run();

        //if (distanceToPlayer <= skeletonArcher.stats.detectionDistance - 4)
        //{
        //    warriorNearPlayer = true;
        //}
        //else
        //{
        //    warriorNearPlayer = false;
        //}

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
