using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMageIdle : SkeletonMageStates
{
    bool playerNearEnemy = false;
    float waitTime;

    public SkeletonMageIdle(SkeletonMage _skeletonMage) : base()
    {
        name = STATES.IDLE;
        skeletonMage = _skeletonMage;
        iniateVariables(skeletonMage);
    }

    public override void Entry()
    {
        base.Entry();
        skeletonMage.skeletonMageAgent.isStopped = true;
        skeletonMage.goToIdle = false;
        waitTime = Random.Range(3f, 6f);
        skeletonMage.StartCoroutine(WaitAndPatrol());

        if (!skeletonMage.damaged)
            skeletonMage.skeletonMageObject.GetComponent<SkeletonMageAnimation>().Idle();
    }

    public override void Updating()
    {
        Debug.Log("IDLE");
        float distanceToPlayer = Vector3.Distance(skeletonMage.skeletonMageObject.transform.position, skeletonMage.playerObject.transform.position);

        //skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Idle();

        if (distanceToPlayer <= skeletonMage.stats.detectionDistance)
        {
            NavMeshPath path = new NavMeshPath();
            if (skeletonMage.skeletonMageAgent.CalculatePath(skeletonMage.playerObject.transform.position, path) &&
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
            nextState = new SkeletonMageFollow(skeletonMage);
            actualPhase = EVENTS.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
        //skeletonArcher.skeletonArcherAnimator.SetBool("Idle", false);
    }

    IEnumerator WaitAndPatrol()
    {
        yield return new WaitForSeconds(waitTime);
        if (!skeletonMage.lookingAtPlayer)
        {
            nextState = new SkeletonMagePatrol(skeletonMage);
            actualPhase = EVENTS.EXIT;
        }
    }
}
