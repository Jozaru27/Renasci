using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarriorPatrol : SkeletonWarriorStates
{
    bool playerNearEnemy = false;
    
    public SkeletonWarriorPatrol(SkeletonWarrior _skeletonWarrior)
    {
        skeletonWarrior = _skeletonWarrior;
        name = STATES.PATROL;
    }

    public override void Entry()
    {
        base.Entry();
        skeletonWarrior.skeletonWarriorAgent.isStopped = false;
        skeletonWarrior.skeletonWarriorAnimator.SetBool("Run", true);
        skeletonWarrior.skeletonWarriorAgent.speed *= 0.5f;
        skeletonWarrior.skeletonWarriorAnimator.speed = 0.5f;
        SetPatrolDestination();
    }

    public override void Updating()
    {
        if (Vector3.Distance(skeletonWarrior.skeletonWarriorObject.transform.position, skeletonWarrior.playerObject.transform.position) <= skeletonWarrior.stats.detectionDistance)
        {
            playerNearEnemy = true;
        }
        else
        {
            playerNearEnemy = false;
        }

        if (playerNearEnemy) 
        {
            nextState = new SkeletonWarriorFollow(skeletonWarrior);
            actualPhase = EVENTS.EXIT;
            return;
        }

        // Destination / Stuck Checker [! A Revisar]
        if (!skeletonWarrior.skeletonWarriorAgent.pathPending &&
            skeletonWarrior.skeletonWarriorAgent.remainingDistance <= skeletonWarrior.skeletonWarriorAgent.stoppingDistance)
        {
            nextState = new SkeletonWarriorIdle(skeletonWarrior);
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
        skeletonWarrior.skeletonWarriorAnimator.SetBool("Run", false);
        skeletonWarrior.skeletonWarriorAgent.speed *= 2f;
        skeletonWarrior.skeletonWarriorAnimator.speed = 1f;
    }

    void SetPatrolDestination()
    {
        int randomDistance = Random.Range(5, 10);
        Vector3 randomPoint = skeletonWarrior.transform.position + Random.insideUnitSphere * randomDistance;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 5f, NavMesh.AllAreas))
        {
            skeletonWarrior.skeletonWarriorAgent.SetDestination(hit.position);
        }
    }
}
