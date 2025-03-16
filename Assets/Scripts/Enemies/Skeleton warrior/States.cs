using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States
{
    public SkeletonWarrior skeletonWarrior;
  
    void iniateVariables(SkeletonWarrior _skeletonWarrior){
        skeletonWarrior=_skeletonWarrior;
    }

    public enum STATES
    {
        FOLLOW,ATTACK,BLOCK
    }

    public enum EVENTS
    {
         ENTRY,UPDATING,EXIT
    }
    
    public STATES name; 
    protected EVENTS actualPhase;
    protected States nextState;

    
    public State()
    {
        
    }
    

    public virtual void Entry(){actualPhase = EVENTS.UPDATING;}
    public virtual void Updating(){actualPhase=EVENTS.UPDATING;}
    public virtual void Exit(){actualPhase=EVENTS.EXIT;}
    

    public States Process()
    {
        if(actualPhase==EVENTS.ENTRY) Entry();
        if(actualPhase==EVENTS.UPDATING) Updating();
        if(actualPhase==EVENTS.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
}
