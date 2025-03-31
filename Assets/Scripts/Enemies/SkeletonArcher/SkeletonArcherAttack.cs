using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonArcherAttack : SkeletonArcherStates
{
    
    //bool warriorFarPlayer=false;

    public SkeletonArcherAttack(SkeletonArcher _skeletonArcher) : base()
    {
        //Debug.Log("ATTACKING");
        name = STATES.ATTACK;
        skeletonArcher=_skeletonArcher;
        iniateVariables(skeletonArcher);
    }

    public override void Entry()
    {
        skeletonArcher.skeletonArcherAgent.isStopped = true;
        skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Attack();
        //skeletonArcher.skeletonArcherObject.transform.LookAt(skeletonArcher.playerObject.transform.position);
        //skeletonArcher.StartCoroutine(AttackCoroutine());
        base.Entry();
    }

    public override void Updating()
    {
        //NavMeshAgent skeletonArcherNav = skeletonArcher.gameObject.GetComponent<NavMeshAgent>();
        //skeletonArcherNav.isStopped = true;
        //skeletonArcher.skeletonArcherObject.transform.LookAt(skeletonArcher.playerObject.transform.position);

        Vector3 playerDirection = skeletonArcher.playerObject.transform.position - skeletonArcher.skeletonArcherObject.transform.position;
        Quaternion playerRotation = Quaternion.LookRotation(playerDirection.normalized);

        skeletonArcher.skeletonArcherObject.transform.rotation = Quaternion.Lerp(skeletonArcher.skeletonArcherObject.transform.rotation, playerRotation, 5 * Time.deltaTime);

        if (Quaternion.Angle(skeletonArcher.skeletonArcherObject.transform.rotation, playerRotation) <= 15f && skeletonArcher.damaged)
            skeletonArcher.skeletonArcherObject.transform.rotation = playerRotation;

        float distanceToPlayer=Vector3.Distance(skeletonArcher.skeletonArcherObject.transform.position,skeletonArcher.playerObject.transform.position);

        //skeletonArcher.skeletonArcherObject.transform.position = Vector3.Slerp(skeletonArcher.skeletonArcherObject.transform.position, skeletonArcher.playerObject.transform.position, 2 * Time.deltaTime);

        //skeletonArcher.skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Attack();
        /*
        if (distanceToPlayer>2){
            warriorFarPlayer=true;
        }else{
            warriorFarPlayer=false;
        }
        */

        //if (playerFar())
        //{
        //    nextState = new SkeletonArcherFollow(skeletonArcher);
        //    actualPhase = EVENTS.EXIT;
        //}

        if (distanceToPlayer > 6.5f)
        {
            nextState = new SkeletonArcherFollow(skeletonArcher);
            actualPhase = EVENTS.EXIT;
        }

        if (distanceToPlayer < 3f)
        {
            nextState = new SkeletonArcherFollow(skeletonArcher);
            actualPhase = EVENTS.EXIT;
            return;
        }

        if (skeletonArcher.goToIdle)
        {
            nextState = new SkeletonArcherIdle(skeletonArcher);
            actualPhase = EVENTS.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    //public bool playerFar(){
    //    if(skeletonArcher.archerAttackFinish==true)
    //    {
    //        return true;
    //    }else{
    //        return false;
    //    }
    //}

//    IEnumerator AttackCoroutine() // A REVISAR SEGÚN ANIMACIONES
//    {
//        yield return new WaitForSeconds(1f); // Espera 1 segundo antes de disparar

//        FireArrow(); // Dispara la flecha

//        yield return new WaitForSeconds(0.5f); // Pequeño retraso entre ataques

//        skeletonArcher.archerAttackFinish = true; // Marca que terminó el ataque
//    }

//    void FireArrow()
//    {
//        if (skeletonArcher.arrowPrefab != null && skeletonArcher.firePoint != null)
//        {
//            GameObject arrow = GameObject.Instantiate(skeletonArcher.arrowPrefab, skeletonArcher.firePoint.position, Quaternion.identity);
//            Rigidbody rb = arrow.GetComponent<Rigidbody>();

//            if (rb != null)
//            {
//                Vector3 direction = (skeletonArcher.playerObject.transform.position - skeletonArcher.firePoint.position).normalized;
//                rb.velocity = direction * 15f;
//            }
//        }
//    }
}