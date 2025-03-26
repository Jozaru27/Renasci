using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
public class SkeletonWarriorBlock : SkeletonWarriorStates
{
    bool warriorFarPlayer = false;
    bool blockStop = false;

    public SkeletonWarriorBlock(SkeletonWarrior _skeletonWarrior) : base()
    {
        //Debug.Log("BLOCKING");
        name = STATES.BLOCK;
        skeletonWarrior=_skeletonWarrior;
        iniateVariables(skeletonWarrior);
    }

    public override void Entry()
    {
        skeletonWarrior.StartCoroutine(GoingToBlock());
        skeletonWarrior.isBlocking=true;
        skeletonWarrior.skeletonWarriorAgent.isStopped = true;
        skeletonWarrior.startBlock = false;
        //skeletonWarrior.skeletonWarriorObject.transform.LookAt(skeletonWarrior.playerObject.transform.position);
        base.Entry();
    }

    public override void Updating()
    {
        RaycastHit hit;
        if (Physics.Raycast(skeletonWarrior.skeletonWarriorObject.transform.position, skeletonWarrior.transform.TransformDirection(Vector3.back),out hit,5,skeletonWarrior.playerMask))
        {
            skeletonWarrior.isBlocking = false;
        }
        else
        {
            skeletonWarrior.isBlocking = true;
        }

        //skeletonWarrior.skeletonWarriorObject.transform.LookAt(skeletonWarrior.playerObject.transform.position);
        //NavMeshAgent skeletonWarriorNav = skeletonWarrior.gameObject.GetComponent<NavMeshAgent>();
        //skeletonWarriorNav.isStopped = true;

        if (!skeletonWarrior.damaged)
        {
            Vector3 playerDirection = skeletonWarrior.playerObject.transform.position - skeletonWarrior.skeletonWarriorObject.transform.position;
            skeletonWarrior.blockTarget = Quaternion.LookRotation(playerDirection.normalized);
            skeletonWarrior.angularVelocityOnBlock = 15f;
        }
        else if (!skeletonWarrior.playerDirectionTaken)
        {
            Vector3 playerDirection = skeletonWarrior.playerObject.transform.position - skeletonWarrior.skeletonWarriorObject.transform.position;
            skeletonWarrior.blockTarget = Quaternion.LookRotation(playerDirection.normalized);
            skeletonWarrior.angularVelocityOnBlock = 15f;
            skeletonWarrior.playerDirectionTaken = true;
        }

        if (!skeletonWarrior.dead)
        {
            skeletonWarrior.skeletonWarriorObject.transform.rotation = Quaternion.Lerp(skeletonWarrior.skeletonWarriorObject.transform.rotation, skeletonWarrior.blockTarget, skeletonWarrior.angularVelocityOnBlock * Time.deltaTime);

            if (Quaternion.Angle(skeletonWarrior.skeletonWarriorObject.transform.rotation, skeletonWarrior.blockTarget) <= 35f && skeletonWarrior.damaged)
            {
                skeletonWarrior.skeletonWarriorObject.transform.rotation = skeletonWarrior.blockTarget;
                skeletonWarrior.damaged = false;
                skeletonWarrior.playerDirectionTaken = false;
            }
        }

        skeletonWarrior.skeletonWarriorObject.GetComponent<SkeletonWarriorAnimation>().Block();

        float distanceToPlayer = Vector3.Distance(skeletonWarrior.skeletonWarriorObject.transform.position, skeletonWarrior.playerObject.transform.position);

        if (distanceToPlayer >= skeletonWarrior.stats.detectionDistance-4)
        {
            warriorFarPlayer = true;
        }
        else
        {
            warriorFarPlayer = false;
        }

        if (playerFar())
        {
            nextState = new SkeletonWarriorFollow(skeletonWarrior);
            actualPhase = EVENTS.EXIT;
        }

        

        if (stopBlocking())
        {
            nextState = new SkeletonWarriorAttack(skeletonWarrior);
            actualPhase = EVENTS.EXIT;
        }

        if (skeletonWarrior.goToIdle)
        {
            nextState = new SkeletonWarriorIdle(skeletonWarrior);
            actualPhase = EVENTS.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public bool stopBlocking()
    {
        if (blockStop == true && skeletonWarrior.lookingAtPlayer==true)
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
