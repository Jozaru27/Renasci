using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMageAnimation : MonoBehaviour
{
    [HideInInspector] public Animator skeletonMageAnim;
    [SerializeField] GameObject book;

    void Start()
    {
        skeletonMageAnim = GetComponent<Animator>();
        book.GetComponent<Animator>().speed = 0;
    }

    public void Idle()
    {
        skeletonMageAnim.SetBool("Idle", true);
        skeletonMageAnim.SetBool("Run", false);
        skeletonMageAnim.SetBool("Attack", false);
        skeletonMageAnim.SetBool("Hit", false);
        skeletonMageAnim.SetBool("SecondAttack", false);
        skeletonMageAnim.SetBool("Teleport", false);
        book.GetComponent<Animator>().Play("Book_Open");
        book.GetComponent<Animator>().speed = 0;
    }
    public void Run()
    {
        skeletonMageAnim.SetBool("Idle", false);
        skeletonMageAnim.SetBool("Run", true);
        skeletonMageAnim.SetBool("Attack", false);
        skeletonMageAnim.SetBool("Hit", false);
        skeletonMageAnim.SetBool("SecondAttack", false);
        skeletonMageAnim.SetBool("Teleport", false);
        book.GetComponent<Animator>().Play("Book_Open");
        book.GetComponent<Animator>().speed = 0;
    }
    public void Attack()
    {
        skeletonMageAnim.SetBool("Idle", false);
        skeletonMageAnim.SetBool("Run", false);
        skeletonMageAnim.SetBool("Attack", true);
        skeletonMageAnim.SetBool("Hit", false);
        skeletonMageAnim.SetBool("SecondAttack", false);
        skeletonMageAnim.SetBool("Teleport", false);
        book.GetComponent<Animator>().speed = 1;
    }
    public void SecondAttack()
    {
        skeletonMageAnim.SetBool("Idle", false);
        skeletonMageAnim.SetBool("Run", false);
        skeletonMageAnim.SetBool("Attack", false);
        skeletonMageAnim.SetBool("Hit", false);
        skeletonMageAnim.SetBool("SecondAttack", true);
        skeletonMageAnim.SetBool("Teleport", false);
        book.GetComponent<Animator>().Play("Book_Open");
        book.GetComponent<Animator>().speed = 0;
    }
    public void Hit()
    {
        skeletonMageAnim.SetBool("Idle", false);
        skeletonMageAnim.SetBool("Run", false);
        skeletonMageAnim.SetBool("Attack", false);
        skeletonMageAnim.SetBool("Hit", true);
        skeletonMageAnim.SetBool("SecondAttack", false);
        skeletonMageAnim.SetBool("Teleport", false);
        book.GetComponent<Animator>().Play("Book_Open");
        book.GetComponent<Animator>().speed = 0;
    }
    public void Teleport()
    {
        skeletonMageAnim.SetBool("Idle", false);
        skeletonMageAnim.SetBool("Run", false);
        skeletonMageAnim.SetBool("Attack", false);
        skeletonMageAnim.SetBool("Hit", false);
        skeletonMageAnim.SetBool("SecondAttack", false);
        skeletonMageAnim.SetBool("Teleport", true);
        book.GetComponent<Animator>().Play("Book_Open");
        book.GetComponent<Animator>().speed = 0;
    }
    public void Death()
    {
        skeletonMageAnim.SetBool("Idle", false);
        skeletonMageAnim.SetBool("Run", false);
        skeletonMageAnim.SetBool("Attack", false);
        skeletonMageAnim.SetBool("Hit", false);
        skeletonMageAnim.SetBool("SecondAttack", false);
        skeletonMageAnim.SetBool("Teleport", false);
        skeletonMageAnim.SetBool("Death", true);
        book.GetComponent<Animator>().Play("Book_Open");
        book.GetComponent<Animator>().speed = 0;
    }

    public void ChangeToIdleState()
    {
        GetComponent<SkeletonMage>().goToIdle = true;
        GetComponent<SkeletonMage>().damaged = false;
    }
}
