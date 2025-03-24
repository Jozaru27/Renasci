using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorAnimation : MonoBehaviour
{
    public Animator SkeletonWarriorAnim;
    // Start is called before the first frame update
    void Start()
    {
        SkeletonWarriorAnim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Idle()
    {
        SkeletonWarriorAnim.SetBool("Idle", true);
        SkeletonWarriorAnim.SetBool("Run", false);
        SkeletonWarriorAnim.SetBool("Attack", false);
        SkeletonWarriorAnim.SetBool("Hit", false);
        SkeletonWarriorAnim.SetBool("Block", false);
    }
    public void Run()
    {
        SkeletonWarriorAnim.SetBool("Idle", false);
        SkeletonWarriorAnim.SetBool("Run", true);
        SkeletonWarriorAnim.SetBool("Attack", false);
        SkeletonWarriorAnim.SetBool("Hit", false);
        SkeletonWarriorAnim.SetBool("Block", false);
    }
    public void Attack()
    {
        SkeletonWarriorAnim.SetBool("Idle", false);
        SkeletonWarriorAnim.SetBool("Run", false);
        SkeletonWarriorAnim.SetBool("Attack", true);
        SkeletonWarriorAnim.SetBool("Hit", false);
        SkeletonWarriorAnim.SetBool("Block", false);
    }
    public void Hit()
    {
        SkeletonWarriorAnim.SetBool("Idle", false);
        SkeletonWarriorAnim.SetBool("Run", false);
        SkeletonWarriorAnim.SetBool("Attack", false);
        SkeletonWarriorAnim.SetBool("Hit", true);
        SkeletonWarriorAnim.SetBool("Block", false);
    }
    public void Block()
    {
        SkeletonWarriorAnim.SetBool("Idle", false);
        SkeletonWarriorAnim.SetBool("Run", false);
        SkeletonWarriorAnim.SetBool("Attack", false);
        SkeletonWarriorAnim.SetBool("Hit", false);
        SkeletonWarriorAnim.SetBool("Block", true);
    }
    public void Death()
    {
        SkeletonWarriorAnim.SetBool("Idle", false);
        SkeletonWarriorAnim.SetBool("Run", false);
        SkeletonWarriorAnim.SetBool("Attack", false);
        SkeletonWarriorAnim.SetBool("Hit", false);
        SkeletonWarriorAnim.SetBool("Block", false);
        SkeletonWarriorAnim.SetBool("Death", true);
    }
}
