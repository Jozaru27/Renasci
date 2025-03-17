using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonWarriorFollow : SkeletonWarriorStates
{
    public SkeletonWarriorFollow(SkeletonWarrior skeletonWarrior) : base()
    {
        name = STATES.FOLLOW;
        iniateVariables(skeletonWarrior);
    }

    public override void Entry()
    {
        base.Entry();
    }

    public override void Updating()
    {
        if (PlayerIsNear())
        {
            nextState = new SkeletonWarriorAttack(skeletonWarrior);
            actualPhase = EVENTS.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public bool PlayerIsNear()
    {
        return false;
    }
}
