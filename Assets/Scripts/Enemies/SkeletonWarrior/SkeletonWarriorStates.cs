using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorStates
{
    public SkeletonWarrior skeletonWarrior;

    public void iniateVariables(SkeletonWarrior _skeletonWarrior)
    {
        skeletonWarrior = _skeletonWarrior;
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
    protected SkeletonWarriorStates nextState;

    public SkeletonWarriorStates()
    {

    }

    public virtual void Entry() { actualPhase = EVENTS.UPDATING; }
    public virtual void Updating() { actualPhase = EVENTS.UPDATING; }
    public virtual void Exit() { actualPhase = EVENTS.EXIT; }


    public SkeletonWarriorStates Process()
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
