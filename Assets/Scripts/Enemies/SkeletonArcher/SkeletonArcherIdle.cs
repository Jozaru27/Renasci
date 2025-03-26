using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonArcherIdle : SkeletonArcherStates
{

    bool playerNearEnemy = false;
    float waitTime;

    public SkeletonArcherIdle(SkeletonArcher _skeletonArcher) : base()
    {
        //Debug.Log("IDLING");
        name = STATES.IDLE;
        skeletonArcher = _skeletonArcher;
        iniateVariables(skeletonArcher);
    }

    public override void Entry()
    {
        base.Entry();
        skeletonArcher.skeletonArcherAgent.isStopped = true;
        skeletonArcher.goToIdle = false;
        skeletonArcher.skeletonArcherAnimator.SetBool("Idle", true);
        waitTime = Random.Range(1f, 10f);
        skeletonArcher.StartCoroutine(WaitAndPatrol());
    }

    public override void Updating()
    {
        float distanceToPlayer = Vector3.Distance(skeletonArcher.skeletonArcherObject.transform.position, skeletonArcher.playerObject.transform.position);

        skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Idle();

        if (distanceToPlayer <= skeletonArcher.stats.detectionDistance)
        {
            NavMeshPath path = new NavMeshPath();
            if (skeletonArcher.skeletonArcherAgent.CalculatePath(skeletonArcher.playerObject.transform.position, path) &&
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
            nextState = new SkeletonArcherFollow(skeletonArcher);
            actualPhase = EVENTS.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
        skeletonArcher.skeletonArcherAnimator.SetBool("Idle", false);
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
        if (!skeletonArcher.lookingAtPlayer)
        {
            nextState = new SkeletonArcherPatrol(skeletonArcher);
            actualPhase = EVENTS.EXIT;
        }
    }
}
