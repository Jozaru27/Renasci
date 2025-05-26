using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMageIdle : SkeletonMageStates
{
    float waitTime;

    public SkeletonMageIdle(SkeletonMage _skeletonMage) : base()
    {
        name = STATES.IDLE;
        skeletonMage = _skeletonMage;
        iniateVariables(skeletonMage);
    }

    public override void Entry()
    {
        skeletonMage.agent.isStopped = true;
        skeletonMage.goToIdle = false;

        if (!skeletonMage.dead)
        {
            waitTime = Random.Range(3f, 6f);
            skeletonMage.StartCoroutine(WaitAndPatrol());

            if (!skeletonMage.damaged)
                skeletonMage.GetComponent<SkeletonMageAnimation>().Idle();
        }

        base.Entry();
    }

    public override void Updating()
    {
        //Debug.Log("IDLE");

        if (!skeletonMage.frozen && !skeletonMage.dead)
        {
            NavMeshPath path = new NavMeshPath();
            bool pathExists = false;

            float distanceToPlayer = Vector3.Distance(skeletonMage.transform.position, skeletonMage.playerObj.transform.position);

            if (skeletonMage.agent.CalculatePath(skeletonMage.playerObj.transform.position, path) && path.status == NavMeshPathStatus.PathComplete)
                pathExists = true;

            if (distanceToPlayer <= skeletonMage.stats.detectionDistance && pathExists)
                PlayerDetected();
        }
    }

    public override void Exit()
    {
        skeletonMage.StopAllCoroutines();
        base.Exit();
    }

    void PlayerDetected()
    {
        nextState = new SkeletonMageFollow(skeletonMage);
        actualPhase = EVENTS.EXIT;
    }

    IEnumerator WaitAndPatrol()
    {
        yield return new WaitForSeconds(waitTime);

        nextState = new SkeletonMagePatrol(skeletonMage);
        actualPhase = EVENTS.EXIT;
    }
}
