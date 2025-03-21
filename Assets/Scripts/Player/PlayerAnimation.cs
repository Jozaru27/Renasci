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
        playerAnim.SetBool("Idle", true);
        playerAnim.SetBool("Attack", false);
        playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Hit", false);
        playerAnim.SetBool("Dash", false);
        GameManager.Instance.playerCannotMove = false;
    }

    public void Attack()
    {
        playerAnim.SetBool("Idle", false);
        playerAnim.SetBool("Attack", true);
        playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Hit", false);
        playerAnim.SetBool("Dash", false);
    }

    public void Hit()
    {
        playerAnim.SetBool("Idle", false);
        playerAnim.SetBool("Attack", false);
        playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Hit", true);
        playerAnim.SetBool("Dash", false);
    }

    public void Run()
    {
        playerAnim.SetBool("Idle", false);
        playerAnim.SetBool("Attack", false);
        playerAnim.SetBool("Run", true);
        playerAnim.SetBool("Hit", false);
        playerAnim.SetBool("Dash", false);
    }

    public void Dash()
    {
        playerAnim.SetBool("Dash", true);
    }

    public void Death()
    {
        playerAnim.SetBool("Death", true);
    }
}
