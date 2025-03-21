using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorIdle : SkeletonWarriorStates
{

    bool playerNearEnemy = false;
    public SkeletonWarriorIdle(SkeletonWarrior _skeletonWarrior) : base()
    {
        Debug.Log("IDLING");
        name = STATES.IDLE;
        skeletonWarrior = _skeletonWarrior;
        iniateVariables(skeletonWarrior);
    }

    public override void Entry()
    {

        base.Entry();
    }

    public override void Updating()
    {
        float distanceToPlayer = Vector3.Distance(skeletonWarrior.skeletonWarriorObject.transform.position, skeletonWarrior.playerObject.transform.position);

        skeletonWarrior.skeletonWarriorObject.GetComponent<SkeletonWarriorAnimation>().Idle();
        if (distanceToPlayer <= 7)
        {
            playerNearEnemy = true;
        }
        else
        {
            playerNearEnemy = false;
        }
        if (playerNear())
        {
            nextState = new SkeletonWarriorFollow(skeletonWarrior);
            actualPhase = EVENTS.EXIT;
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

    public bool playerNear()
    {
        if (playerNearEnemy == true)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
