using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorAttack : SkeletonWarriorStates
{
    public SkeletonWarriorAttack(SkeletonWarrior skeletonWarrior) : base()
    {
        name = STATES.ATTACK;
        iniateVariables(skeletonWarrior);
    }

    public override void Entry()
    {
        base.Entry();
    }

    public override void Updating()
    {

    }

    public override void Exit()
    {
        base.Exit();
    }
}
