using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMageAttack : SkeletonMageStates
{
    float attackRandomizer;
    float basicAttackProbability;

    public SkeletonMageAttack(SkeletonMage _skeletonMage) : base()
    {
        name = STATES.ATTACK;
        skeletonMage = _skeletonMage;
        iniateVariables(skeletonMage);
    }

    public override void Entry()
    {
        skeletonMage.agent.isStopped = true;

        //if (!skeletonMage.teleporting)
        //{
        //    if (attackRandomizer <= basicAttackProbability)
        //        skeletonMage.skeletonMageObject.GetComponent<SkeletonMageAnimation>().Attack();

        //    skeletonMage.attacking = true;
        //}

        base.Entry();
    }

    public override void Updating()
    {
        //Debug.Log("ATTACK");

        if (!skeletonMage.frozen && !skeletonMage.goToIdle)
        {
            RotateEnemy();

            NavMeshPath path = new NavMeshPath();
            bool pathExists = false;

            float distanceToPlayer = Vector3.Distance(skeletonMage.transform.position, skeletonMage.playerObj.transform.position);

            if (skeletonMage.agent.CalculatePath(skeletonMage.playerObj.transform.position, path) && path.status == NavMeshPathStatus.PathComplete)
                pathExists = true;

            if (!skeletonMage.attacking && !skeletonMage.damaged)
            {
                if (distanceToPlayer <= skeletonMage.stats.detectionDistance && distanceToPlayer > 5f && pathExists && skeletonMage.lookingAtPlayer)
                    PlayerDetected();
                else if (distanceToPlayer < 5f)
                    Teleport();
            }
            if (distanceToPlayer > skeletonMage.stats.detectionDistance)
                PlayerUndetected();

            //if (!skeletonMage.attacking && !skeletonMage.damaged)
            //{
            //    if (!skeletonMage.attacking && !skeletonMage.damaged)
            //        if (distanceToPlayer <= skeletonMage.stats.detectionDistance && distanceToPlayer > 5f && pathExists && skeletonMage.lookingAtPlayer)
            //            PlayerDetected();
            //        else if (distanceToPlayer < 5f)
            //            Teleport();
            //        else
            //            PlayerUndetected();
            //}
        }
        if (skeletonMage.dead || skeletonMage.goToIdle)
            ReturnToIdle();

        //if (!skeletonMage.frozen)
        //{
        //    if (!skeletonMage.teleporting)
        //    {
        //        

        //        

        //        if ((Quaternion.Angle(skeletonMage.skeletonMageObject.transform.rotation, playerRotation) <= 15f || skeletonMage.damaged) && !skeletonMage.secondAttack)
        //        {
        //            skeletonMage.skeletonMageObject.transform.rotation = playerRotation;

        //            if (attackRandomizer > basicAttackProbability)
        //            {
        //                //skeletonMage.StartCoroutine(skeletonMage.MakingSecondAttack());
        //                skeletonMage.secondAttack = true;
        //                skeletonMage.GetComponent<SkeletonMageAnimation>().SecondAttack();
        //            }
        //        }

        //        if (attackRandomizer <= basicAttackProbability && !skeletonMage.attacking)
        //        {
        //            if (skeletonMage.goToIdle && !skeletonMage.teleporting)
        //            {
        //                //Debug.Log("ESO");
        //                nextState = new SkeletonMageIdle(skeletonMage);
        //                actualPhase = EVENTS.EXIT;
        //            }
        //        }
        //        if (!skeletonMage.attacking && skeletonMage.goToIdle && !skeletonMage.teleporting)
        //        {
        //            //Debug.Log("AQUELLO");
        //            nextState = new SkeletonMageIdle(skeletonMage);
        //            actualPhase = EVENTS.EXIT;
        //        }
        //    }
        //    else
        //    {
        //        //Debug.Log("ESTO TAMPOCO");
        //        skeletonMage.attacking = false;
        //        nextState = new SkeletonMageFollow(skeletonMage);
        //        actualPhase = EVENTS.EXIT;
        //        return;
        //    }
        //}
    }

    void PlayerDetected()
    {
        attackRandomizer = Random.Range(0f, 1f);
        basicAttackProbability = 0.66f;

        if (attackRandomizer <= basicAttackProbability)
            BasicAttack();
        else
            SecondAttack();

        skeletonMage.attacking = true;
    }

    void Teleport()
    {
        nextState = new SkeletonMageFollow(skeletonMage);
        actualPhase = EVENTS.EXIT;
    }

    void PlayerUndetected()
    {
        skeletonMage.StopAttack();
    }

    void BasicAttack()
    {
        skeletonMage.GetComponent<SkeletonMageAnimation>().Attack();
        skeletonMage.GetComponent<SkeletonMage>().PlayAttackOrb();
    }

    void SecondAttack()
    {
        skeletonMage.GetComponent<SkeletonMageAnimation>().SecondAttack();
        skeletonMage.GetComponent<SkeletonMage>().PlayAttackRay();
        skeletonMage.InitiateRay();
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

        if (!skeletonMage.attacking)
        {
            //if (!skeletonMage.secondAttack)
            skeletonMage.transform.rotation = Quaternion.Lerp(skeletonMage.transform.rotation, playerRotation, 5 * Time.deltaTime);

            if (Quaternion.Angle(skeletonMage.transform.rotation, playerRotation) <= 15f)
                skeletonMage.transform.rotation = playerRotation;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
