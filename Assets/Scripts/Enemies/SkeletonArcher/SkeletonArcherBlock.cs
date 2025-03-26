using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
public class SkeletonArcherBlock : SkeletonArcherStates
{
    bool warriorFarPlayer = false;
    bool blockStop = false;

    public SkeletonArcherBlock(SkeletonArcher _skeletonArcher) : base()
    {
        //Debug.Log("BLOCKING");
        name = STATES.BLOCK;
        skeletonArcher=_skeletonArcher;
        iniateVariables(skeletonArcher);
    }

    public override void Entry()
    {
        skeletonArcher.StartCoroutine(GoingToBlock());
        skeletonArcher.isBlocking=true;
        skeletonArcher.skeletonArcherAgent.isStopped = true;
        skeletonArcher.startBlock = false;
        //skeletonArcher.skeletonArcherObject.transform.LookAt(skeletonArcher.playerObject.transform.position);
        base.Entry();
    }

    public override void Updating()
    {
        RaycastHit hit;
        if (Physics.Raycast(skeletonArcher.skeletonArcherObject.transform.position, skeletonArcher.transform.TransformDirection(Vector3.back),out hit,5,skeletonArcher.playerMask))
        {
            skeletonArcher.isBlocking = false;
        }
        else
        {
            skeletonArcher.isBlocking = true;
        }

        //skeletonArcher.skeletonArcherObject.transform.LookAt(skeletonArcher.playerObject.transform.position);
        //NavMeshAgent skeletonArcherNav = skeletonArcher.gameObject.GetComponent<NavMeshAgent>();
        //skeletonArcherNav.isStopped = true;

        if (!skeletonArcher.damaged)
        {
            Vector3 playerDirection = skeletonArcher.playerObject.transform.position - skeletonArcher.skeletonArcherObject.transform.position;
            skeletonArcher.blockTarget = Quaternion.LookRotation(playerDirection.normalized);
            skeletonArcher.angularVelocityOnBlock = 15f;
        }
        else if (!skeletonArcher.playerDirectionTaken)
        {
            Vector3 playerDirection = skeletonArcher.playerObject.transform.position - skeletonArcher.skeletonArcherObject.transform.position;
            skeletonArcher.blockTarget = Quaternion.LookRotation(playerDirection.normalized);
            skeletonArcher.angularVelocityOnBlock = 15f;
            skeletonArcher.playerDirectionTaken = true;
        }

        if (!skeletonArcher.dead)
        {
            skeletonArcher.skeletonArcherObject.transform.rotation = Quaternion.Lerp(skeletonArcher.skeletonArcherObject.transform.rotation, skeletonArcher.blockTarget, skeletonArcher.angularVelocityOnBlock * Time.deltaTime);

            if (Quaternion.Angle(skeletonArcher.skeletonArcherObject.transform.rotation, skeletonArcher.blockTarget) <= 35f && skeletonArcher.damaged)
            {
                skeletonArcher.skeletonArcherObject.transform.rotation = skeletonArcher.blockTarget;
                skeletonArcher.damaged = false;
                skeletonArcher.playerDirectionTaken = false;
            }
        }

        skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Block();

        float distanceToPlayer = Vector3.Distance(skeletonArcher.skeletonArcherObject.transform.position, skeletonArcher.playerObject.transform.position);

        if (distanceToPlayer >= skeletonArcher.stats.detectionDistance-4)
        {
            warriorFarPlayer = true;
        }
        else
        {
            warriorFarPlayer = false;
        }

        if (playerFar())
        {
            nextState = new SkeletonArcherFollow(skeletonArcher);
            actualPhase = EVENTS.EXIT;
        }

        

        if (stopBlocking())
        {
            nextState = new SkeletonArcherAttack(skeletonArcher);
            actualPhase = EVENTS.EXIT;
        }

        if (skeletonArcher.goToIdle)
        {
            nextState = new SkeletonArcherIdle(skeletonArcher);
            actualPhase = EVENTS.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public bool stopBlocking()
    {
        if (blockStop == true && skeletonArcher.lookingAtPlayer==true)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public bool playerFar()
    {
        if (warriorFarPlayer == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator GoingToBlock()
    {
        yield return new WaitForSeconds(1f);
        blockStop = true;
        
    }
}
