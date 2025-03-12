using System.Collections;
using UnityEngine;

public class ZZTest : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(IA());
    }

    private void Update()
    {
        //rb.AddForce(Vector3.right * 2, ForceMode.Acceleration);
        rb.velocity = new Vector3(3, rb.velocity.y, rb.velocity.z);
    }

    IEnumerator IA()
    {
        yield return new WaitForSeconds(1.5f);

        rb.AddForce(transform.up * 10, ForceMode.Impulse);
    }
}
