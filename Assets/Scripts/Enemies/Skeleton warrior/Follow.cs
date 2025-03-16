using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : States
{
    public Follow(): base()
    {
        Debug.Log("FOLLOWING");
        name=STATES.FOLLOW;
    }

    public override void Entry()
    {
        base.Entry();
    }

    public override void Updating()
    {
        if(PLayerIsNear())
        {
            nextState=new Attack();
            actualPhase=EVENT.EXIT;
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
