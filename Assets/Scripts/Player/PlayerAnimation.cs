using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator playerAnim;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    public void Idle()
    {
        Debug.Log("IDLE");
        playerAnim.SetBool("Idle", true);
        playerAnim.SetBool("Attack", false);
        playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Hit", false);
        playerAnim.SetBool("Dash", false);
    }

    public void Attack()
    {
        Debug.Log("ATTACK");
        playerAnim.SetBool("Idle", false);
        playerAnim.SetBool("Attack", true);
        playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Hit", false);
        playerAnim.SetBool("Dash", false);
    }

    public void Hit()
    {
        Debug.Log("HIT");
        playerAnim.SetBool("Idle", false);
        playerAnim.SetBool("Attack", false);
        playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Hit", true);
        playerAnim.SetBool("Dash", false);
    }

    public void Run()
    {
        Debug.Log("RUN");
        playerAnim.SetBool("Idle", false);
        playerAnim.SetBool("Attack", false);
        playerAnim.SetBool("Run", true);
        playerAnim.SetBool("Hit", false);
        playerAnim.SetBool("Dash", false);
    }

    public void Dash()
    {
        Debug.Log("DASH");
        playerAnim.SetBool("Dash", true);
    }

    public void Death()
    {
        Debug.Log("DEATH");
        playerAnim.SetBool("Death", true);
    }
}
