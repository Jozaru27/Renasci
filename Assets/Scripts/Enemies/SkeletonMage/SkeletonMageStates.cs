using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMageStates
{
    public SkeletonMage skeletonMage;

    public void iniateVariables(SkeletonMage _skeletonMage)
    {
        skeletonMage = _skeletonMage;
    }

    public enum STATES
    {
        IDLE, FOLLOW, ATTACK, PATROL
    }

    public enum EVENTS
    {
        ENTRY, UPDATING, EXIT
    }

    public STATES name;
    protected EVENTS actualPhase;
    protected SkeletonMageStates nextState;

    public SkeletonMageStates()
    {

    }

    public virtual void Entry() { actualPhase = EVENTS.UPDATING; }
    public virtual void Updating() { actualPhase = EVENTS.UPDATING; }
    public virtual void Exit() { actualPhase = EVENTS.EXIT; }


    public SkeletonMageStates Process()
    {
        if (actualPhase == EVENTS.ENTRY) Entry();
        if (actualPhase == EVENTS.UPDATING) Updating();
        if (actualPhase == EVENTS.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
}
