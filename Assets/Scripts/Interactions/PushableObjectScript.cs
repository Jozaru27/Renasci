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

    // DESINTEGRACIÓN
    //IEnumerator DissolveRock(GameObject parentObject)
    //{
    //    GetComponent<Renderer>().material = dissolveMaterial;
    //    currentMaterial = GetComponent<Renderer>().material;
    //    rb.constraints = RigidbodyConstraints.None;

    //    while (currentMaterial.GetFloat("_CutOff_Height") > -5)
    //    {
    //        currentMaterial.SetFloat("_CutOff_Height", currentMaterial.GetFloat("_CutOff_Height") - (5 * Time.deltaTime));
    //        yield return null;
    //    }

    //    if (parentObject != null)
    //        Destroy(parentObject);

    //    Destroy(this.gameObject);
    //}

    // DESINTEGRACIÓN + ENCOGER
    IEnumerator DissolveRock(GameObject parentObject)
    {
        GetComponent<Renderer>().material = dissolveMaterial;
        currentMaterial = GetComponent<Renderer>().material;

        if (!currentMaterial.HasProperty("_CutOff_Height"))
        {
            Debug.LogWarning("El material no tiene la propiedad _CutOff_Height");
            yield break;
        }

        rb.constraints = RigidbodyConstraints.None;

        Vector3 originalScale = transform.localScale;

        float cutoff = currentMaterial.GetFloat("_CutOff_Height");
        float minCutoff = -5f;

        while (cutoff > minCutoff)
        {
            cutoff -= 5f * Time.deltaTime;
            currentMaterial.SetFloat("_CutOff_Height", cutoff);

            float t = Mathf.InverseLerp(minCutoff, 1f, cutoff);
            transform.localScale = originalScale * t;

            yield return null;
        }

        if (parentObject != null)
            Destroy(parentObject);

        Destroy(this.gameObject);
    }

}
