using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMagePatrol : SkeletonMageStates
{
    bool playerNearEnemy = false;

    public SkeletonMagePatrol(SkeletonMage _skeletonMage)
    {
        skeletonMage = _skeletonMage;
        name = STATES.PATROL;
    }

    public override void Entry()
    {
        base.Entry();
        if (!skeletonMage.dead)
        {
            skeletonMage.skeletonMageAgent.isStopped = false;
            //skeletonArcher.skeletonArcherAnimator.SetBool("Run", true);
            skeletonMage.skeletonMageObject.GetComponent<SkeletonMageAnimation>().Run();
            skeletonMage.skeletonMageAgent.speed *= 0.5f;
            skeletonMage.skeletonMageAnimator.speed = 0.5f;
            SetPatrolDestination();
        }
    }

    public override void Updating()
    {
        //Debug.Log("PATROL");
        float distanceToPlayer = Vector3.Distance(skeletonMage.skeletonMageObject.transform.position, skeletonMage.playerObject.transform.position);

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

        playerNearEnemy = distanceToPlayer <= skeletonMage.stats.detectionDistance && skeletonMage.skeletonMageAgent.CalculatePath(skeletonMage.playerObject.transform.position, new NavMeshPath());

        NavMeshPath path = new NavMeshPath();
        bool pathExists = skeletonMage.skeletonMageAgent.CalculatePath(skeletonMage.playerObject.transform.position, path) && path.status == NavMeshPathStatus.PathComplete;

        if (!skeletonMage.skeletonMageAgent.pathPending &&
            skeletonMage.skeletonMageAgent.remainingDistance <= skeletonMage.skeletonMageAgent.stoppingDistance)
        {
            nextState = new SkeletonMageIdle(skeletonMage);
            actualPhase = EVENTS.EXIT;
        }

        if (Vector3.Distance(skeletonMage.transform.position, skeletonMage.playerObject.transform.position) <= skeletonMage.stats.detectionDistance && pathExists)
        {
            nextState = new SkeletonMageAttack(skeletonMage);
            actualPhase = EVENTS.EXIT;
        }

        if (skeletonMage.goToIdle)
        {
            nextState = new SkeletonMageIdle(skeletonMage);
            actualPhase = EVENTS.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
        //skeletonArcher.skeletonArcherAnimator.SetBool("Run", false);
        skeletonMage.skeletonMageAgent.speed *= 2f;
        skeletonMage.skeletonMageAnimator.speed = 1f;
    }

    void SetPatrolDestination()
    {
        float randomDistance = Random.Range(5, 10);
        Vector3 bestPoint = skeletonMage.transform.position;

        Vector3 randomDirection = Random.insideUnitSphere * randomDistance;
        randomDirection.y = 0;
        Vector3 randomPoint = skeletonMage.transform.position + randomDirection;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
        {
            NavMeshPath path = new NavMeshPath();
            if (skeletonMage.skeletonMageAgent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete)
            {
                bestPoint = hit.position;

            }
        }

        if (bestPoint != skeletonMage.transform.position)
        {
            skeletonMage.skeletonMageAgent.SetDestination(bestPoint);
        }
    }
}
