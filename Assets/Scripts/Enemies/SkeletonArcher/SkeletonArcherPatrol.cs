using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonArcherPatrol : SkeletonArcherStates
{
    bool playerNearEnemy = false;
    
    public SkeletonArcherPatrol(SkeletonArcher _skeletonArcher)
    {
        skeletonArcher = _skeletonArcher;
        name = STATES.PATROL;
    }

    public override void Entry()
    {
        base.Entry();
        if (!skeletonArcher.dead)
        {
            skeletonArcher.skeletonArcherAgent.isStopped = false;
            //skeletonArcher.skeletonArcherAnimator.SetBool("Run", true);
            skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Run();
            skeletonArcher.skeletonArcherAgent.speed *= 0.5f;
            skeletonArcher.skeletonArcherAnimator.speed = 0.5f;
            SetPatrolDestination();
        }
    }

    public override void Updating()
    {
        float distanceToPlayer = Vector3.Distance(skeletonArcher.skeletonArcherObject.transform.position, skeletonArcher.playerObject.transform.position);

        //if (distanceToPlayer <= skeletonArcher.stats.detectionDistance)
        //{
        //    NavMeshPath path = new NavMeshPath();
        //    if (skeletonArcher.skeletonArcherAgent.CalculatePath(skeletonArcher.playerObject.transform.position, path) &&
        //        path.status == NavMeshPathStatus.PathComplete)
        //    {
        //        playerNearEnemy = true;
        //    }
        //    else
        //    {
        //        playerNearEnemy = false;
        //    }
        //}
        //else
        //{
        //    playerNearEnemy = false;
        //}

        //if (playerNearEnemy) 
        //{
        //    nextState = new SkeletonArcherFollow(skeletonArcher);
        //    actualPhase = EVENTS.EXIT;
        //    return;
        //}

        playerNearEnemy = distanceToPlayer <= skeletonArcher.stats.detectionDistance && skeletonArcher.skeletonArcherAgent.CalculatePath(skeletonArcher.playerObject.transform.position, new NavMeshPath());


        if (!skeletonArcher.skeletonArcherAgent.pathPending &&
            skeletonArcher.skeletonArcherAgent.remainingDistance <= skeletonArcher.skeletonArcherAgent.stoppingDistance)
        {
            nextState = new SkeletonArcherIdle(skeletonArcher);
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
        //skeletonArcher.skeletonArcherAnimator.SetBool("Run", false);
        skeletonArcher.skeletonArcherAgent.speed *= 2f;
        skeletonArcher.skeletonArcherAnimator.speed = 1f;
    }

    void SetPatrolDestination()
    {
        float randomDistance = Random.Range(5, 10);
        Vector3 bestPoint = skeletonArcher.transform.position;

        Vector3 randomDirection = Random.insideUnitSphere * randomDistance;
        randomDirection.y = 0;
        Vector3 randomPoint = skeletonArcher.transform.position + randomDirection;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
        {
            NavMeshPath path = new NavMeshPath();
            if (skeletonArcher.skeletonArcherAgent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete)
            {
                bestPoint = hit.position;
                    
            }
        }

        if (bestPoint != skeletonArcher.transform.position)
        {
            skeletonArcher.skeletonArcherAgent.SetDestination(bestPoint);
        }
    }
}
