using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObjectScript : MonoBehaviour
{
    [SerializeField] float pushableForce;
    [SerializeField] Material dissolveMaterial;

    Rigidbody rb;
    Material currentMaterial;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentMaterial = GetComponent<Renderer>().material;
    }

    public void PushRock(Vector3 windPosition, GameObject parentObject)
    {
        Vector3 pushDirection = transform.position - windPosition;
        pushDirection = new Vector3(pushDirection.x, 0, pushDirection.z);
        rb.AddForce(pushDirection.normalized * pushableForce, ForceMode.Impulse);
        StartCoroutine(DissolveRock(parentObject));
    }

    IEnumerator DissolveRock(GameObject parentObject)
    {
        GetComponent<Renderer>().material = dissolveMaterial;
        currentMaterial = GetComponent<Renderer>().material;
        rb.constraints = RigidbodyConstraints.None;

        while (currentMaterial.GetFloat("_CutOff_Height") > -5)
        {
            currentMaterial.SetFloat("_CutOff_Height", currentMaterial.GetFloat("_CutOff_Height") - (5 * Time.deltaTime));
            yield return null;
        }

        if (parentObject != null)
            Destroy(parentObject);

        Destroy(this.gameObject);
    }
}
