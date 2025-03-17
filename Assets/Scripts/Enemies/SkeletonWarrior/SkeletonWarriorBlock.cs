using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorBlock : SkeletonWarriorStates
{
    public SkeletonWarriorBlock(SkeletonWarrior _skeletonWarrior) : base()
    {
        name = STATES.BLOCK;
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
