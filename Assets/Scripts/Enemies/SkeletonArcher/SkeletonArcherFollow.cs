using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonArcherFollow : SkeletonArcherStates
{

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
            skeletonArcher.skeletonArcherAgent.isStopped = false;
            skeletonArcher.archerAttackFinish = false;
            skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Run();
        }
        base.Entry();
    }

     public override void Updating()
     {       
        if (!skeletonArcher.frozen)
        {
            float distanceToPlayer = Vector3.Distance(skeletonArcher.skeletonArcherObject.transform.position, skeletonArcher.playerObject.transform.position);

            NavMeshPath path = new NavMeshPath();
            bool pathExists = skeletonArcher.skeletonArcherAgent.CalculatePath(skeletonArcher.playerObject.transform.position, path) && path.status == NavMeshPathStatus.PathComplete;

            if (!pathExists || distanceToPlayer >= 10f)
            {
                nextState = new SkeletonArcherIdle(skeletonArcher);
                actualPhase = EVENTS.EXIT;
                return;
            }


            if (distanceToPlayer > 7f)
            {
                skeletonArcher.skeletonArcherAgent.destination = skeletonArcher.playerObject.transform.position;
            }
            else if (distanceToPlayer < 5f)
            {
                skeletonArcher.isRepositioning = true;
                skeletonArcher.skeletonArcherAgent.isStopped = true;
                skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Idle();
                skeletonArcher.StartCoroutine(skeletonArcher.WaitAndReposition());
            }
            else if (distanceToPlayer <= 7f && distanceToPlayer >= 5f)
            {
                nextState = new SkeletonArcherAttack(skeletonArcher);
                actualPhase = EVENTS.EXIT;
            }

            if (skeletonArcher.goToIdle)
            {
                nextState = new SkeletonArcherIdle(skeletonArcher);
                actualPhase = EVENTS.EXIT;
            }
        }
     }

    public override void Exit()
    {
        base.Exit();
    }
}