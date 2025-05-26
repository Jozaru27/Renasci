using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMagePatrol : SkeletonMageStates
{
   // bool playerNearEnemy = false;

    public SkeletonMagePatrol(SkeletonMage _skeletonMage)
    {
        skeletonMage = _skeletonMage;
        name = STATES.PATROL;
    }

    public override void Entry()
    {
        skeletonMage.agent.isStopped = false;
        skeletonMage.GetComponent<SkeletonMageAnimation>().Run();
        skeletonMage.agent.speed *= 0.5f;
        skeletonMage.anim.speed = 0.5f;

        SetPatrolDestination();

        base.Entry();
    }

    public override void Updating()
    {
        //Debug.Log("PATROL");

        if (!skeletonMage.frozen && !skeletonMage.goToIdle)
        {
            NavMeshPath path = new NavMeshPath();
            bool pathExists = false;

            float distanceToPlayer = Vector3.Distance(skeletonMage.transform.position, skeletonMage.playerObj.transform.position);

            if (skeletonMage.agent.CalculatePath(skeletonMage.playerObj.transform.position, path) && path.status == NavMeshPathStatus.PathComplete)
                pathExists = true;

            if (distanceToPlayer <= skeletonMage.stats.detectionDistance && pathExists)
                PlayerDetected();

            if (!skeletonMage.agent.pathPending && skeletonMage.agent.remainingDistance <= skeletonMage.agent.stoppingDistance)
            {
                nextState = new SkeletonMageIdle(skeletonMage);
                actualPhase = EVENTS.EXIT;
            }
        }
        if (skeletonMage.dead || skeletonMage.goToIdle)
            ReturnToIdle();

        //if (Vector3.Distance(skeletonMage.transform.position, skeletonMage.playerObject.transform.position) <= skeletonMage.stats.detectionDistance && pathExists && !skeletonMage.teleporting)
        //{
        //    nextState = new SkeletonMageAttack(skeletonMage);
        //    actualPhase = EVENTS.EXIT;
        //}

        //if (skeletonMage.goToIdle && !skeletonMage.teleporting)
        //{
        //    nextState = new SkeletonMageIdle(skeletonMage);
        //    actualPhase = EVENTS.EXIT;
        //}
    }

    public override void Exit()
    {
        skeletonMage.agent.speed *= 2f;
        skeletonMage.anim.speed = 1f;
        base.Exit();
    }

    void PlayerDetected()
    {
        nextState = new SkeletonMageFollow(skeletonMage);
        actualPhase = EVENTS.EXIT;
    }

    void ReturnToIdle()
    {
        nextState = new SkeletonMageIdle(skeletonMage);
        actualPhase = EVENTS.EXIT;
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

            if (skeletonMage.agent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete)
                bestPoint = hit.position;
        }

        if (bestPoint != skeletonMage.transform.position)
        {
            skeletonMage.agent.SetDestination(bestPoint);
        }
    }
}
