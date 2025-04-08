using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMageAttack : SkeletonMageStates
{
    public SkeletonMageAttack(SkeletonMage _skeletonMage) : base()
    {
        name = STATES.ATTACK;
        skeletonMage = _skeletonMage;
        iniateVariables(skeletonMage);
    }

    public override void Entry()
    {
        skeletonMage.skeletonMageAgent.isStopped = true;
        skeletonMage.skeletonMageObject.GetComponent<SkeletonMageAnimation>().Attack();
        base.Entry();
    }

    public override void Updating()
    {
        //Debug.Log("ATTACK");
        Vector3 playerDirection = skeletonMage.playerObject.transform.position - skeletonMage.skeletonMageObject.transform.position;
        Quaternion playerRotation = Quaternion.LookRotation(playerDirection.normalized);

        skeletonMage.skeletonMageObject.transform.rotation = Quaternion.Lerp(skeletonMage.skeletonMageObject.transform.rotation, playerRotation, 5 * Time.deltaTime);

        if (Quaternion.Angle(skeletonMage.skeletonMageObject.transform.rotation, playerRotation) <= 15f && skeletonMage.damaged)
            skeletonMage.skeletonMageObject.transform.rotation = playerRotation;

        float distanceToPlayer = Vector3.Distance(skeletonMage.skeletonMageObject.transform.position, skeletonMage.playerObject.transform.position);

        NavMeshPath path = new NavMeshPath();
        bool pathExists = skeletonMage.skeletonMageAgent.CalculatePath(skeletonMage.playerObject.transform.position, path) && path.status == NavMeshPathStatus.PathComplete;

        if (distanceToPlayer > skeletonMage.stats.detectionDistance)
        {
            nextState = new SkeletonMageIdle(skeletonMage);
            actualPhase = EVENTS.EXIT;
        }

        if (distanceToPlayer < 5f && pathExists)
        {
            nextState = new SkeletonMageFollow(skeletonMage);
            actualPhase = EVENTS.EXIT;
            return;
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
