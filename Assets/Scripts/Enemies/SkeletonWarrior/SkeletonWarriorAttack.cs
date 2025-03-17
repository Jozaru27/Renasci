using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarriorAttack : SkeletonWarriorStates
{
    public SkeletonWarriorAttack(SkeletonWarrior _skeletonWarrior) : base()
    {
        Debug.Log("ATTACKING");
        name = STATES.ATTACK;
    }

    public override void Entry()
    {
        base.Entry();
    }

    public override void Updating()
    {
        if(blocking())
        {
            nextState=new SkeletonWarriorBlock();
            actualPhase=EVENTS.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
    public bool blocking(){
        return false;
    }
}
