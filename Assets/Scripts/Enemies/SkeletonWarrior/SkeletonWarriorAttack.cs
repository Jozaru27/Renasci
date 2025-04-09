using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarriorAttack : SkeletonWarriorStates
{
    
    
    //bool warriorFarPlayer=false;
    //bool startBlock = false;
    public SkeletonWarriorAttack(SkeletonWarrior _skeletonWarrior) : base()
    {
        //Debug.Log("ATTACKING");
        name = STATES.ATTACK;
        skeletonWarrior=_skeletonWarrior;
        iniateVariables(skeletonWarrior);
    }

    public override void Entry()
    {
        
        

        if (!skeletonWarrior.dead)
        {
            //skeletonWarrior.StartCoroutine(GoingToBlock());
            skeletonWarrior.isBlocking = false;
            skeletonWarrior.skeletonWarriorAgent.isStopped = true;
            skeletonWarrior.skeletonWarriorObject.GetComponent<SkeletonWarriorAnimation>().Attack();
            //skeletonWarrior.skeletonWarriorObject.transform.LookAt(skeletonWarrior.playerObject.transform.position);
        }

        base.Entry();
    }

    public override void Updating()
    {
        //NavMeshAgent skeletonWarriorNav = skeletonWarrior.gameObject.GetComponent<NavMeshAgent>();
        //skeletonWarriorNav.isStopped = true;
        //skeletonWarrior.skeletonWarriorObject.transform.LookAt(skeletonWarrior.playerObject.transform.position);
        float distanceToPlayer=Vector3.Distance(skeletonWarrior.skeletonWarriorObject.transform.position,skeletonWarrior.playerObject.transform.position);

        if (distanceToPlayer > skeletonWarrior.stats.detectionDistance)
            AmbientSoundManager.Instance.enableCombatMusic = false;

        //skeletonWarrior.skeletonWarriorObject.transform.position = Vector3.Slerp(skeletonWarrior.skeletonWarriorObject.transform.position, skeletonWarrior.playerObject.transform.position, 2 * Time.deltaTime);

        //skeletonWarrior.skeletonWarriorObject.GetComponent<SkeletonWarriorAnimation>().Attack();
        /*
        if (distanceToPlayer>2){
            warriorFarPlayer=true;
        }else{
            warriorFarPlayer=false;
        }
        */

        if (playerFar())
        {
            nextState = new SkeletonWarriorFollow(skeletonWarrior);
            actualPhase = EVENTS.EXIT;
        }

        
        if(blocking())
        {
            nextState=new SkeletonWarriorBlock(skeletonWarrior);
            actualPhase=EVENTS.EXIT;
        }

        if (skeletonWarrior.goToIdle)
        {
            nextState = new SkeletonWarriorIdle(skeletonWarrior);
            actualPhase = EVENTS.EXIT;
        }
        


    }

    public override void Exit()
    {
        base.Exit();
    }
    public bool blocking(){
        if(skeletonWarrior.startBlock==true){
            return true;
        }else{
            return false;
        }
        
        
    }
    public bool playerFar(){
    if(skeletonWarrior.warriorAttackFinish==true){
        return true;
    }else{
        return false;
    }
    }
    /*
    IEnumerator GoingToBlock()
    {
        yield return new WaitForSeconds(1f);
        startBlock = true;
        
    }
    */
    

    
}
