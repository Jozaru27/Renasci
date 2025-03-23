using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        base.Entry();
    }

    public override void Updating()
    {
        skeletonWarrior.skeletonWarriorObject.transform.LookAt(skeletonWarrior.playerObject.transform.position);

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
        if (blockStop == true)
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
