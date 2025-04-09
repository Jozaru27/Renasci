using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMageAttack : SkeletonMageStates
{
    float attackRandomizer;

    float basicAttackProbability;

    bool rotationTaken = false;

    Quaternion startRotation = Quaternion.identity;

    public SkeletonMageAttack(SkeletonMage _skeletonMage) : base()
    {
        name = STATES.ATTACK;
        skeletonMage = _skeletonMage;
        iniateVariables(skeletonMage);
    }

    public override void Entry()
    {
        skeletonMage.skeletonMageAgent.isStopped = true;

        attackRandomizer = Random.Range(0, 1);

        basicAttackProbability = -1;

        if (!skeletonMage.teleporting)
        {
            if (attackRandomizer <= basicAttackProbability)
                skeletonMage.skeletonMageObject.GetComponent<SkeletonMageAnimation>().Attack();
            else
                skeletonMage.skeletonMageObject.GetComponent<SkeletonMageAnimation>().SecondAttack();

            skeletonMage.attacking = true;
        }
        base.Entry();
    }

    public override void Updating()
    {
        //Debug.Log("ATTACK");

        Vector3 playerDirection = skeletonMage.playerObject.transform.position - skeletonMage.skeletonMageObject.transform.position;
        Quaternion playerRotation = Quaternion.LookRotation(playerDirection.normalized);

        float rotationSpeed = 0;

        if (attackRandomizer <= basicAttackProbability)
            rotationSpeed = 5f;

        else
            rotationSpeed = 1.5f;


        if (attackRandomizer <= basicAttackProbability)
        {
            skeletonMage.skeletonMageObject.transform.rotation = Quaternion.Lerp(skeletonMage.skeletonMageObject.transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(skeletonMage.skeletonMageObject.transform.rotation, playerRotation) <= 15f && skeletonMage.damaged)
                skeletonMage.skeletonMageObject.transform.rotation = playerRotation;
        }
        else
        {
            Vector3 rotation = Vector3.zero;

            rotation = playerRotation.eulerAngles;

            Debug.Log(playerRotation);
            Debug.Log(rotation);

            //.skeletonMageObject.transform.eulerAngles = Vector3.MoveTowards(skeletonMage.skeletonMageObject.transform.eulerAngles, rotation, 1.5f * Time.deltaTime);

            skeletonMageObj
            //skeletonMage.skeletonMageObject.transform.rotation = Quaternion.Euler(rotation);
        }

        float distanceToPlayer = Vector3.Distance(skeletonMage.skeletonMageObject.transform.position, skeletonMage.playerObject.transform.position);

        NavMeshPath path = new NavMeshPath();
        bool pathExists = skeletonMage.skeletonMageAgent.CalculatePath(skeletonMage.playerObject.transform.position, path) && path.status == NavMeshPathStatus.PathComplete;

        if (attackRandomizer <= basicAttackProbability)
        {
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
        else
        {
            Ray attackRay = new Ray(skeletonMage.secondFirePoint.transform.position, skeletonMage.transform.forward);
            Debug.DrawRay(skeletonMage.secondFirePoint.transform.position, skeletonMage.transform.forward * 100, Color.red);

            if (Physics.Raycast(attackRay, out RaycastHit hit, Mathf.Infinity, skeletonMage.secondAttackMask))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("ASDASDASD");
                }
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
