using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcherStates
{
    public SkeletonArcher skeletonArcher;

    public void iniateVariables(SkeletonArcher _skeletonArcher)
    {
        skeletonArcher = _skeletonArcher;
    }

    public enum STATES
    {
        IDLE, FOLLOW, ATTACK, BLOCK, PATROL
    }

    public enum EVENTS
    {
        ENTRY, UPDATING, EXIT
    }

    public STATES name;
    protected EVENTS actualPhase;
    protected SkeletonArcherStates nextState;

    public SkeletonArcherStates()
    {

    }

    public virtual void Entry() { actualPhase = EVENTS.UPDATING; }
    public virtual void Updating() { actualPhase = EVENTS.UPDATING; }
    public virtual void Exit() { actualPhase = EVENTS.EXIT; }


    public SkeletonArcherStates Process()
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
