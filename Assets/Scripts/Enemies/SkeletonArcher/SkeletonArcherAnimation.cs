using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcherAnimation : MonoBehaviour
{
    public Animator SkeletonArcherAnim;

    void Start()
    {
        SkeletonArcherAnim = this.gameObject.GetComponent<Animator>();
    }

    public void Idle()
    {
        SkeletonArcherAnim.SetBool("Idle", true);
        SkeletonArcherAnim.SetBool("Run", false);
        SkeletonArcherAnim.SetBool("Attack", false);
        SkeletonArcherAnim.SetBool("Hit", false);
    }
    public void Run()
    {
        SkeletonArcherAnim.SetBool("Idle", false);
        SkeletonArcherAnim.SetBool("Run", true);
        SkeletonArcherAnim.SetBool("Attack", false);
        SkeletonArcherAnim.SetBool("Hit", false);
    }
    public void Attack()
    {
        SkeletonArcherAnim.SetBool("Idle", false);
        SkeletonArcherAnim.SetBool("Run", false);
        SkeletonArcherAnim.SetBool("Attack", true);
        SkeletonArcherAnim.SetBool("Hit", false);
    }
    public void Hit()
    {
        SkeletonArcherAnim.SetBool("Idle", false);
        SkeletonArcherAnim.SetBool("Run", false);
        SkeletonArcherAnim.SetBool("Attack", false);
        SkeletonArcherAnim.SetBool("Hit", true);
        GetComponent<SkeletonArcher>().goToIdle = true;
    }

    public void Death()
    {
        SkeletonArcherAnim.SetBool("Idle", false);
        SkeletonArcherAnim.SetBool("Run", false);
        SkeletonArcherAnim.SetBool("Attack", false);
        SkeletonArcherAnim.SetBool("Hit", false);
        SkeletonArcherAnim.SetBool("Death", true);
    }
}
