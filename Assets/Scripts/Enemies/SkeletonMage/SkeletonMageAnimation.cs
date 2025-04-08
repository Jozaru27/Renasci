using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMageAnimation : MonoBehaviour
{
    [HideInInspector] public Animator skeletonMageAnim;

    void Start()
    {
        skeletonMageAnim = GetComponent<Animator>();
    }

    public void Idle()
    {
        skeletonMageAnim.SetBool("Idle", true);
        skeletonMageAnim.SetBool("Run", false);
        skeletonMageAnim.SetBool("Attack", false);
        skeletonMageAnim.SetBool("Hit", false);
        skeletonMageAnim.SetBool("SecondAttack", false);
    }
    public void Run()
    {
        skeletonMageAnim.SetBool("Idle", false);
        skeletonMageAnim.SetBool("Run", true);
        skeletonMageAnim.SetBool("Attack", false);
        skeletonMageAnim.SetBool("Hit", false);
        skeletonMageAnim.SetBool("SecondAttack", false);
    }
    public void Attack()
    {
        skeletonMageAnim.SetBool("Idle", false);
        skeletonMageAnim.SetBool("Run", false);
        skeletonMageAnim.SetBool("Attack", true);
        skeletonMageAnim.SetBool("Hit", false);
        skeletonMageAnim.SetBool("SecondAttack", false);
    }
    public void SecondAttack()
    {
        skeletonMageAnim.SetBool("Idle", false);
        skeletonMageAnim.SetBool("Run", false);
        skeletonMageAnim.SetBool("Attack", false);
        skeletonMageAnim.SetBool("Hit", false);
        skeletonMageAnim.SetBool("SecondAttack", true);
    }
    public void Hit()
    {
        skeletonMageAnim.SetBool("Idle", false);
        skeletonMageAnim.SetBool("Run", false);
        skeletonMageAnim.SetBool("Attack", false);
        skeletonMageAnim.SetBool("Hit", true);
        skeletonMageAnim.SetBool("SecondAttack", false);
        GetComponent<SkeletonMage>().goToIdle = true;
    }

    public void Death()
    {
        skeletonMageAnim.SetBool("Idle", false);
        skeletonMageAnim.SetBool("Run", false);
        skeletonMageAnim.SetBool("Attack", false);
        skeletonMageAnim.SetBool("Hit", false);
        skeletonMageAnim.SetBool("SecondAttack", false);
        skeletonMageAnim.SetBool("Death", true);
    }
}
