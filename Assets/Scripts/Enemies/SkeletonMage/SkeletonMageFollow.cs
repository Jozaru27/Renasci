using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMageFollow : SkeletonMageStates
{
    bool gotTP;

    public SkeletonMageFollow(SkeletonMage _skeletonMage) : base()
    {
        name = STATES.FOLLOW;
        skeletonMage = _skeletonMage;
        iniateVariables(skeletonMage);
    }

    public override void Entry()
    {
        if (!skeletonMage.dead)
        {
            skeletonMage.skeletonMageAgent.isStopped = false;
            skeletonMage.archerAttackFinish = false;
            gotTP = false;
        }
        base.Entry();
    }

    public override void Updating()
    {
        Debug.Log("FOLLOW");
        float distanceToPlayer = Vector3.Distance(skeletonMage.skeletonMageObject.transform.position, skeletonMage.playerObject.transform.position);

        NavMeshPath path = new NavMeshPath();
        bool pathExists = skeletonMage.skeletonMageAgent.CalculatePath(skeletonMage.playerObject.transform.position, path) && path.status == NavMeshPathStatus.PathComplete;

        if (!pathExists || distanceToPlayer >= 10f)
        {
            nextState = new SkeletonMageIdle(skeletonMage);
            actualPhase = EVENTS.EXIT;
            return;
        }

        if (distanceToPlayer < 5f && !gotTP)
        {
            skeletonMage.isRepositioning = true;
            skeletonMage.skeletonMageAgent.isStopped = true;
            //skeletonMage.skeletonMageObject.GetComponent<SkeletonMageAnimation>().Idle();
            gotTP = true;
            skeletonMage.StartCoroutine(skeletonMage.Teleporting(1f));
        }
        else if (distanceToPlayer >= 5f)
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
    }
}
