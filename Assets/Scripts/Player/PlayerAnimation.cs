using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] GameObject sword;
    [SerializeField] GameObject revolver;
    [SerializeField] GameObject swordParticles;
    [SerializeField] GameObject dustWalk;
    [SerializeField] GameObject[] dustPositions;

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
        playerAnim.SetBool("Interact", false);
        playerAnim.SetBool("RelicAttack", false);
        playerAnim.SetBool("Shoot", false);
        sword.SetActive(true);
        swordParticles.SetActive(false);
        revolver.SetActive(false);
        GameManager.Instance.playerCannotMove = false;
    }

    public void Attack()
    {
        playerAnim.SetBool("Idle", false);
        playerAnim.SetBool("Attack", true);
        playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Hit", false);
        playerAnim.SetBool("Dash", false);
        playerAnim.SetBool("Interact", false);
        playerAnim.SetBool("RelicAttack", false);
        playerAnim.SetBool("Shoot", false);
        sword.SetActive(true);
        swordParticles.SetActive(true);
        swordParticles.GetComponent<ParticleSystem>().Play();
        revolver.SetActive(false);
    }

    public void Hit()
    {
        playerAnim.SetBool("Idle", false);
        playerAnim.SetBool("Attack", false);
        playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Hit", true);
        playerAnim.SetBool("Dash", false);
        playerAnim.SetBool("Interact", false);
        playerAnim.SetBool("RelicAttack", false);
        playerAnim.SetBool("Shoot", false);
        swordParticles.SetActive(false);
        sword.SetActive(true);
        revolver.SetActive(false);
    }

    public void Run()
    {
        playerAnim.SetBool("Idle", false);
        playerAnim.SetBool("Attack", false);
        playerAnim.SetBool("Run", true);
        playerAnim.SetBool("Hit", false);
        playerAnim.SetBool("Dash", false);
        playerAnim.SetBool("Interact", false);
        playerAnim.SetBool("RelicAttack", false);
        playerAnim.SetBool("Shoot", false);
        swordParticles.SetActive(false);
        sword.SetActive(true);
        revolver.SetActive(false);
    }

    public void Interact()
    {
        playerAnim.SetBool("Idle", false);
        playerAnim.SetBool("Attack", false);
        playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Hit", false);
        playerAnim.SetBool("Dash", false);
        playerAnim.SetBool("Interact", true);
        playerAnim.SetBool("RelicAttack", false);
        playerAnim.SetBool("Shoot", false);
        swordParticles.SetActive(false);
        sword.SetActive(false);
        revolver.SetActive(false);
    }

    public void RelicAttack()
    {
        playerAnim.SetBool("Idle", false);
        playerAnim.SetBool("Attack", false);
        playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Hit", false);
        playerAnim.SetBool("Dash", false);
        playerAnim.SetBool("Interact", false);
        playerAnim.SetBool("RelicAttack", true);
        playerAnim.SetBool("Shoot", false);
        swordParticles.SetActive(false);
        sword.SetActive(false);
        revolver.SetActive(false);
    }

    public void Shoot()
    {
        playerAnim.SetBool("Idle", false);
        playerAnim.SetBool("Attack", false);
        playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Hit", false);
        playerAnim.SetBool("Dash", false);
        playerAnim.SetBool("Interact", false);
        playerAnim.SetBool("RelicAttack", false);
        playerAnim.SetBool("Shoot", true);
        swordParticles.SetActive(false);
        sword.SetActive(false);
        revolver.SetActive(true);
    }

    public void Dash()
    {
        playerAnim.SetBool("Dash", true);
        sword.SetActive(false);
        revolver.SetActive(false);
        swordParticles.SetActive(false);
    }

    public void Death()
    {
        playerAnim.SetBool("Death", true);
        sword.SetActive(false);
        revolver.SetActive(true);
        swordParticles.SetActive(false);
    }

    public void GenerateDust(int position)
    {
        GameObject newDustParticle = Instantiate(dustWalk, dustPositions[position].transform.position, Quaternion.identity);
        StartCoroutine(VanishDust(newDustParticle));
    }

    IEnumerator VanishDust(GameObject dustParticle)
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(dustParticle);
    }
}
