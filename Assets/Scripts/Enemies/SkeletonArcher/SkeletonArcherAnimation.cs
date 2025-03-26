using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcherAnimation : MonoBehaviour
{
    public Animator SkeletonArcherAnim;
    // Start is called before the first frame update
    void Start()
    {
        SkeletonArcherAnim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Idle()
    {
        SkeletonArcherAnim.SetBool("Idle", true);
        SkeletonArcherAnim.SetBool("Run", false);
        SkeletonArcherAnim.SetBool("Attack", false);
        SkeletonArcherAnim.SetBool("Hit", false);
        SkeletonArcherAnim.SetBool("Block", false);
    }
    public void Run()
    {
        SkeletonArcherAnim.SetBool("Idle", false);
        SkeletonArcherAnim.SetBool("Run", true);
        SkeletonArcherAnim.SetBool("Attack", false);
        SkeletonArcherAnim.SetBool("Hit", false);
        SkeletonArcherAnim.SetBool("Block", false);
    }
    public void Attack()
    {
        SkeletonArcherAnim.SetBool("Idle", false);
        SkeletonArcherAnim.SetBool("Run", false);
        SkeletonArcherAnim.SetBool("Attack", true);
        SkeletonArcherAnim.SetBool("Hit", false);
        SkeletonArcherAnim.SetBool("Block", false);
    }
    public void Hit()
    {
        SkeletonArcherAnim.SetBool("Idle", false);
        SkeletonArcherAnim.SetBool("Run", false);
        SkeletonArcherAnim.SetBool("Attack", false);
        SkeletonArcherAnim.SetBool("Hit", true);
        SkeletonArcherAnim.SetBool("Block", false);
        GetComponent<SkeletonArcher>().goToIdle = true;
    }
    public void Block()
    {
        SkeletonArcherAnim.SetBool("Idle", false);
        SkeletonArcherAnim.SetBool("Run", false);
        SkeletonArcherAnim.SetBool("Attack", false);
        SkeletonArcherAnim.SetBool("Hit", false);
        SkeletonArcherAnim.SetBool("Block", true);
    }
    public void Death()
    {
        SkeletonArcherAnim.SetBool("Idle", false);
        SkeletonArcherAnim.SetBool("Run", false);
        SkeletonArcherAnim.SetBool("Attack", false);
        SkeletonArcherAnim.SetBool("Hit", false);
        SkeletonArcherAnim.SetBool("Block", false);
        SkeletonArcherAnim.SetBool("Death", true);
    }
}
