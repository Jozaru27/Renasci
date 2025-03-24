using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SkeletonWarriorBlock : SkeletonWarriorStates
{
   
    bool warriorFarPlayer = false;
    bool blockStop = false;

   
    public SkeletonWarriorBlock(SkeletonWarrior _skeletonWarrior) : base()
    {
        Debug.Log("BLOCKING");
        name = STATES.BLOCK;
        skeletonWarrior=_skeletonWarrior;
        iniateVariables(skeletonWarrior);
    }

    public override void Entry()
    {
        skeletonWarrior.StartCoroutine(GoingToBlock());
        skeletonWarrior.isBlocking=true;
        skeletonWarrior.skeletonWarriorAgent.isStopped = true;
        //skeletonWarrior.skeletonWarriorObject.transform.LookAt(skeletonWarrior.playerObject.transform.position);
        base.Entry();
    }

    public override void Updating()
    {
        //skeletonWarrior.skeletonWarriorObject.transform.LookAt(skeletonWarrior.playerObject.transform.position);
        //NavMeshAgent skeletonWarriorNav = skeletonWarrior.gameObject.GetComponent<NavMeshAgent>();
        //skeletonWarriorNav.isStopped = true;
        Vector3 playerDirection = skeletonWarrior.playerObject.transform.position - skeletonWarrior.transform.position;
        Quaternion lookAngle = Quaternion.LookRotation(playerDirection.normalized);
        skeletonWarrior.skeletonWarriorObject.transform.rotation = Quaternion.Slerp(skeletonWarrior.skeletonWarriorObject.transform.rotation, lookAngle, 1.5f * Time.deltaTime);

        skeletonWarrior.skeletonWarriorObject.GetComponent<SkeletonWarriorAnimation>().Block();

        float distanceToPlayer = Vector3.Distance(skeletonWarrior.skeletonWarriorObject.transform.position, skeletonWarrior.playerObject.transform.position);

        if (distanceToPlayer > 2)
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
