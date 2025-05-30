using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMageFollow : SkeletonMageStates
{
    public SkeletonMageFollow(SkeletonMage _skeletonMage) : base()
    {
        name = STATES.FOLLOW;
        skeletonMage = _skeletonMage;
        iniateVariables(skeletonMage);
    }

    public override void Entry()
    {
        skeletonMage.agent.isStopped = true;

        //if (!skeletonMage.dead)
        //{
        //    
        //    skeletonMage.mageAttackFinish = false;

        //    //skeletonMage.hasTeleported = false; // JOSE: HE AÑADIDO EL BOOL DE HAS TELEPORTED
        //}
        base.Entry();
    }

    public override void Updating()
    {
        //Debug.Log("FOLLOW");

        if (!skeletonMage.frozen && !skeletonMage.teleporting && !skeletonMage.goToIdle)
        {
            NavMeshPath path = new NavMeshPath();
            bool pathExists = false;

            if (skeletonMage.agent.CalculatePath(skeletonMage.playerObj.transform.position, path) && path.status == NavMeshPathStatus.PathComplete)
                pathExists = true;

            float distanceToPlayer = Vector3.Distance(skeletonMage.transform.position, skeletonMage.playerObj.transform.position);

            if (!skeletonMage.damaged)
            {
                if (distanceToPlayer > skeletonMage.stats.detectionDistance || !pathExists)
                    PlayerUndetected();
                else if (distanceToPlayer < 5f)
                    StartTeleport();
                else
                    AttackPlayer();
            }

            RotateEnemy();
        }
        if (skeletonMage.dead || skeletonMage.goToIdle)
            ReturnToIdle();

        //    if (skeletonMage.goToIdle && !skeletonMage.teleporting)
        //    {
        //        nextState = new SkeletonMageIdle(skeletonMage);
        //        actualPhase = EVENTS.EXIT;
        //    }
    }

    void PlayerUndetected()
    {
        nextState = new SkeletonMageIdle(skeletonMage);
        actualPhase = EVENTS.EXIT;
    }

    void StartTeleport()
    {
        skeletonMage.GetComponent<SkeletonMageAnimation>().Teleport();
        skeletonMage.teleporting = true;
        Debug.Log("HA ENTRADO PA TELEPORT");
    }

    void AttackPlayer()
    {
        nextState = new SkeletonMageAttack(skeletonMage);
        actualPhase = EVENTS.EXIT;
    }

    void ReturnToIdle()
    {
        nextState = new SkeletonMageIdle(skeletonMage);
        actualPhase = EVENTS.EXIT;
    }

    void RotateEnemy()
    {
        Vector3 playerDirection = skeletonMage.playerObj.transform.position - skeletonMage.transform.position;
        Quaternion playerRotation = Quaternion.LookRotation(playerDirection.normalized);

        skeletonMage.transform.rotation = Quaternion.Lerp(skeletonMage.transform.rotation, playerRotation, 5 * Time.deltaTime);

        if (Quaternion.Angle(skeletonMage.transform.rotation, playerRotation) <= 15f || skeletonMage.damaged)
        {
            skeletonMage.transform.rotation = playerRotation;
            skeletonMage.damaged = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
