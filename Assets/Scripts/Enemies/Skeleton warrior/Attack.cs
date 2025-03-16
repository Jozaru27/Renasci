using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : States
{
    public Attack() : base()
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
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
