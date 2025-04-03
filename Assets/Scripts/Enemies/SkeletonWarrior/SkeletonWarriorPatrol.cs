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

        if (!skeletonWarrior.dead)
        {
            skeletonWarrior.skeletonWarriorAgent.isStopped = false;
            skeletonWarrior.skeletonWarriorAnimator.SetBool("Run", true);
            skeletonWarrior.skeletonWarriorAgent.speed *= 0.5f;
            skeletonWarrior.skeletonWarriorAnimator.speed = 0.5f;
            SetPatrolDestination();
        }

        skeletonWarrior.isBlocking = false;
    }

    public override void Updating()
    {
        float distanceToPlayer = Vector3.Distance(skeletonWarrior.skeletonWarriorObject.transform.position, skeletonWarrior.playerObject.transform.position);

        if (distanceToPlayer <= skeletonWarrior.stats.detectionDistance)
        {
            NavMeshPath path = new NavMeshPath();
            if (skeletonWarrior.skeletonWarriorAgent.CalculatePath(skeletonWarrior.playerObject.transform.position, path) &&
                path.status == NavMeshPathStatus.PathComplete)
            {
                playerNearEnemy = true;
            }
            else
            {
                playerNearEnemy = false;
            }
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
        //int maxAttempts = 20;
        float randomDistance = Random.Range(5, 10);
        Vector3 bestPoint = skeletonWarrior.transform.position;

        //for (int i = 0; i < maxAttempts; i++)
        //{
            Vector3 randomDirection = Random.insideUnitSphere * randomDistance;
            randomDirection.y = 0;
            Vector3 randomPoint = skeletonWarrior.transform.position + randomDirection;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                NavMeshPath path = new NavMeshPath();
                if (skeletonWarrior.skeletonWarriorAgent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete)
                {
                    bestPoint = hit.position;
                    //break;
                }
            }
        //}

        if (bestPoint != skeletonWarrior.transform.position)
        {
            skeletonWarrior.skeletonWarriorAgent.SetDestination(bestPoint);
        }
    }
}
