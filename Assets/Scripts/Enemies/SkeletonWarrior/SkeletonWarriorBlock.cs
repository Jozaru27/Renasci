using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorBlock : SkeletonWarriorStates
{
    public SkeletonWarriorBlock() : base()
    {
        Debug.Log("BLOCKING");
        name = STATES.BLOCK;

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
