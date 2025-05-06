using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObjectScript : MonoBehaviour
{
    [SerializeField] float pushableForce;

    Rigidbody rb;
    Material material;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        material = GetComponent<Renderer>().material;
    }

    public void PushRock(Vector3 windPosition, GameObject parentObject)
    {
        Vector3 pushDirection = transform.position - windPosition;
        rb.AddForce(pushDirection.normalized * pushableForce, ForceMode.Impulse);
        StartCoroutine(DissolveRock(parentObject));
    }

    IEnumerator DissolveRock(GameObject parentObject)
    {
        while (material.GetFloat("_CutOff_Height") > -80)
        {
            material.SetFloat("_CutOff_Height", material.GetFloat("_CutOff_Height") - (25 * Time.deltaTime));
            yield return null;
        }

        if (parentObject != null)
            Destroy(parentObject);

        Destroy(this.gameObject);
    }
}
