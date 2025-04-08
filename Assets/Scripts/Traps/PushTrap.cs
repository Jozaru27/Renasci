using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTrap : MonoBehaviour
{
    public Animator animator;
    public GameObject pushZone;

    public float pushForce = 10f;
    public float delayBeforePush = 0.5f;
    public float resetTime = 2f;

    private bool isPushing = false;

    private void OnCollisionEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);

        if (!isPushing && other.CompareTag("Player"))
        {
            StartCoroutine(ActivateTrap(other));
        }
    }

    private IEnumerator ActivateTrap(Collider playerCollider)
    {
        isPushing = true;
        Debug.Log("Trap activated!");

        animator.SetBool("push", true);

        yield return new WaitForSeconds(delayBeforePush);

        Debug.Log("PushZone activated");
        pushZone.SetActive(true);

        Rigidbody playerRb = playerCollider.GetComponent<Rigidbody>();
        if (playerRb != null && IsPlayerInPushZone(playerCollider))
        {
            Vector3 pushDir = (playerRb.transform.position - transform.position).normalized;
            playerRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(0.2f);
        pushZone.SetActive(false);

        yield return new WaitForSeconds(resetTime);

        animator.SetBool("push", false);
        isPushing = false;
    }

    private bool IsPlayerInPushZone(Collider playerCollider)
    {
        Collider pushCollider = pushZone.GetComponent<Collider>();
        return pushCollider.bounds.Intersects(playerCollider.bounds);
    }
}
