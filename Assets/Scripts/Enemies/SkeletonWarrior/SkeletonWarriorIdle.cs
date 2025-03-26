using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarriorIdle : SkeletonWarriorStates
{

    bool playerNearEnemy = false;
    float waitTime;

    public SkeletonWarriorIdle(SkeletonWarrior _skeletonWarrior) : base()
    {
        //Debug.Log("IDLING");
        name = STATES.IDLE;
        skeletonWarrior = _skeletonWarrior;
        iniateVariables(skeletonWarrior);
    }

    public override void Entry()
    {
        base.Entry();
        skeletonWarrior.skeletonWarriorAgent.isStopped = true;
        skeletonWarrior.goToIdle = false;
        skeletonWarrior.skeletonWarriorAnimator.SetBool("Idle", true);
        waitTime = Random.Range(1f, 10f);
        skeletonWarrior.StartCoroutine(WaitAndPatrol());
    }

    public override void Updating()
    {
        float distanceToPlayer = Vector3.Distance(skeletonWarrior.skeletonWarriorObject.transform.position, skeletonWarrior.playerObject.transform.position);

        skeletonWarrior.skeletonWarriorObject.GetComponent<SkeletonWarriorAnimation>().Idle();

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
        }
    }

    public override void Exit()
    {
        base.Exit();
        skeletonWarrior.skeletonWarriorAnimator.SetBool("Idle", false);
    }

    public bool playerNear()
    {
        if (playerNearEnemy == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator WaitAndPatrol()
    {
        yield return new WaitForSeconds(waitTime);
        if (!skeletonWarrior.lookingAtPlayer)
        {
            nextState = new SkeletonWarriorPatrol(skeletonWarrior);
            actualPhase = EVENTS.EXIT;
        }
    }
}
