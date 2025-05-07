using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonArcherAttack : SkeletonArcherStates
{

    public SkeletonArcherAttack(SkeletonArcher _skeletonArcher) : base()
    {
        name = STATES.ATTACK;
        skeletonArcher=_skeletonArcher;
        iniateVariables(skeletonArcher);
    }

    public override void Entry()
    {
        skeletonArcher.skeletonArcherAgent.isStopped = true;
        skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Attack();
        base.Entry();
    }

    public override void Updating()
    {
        if (!skeletonArcher.frozen)
        {
            Vector3 playerDirection = skeletonArcher.playerObject.transform.position - skeletonArcher.skeletonArcherObject.transform.position;
            Quaternion playerRotation = Quaternion.LookRotation(playerDirection.normalized);

            skeletonArcher.skeletonArcherObject.transform.rotation = Quaternion.Lerp(skeletonArcher.skeletonArcherObject.transform.rotation, playerRotation, 5 * Time.deltaTime);

            if (Quaternion.Angle(skeletonArcher.skeletonArcherObject.transform.rotation, playerRotation) <= 15f && skeletonArcher.damaged)
                skeletonArcher.skeletonArcherObject.transform.rotation = playerRotation;

            float distanceToPlayer = Vector3.Distance(skeletonArcher.skeletonArcherObject.transform.position, skeletonArcher.playerObject.transform.position);

            if (distanceToPlayer > 7f)
            {
                nextState = new SkeletonArcherFollow(skeletonArcher);
                actualPhase = EVENTS.EXIT;
            }

            //if (distanceToPlayer < 5f)
            //{
            //    nextState = new SkeletonArcherFollow(skeletonArcher);
            //    actualPhase = EVENTS.EXIT;
            //    return;
            //}

            if (skeletonArcher.goToIdle)
            {
                nextState = new SkeletonArcherIdle(skeletonArcher);
                actualPhase = EVENTS.EXIT;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}