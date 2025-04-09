using System.Collections;
using UnityEngine;

public class PushTrap : MonoBehaviour
{
    public Animator animator;
    public GameObject pushZone;
    public float pushForce = 10f;
    public float delayBeforePush = 0.5f;
    public float zoneActiveTime = 0.2f;

    private bool isPushing = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPushing) return;
        if (!other.gameObject.CompareTag("Player")) return;

        //Debug.Log("Player collided with DetectionZone");
        StartCoroutine(ActivateTrap(other.GetComponent<Collider>()));
    }

    private IEnumerator ActivateTrap(Collider playerCollider)
    {
        isPushing = true;

        animator.Play("IdlePush", 0, 0f);

        yield return new WaitForSeconds(delayBeforePush);

        pushZone.SetActive(true);
        //Debug.Log("PushZone ON");

        Rigidbody rb = playerCollider.GetComponent<Rigidbody>();
        if (rb != null && IsInsidePushZone(playerCollider))
        {
            Vector3 dir = (playerCollider.transform.position - transform.position).normalized;
            rb.AddForce(dir * pushForce, ForceMode.Impulse);
            //Debug.Log("Player pushed");
        }

        yield return new WaitForSeconds(zoneActiveTime);
        pushZone.SetActive(false);
        //Debug.Log("PushZone OFF");

        isPushing = false;
    }

    private bool IsInsidePushZone(Collider player)
    {
        Collider zone = pushZone.GetComponent<Collider>();
        return zone.bounds.Intersects(player.bounds);
    }
}
