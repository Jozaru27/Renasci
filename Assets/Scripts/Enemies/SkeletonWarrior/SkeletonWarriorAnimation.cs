using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorAnimation : MonoBehaviour
{
    public Animator SkeletonWarriorAnim;

    [SerializeField] GameObject swordParticles;

    void Start()
    {
        SkeletonWarriorAnim = this.gameObject.GetComponent<Animator>();
    }

    public void Idle()
    {
        SkeletonWarriorAnim.SetBool("Idle", true);
        SkeletonWarriorAnim.SetBool("Run", false);
        SkeletonWarriorAnim.SetBool("Attack", false);
        SkeletonWarriorAnim.SetBool("Hit", false);
        SkeletonWarriorAnim.SetBool("Block", false);
        swordParticles.SetActive(false);
    }
    public void Run()
    {
        SkeletonWarriorAnim.SetBool("Idle", false);
        SkeletonWarriorAnim.SetBool("Run", true);
        SkeletonWarriorAnim.SetBool("Attack", false);
        SkeletonWarriorAnim.SetBool("Hit", false);
        SkeletonWarriorAnim.SetBool("Block", false);
        swordParticles.SetActive(false);
    }
    public void Attack()
    {
        SkeletonWarriorAnim.SetBool("Idle", false);
        SkeletonWarriorAnim.SetBool("Run", false);
        SkeletonWarriorAnim.SetBool("Attack", true);
        SkeletonWarriorAnim.SetBool("Hit", false);
        SkeletonWarriorAnim.SetBool("Block", false);
        swordParticles.SetActive(true);
        swordParticles.GetComponent<ParticleSystem>().Play();
    }
    public void Hit()
    {
        SkeletonWarriorAnim.SetBool("Idle", false);
        SkeletonWarriorAnim.SetBool("Run", false);
        SkeletonWarriorAnim.SetBool("Attack", false);
        SkeletonWarriorAnim.SetBool("Hit", true);
        SkeletonWarriorAnim.SetBool("Block", false);
        GetComponent<SkeletonWarrior>().goToIdle = true;
        swordParticles.SetActive(false);
    }
    public void Block()
    {
        SkeletonWarriorAnim.SetBool("Idle", false);
        SkeletonWarriorAnim.SetBool("Run", false);
        SkeletonWarriorAnim.SetBool("Attack", false);
        SkeletonWarriorAnim.SetBool("Hit", false);
        SkeletonWarriorAnim.SetBool("Block", true);
        swordParticles.SetActive(false);
    }
    public void Death()
    {
        SkeletonWarriorAnim.SetBool("Idle", false);
        SkeletonWarriorAnim.SetBool("Run", false);
        SkeletonWarriorAnim.SetBool("Attack", false);
        SkeletonWarriorAnim.SetBool("Hit", false);
        SkeletonWarriorAnim.SetBool("Block", false);
        SkeletonWarriorAnim.SetBool("Death", true);
        swordParticles.SetActive(false);
    }
}
