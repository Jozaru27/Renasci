using System.Collections;
using UnityEngine;

public class PushTrap : MonoBehaviour
{
    Animator animator;
    GameObject playerObj;

    [SerializeField] Collider trigger;

    public GameObject pushZone;
    public float pushForce = 25f;
    public float delayBeforePush = 0.5f;
    public float zoneActiveTime = 0.2f;

    private bool isPushing = false;
    bool playerInside;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerObj = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("A");

        if (other.gameObject.CompareTag("Player") && !isPushing)
        {
            isPushing = true;
            playerInside = true;
            animator.Play("IdlePush");
        }

        //Debug.Log("Player collided with DetectionZone");


        //StartCoroutine(ActivateTrap(other.GetComponent<Collider>()));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerInside = false;
    }

    public void PushPlayer()
    {
        if (playerInside)
        {
            Vector3 dir = (playerObj.transform.position - transform.position).normalized;
            playerObj.gameObject.GetComponent<Rigidbody>().AddForce(dir * pushForce, ForceMode.Impulse);
        }
    }

    public void PushingFalse()
    {
        isPushing = false;
    }

    //private IEnumerator ActivateTrap(Collider playerCollider)
    //{
    //    isPushing = true;

    //    animator.Play("IdlePush", 0, 0f);

    //    yield return new WaitForSeconds(delayBeforePush);

    //    pushZone.SetActive(true);
    //    //Debug.Log("PushZone ON");

    //    Rigidbody rb = playerCollider.GetComponent<Rigidbody>();
    //    if (rb != null && IsInsidePushZone(playerCollider))
    //    {
    //        Vector3 dir = (playerCollider.transform.position - transform.position).normalized;
    //        rb.AddForce(dir * pushForce, ForceMode.Impulse);
    //        //Debug.Log("Player pushed");
    //    }

    //    yield return new WaitForSeconds(zoneActiveTime);
    //    pushZone.SetActive(false);
    //    //Debug.Log("PushZone OFF");

    //    isPushing = false;
    //}

    //private bool IsInsidePushZone(Collider player)
    //{
    //    Collider zone = pushZone.GetComponent<Collider>();
    //    return zone.bounds.Intersects(player.bounds);
    //}
}
