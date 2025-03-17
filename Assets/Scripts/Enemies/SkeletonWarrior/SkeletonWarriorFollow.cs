using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorFollow : SkeletonWarriorStates
{
    public SkeletonWarriorFollow(SkeletonWarrior _skeletonWarrior) : base()
    {
        Debug.Log("FOLLOWING");
        name = STATES.FOLLOW;
    }

    public override void Entry()
    {
        base.Entry();
    }

    public override void Updating()
    {
        if(BeginAttack()){
            nextState=new SkeletonWarriorAttack(skeletonWarrior);
            actualPhase=EVENTS.EXIT;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

    public bool BeginAttack(){
        return false;
    }
}
