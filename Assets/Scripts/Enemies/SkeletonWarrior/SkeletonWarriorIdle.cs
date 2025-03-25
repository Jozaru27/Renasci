using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorIdle : SkeletonWarriorStates
{

    bool playerNearEnemy = false;
    float waitTime; // Wait time before patrolling [EXPERIMENTAL, PATRULLA]

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
        skeletonWarrior.skeletonWarriorAnimator.SetBool("Idle", true); // [EXPERIMENTAL, PATRULLA]
        waitTime = Random.Range(1f, 10f); // [EXPERIMENTAL, PATRULLA]
        skeletonWarrior.StartCoroutine(WaitAndPatrol()); // [EXPERIMENTAL, PATRULLA]
    }

    public override void Updating()
    {
        float distanceToPlayer = Vector3.Distance(skeletonWarrior.skeletonWarriorObject.transform.position, skeletonWarrior.playerObject.transform.position);

        skeletonWarrior.skeletonWarriorObject.GetComponent<SkeletonWarriorAnimation>().Idle();
        if (distanceToPlayer <= skeletonWarrior.stats.detectionDistance)
        {
            playerNearEnemy = true;
        }
        else
        {
            playerNearEnemy = false;
        }
        if (playerNear())
        {
            nextState = new SkeletonWarriorFollow(skeletonWarrior);
            actualPhase = EVENTS.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
        skeletonWarrior.skeletonWarriorAnimator.SetBool("Idle", false); // [EXPERIMENTAL, PATRULLA]
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

    // Patrols if the enemy isn't nearby [EXPERIMENTAL, PATRULLA]
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
