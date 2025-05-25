using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (!skeletonMage.dead)
        {
            skeletonMage.skeletonMageAgent.isStopped = false;
            skeletonMage.mageAttackFinish = false;

            //skeletonMage.hasTeleported = false; // JOSE: HE AÑADIDO EL BOOL DE HAS TELEPORTED
        }
        base.Entry();
    }

    public override void Updating()
    {
        //Debug.Log("FOLLOW");

        if (!skeletonMage.frozen)
        {
            Vector3 playerDirection = skeletonMage.playerObject.transform.position - skeletonMage.skeletonMageObject.transform.position;
            Quaternion playerRotation = Quaternion.LookRotation(playerDirection.normalized);

            skeletonMage.skeletonMageObject.transform.rotation = Quaternion.Lerp(skeletonMage.skeletonMageObject.transform.rotation, playerRotation, 5 * Time.deltaTime);

            if (Quaternion.Angle(skeletonMage.skeletonMageObject.transform.rotation, playerRotation) <= 15f && skeletonMage.damaged)
                skeletonMage.skeletonMageObject.transform.rotation = playerRotation;

            //Debug.Log("FOLLOW");
            float distanceToPlayer = Vector3.Distance(skeletonMage.skeletonMageObject.transform.position, skeletonMage.playerObject.transform.position);

            NavMeshPath path = new NavMeshPath();
            bool pathExists = skeletonMage.skeletonMageAgent.CalculatePath(skeletonMage.playerObject.transform.position, path) && path.status == NavMeshPathStatus.PathComplete;

            if (!pathExists || distanceToPlayer >= 10f)
            {
                Debug.Log("HA HECHO ESTO Q ES BASTANTE RARO LA VERDAD");
                nextState = new SkeletonMageIdle(skeletonMage);
                actualPhase = EVENTS.EXIT;
                return;
            }

            if (distanceToPlayer < 5f && !skeletonMage.teleporting && !skeletonMage.attacking) // JOSE: HE AÑADIDO EL COMPROBADOR DE HAS TELEPORTER
            {
                skeletonMage.skeletonMageAgent.isStopped = true;
                //skeletonMage.StartCoroutine(skeletonMage.Teleporting(1f));
                skeletonMage.UseTeleportAnim();
                skeletonMage.teleporting = true;
            }
            else if (distanceToPlayer >= 5f && !skeletonMage.teleporting)
            {
                nextState = new SkeletonMageAttack(skeletonMage);
                actualPhase = EVENTS.EXIT;
            }

            if (skeletonMage.goToIdle && !skeletonMage.teleporting)
            {
                nextState = new SkeletonMageIdle(skeletonMage);
                actualPhase = EVENTS.EXIT;
            }
        }
    }

    public override void Exit()
    {
       // Debug.Log("EXIT");
        base.Exit();
    }
}
