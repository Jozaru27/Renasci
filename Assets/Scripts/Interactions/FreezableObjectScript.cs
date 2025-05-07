using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezableObjectScript : MonoBehaviour
{
    [SerializeField] Material solidMaterial;

    [SerializeField] GameObject ice_01, ice_02;

    Material currentMaterial;
    Color currentColor;

    private void Start()
    {
        currentMaterial = ice_01.GetComponent<Renderer>().material;
        currentColor = ice_02.GetComponent<Renderer>().material.color;
        currentColor.a = ice_02.GetComponent<Renderer>().material.color.a;
    }

    public void Freeze(GameObject freezeManager)
    {
        StartCoroutine(StartFreezing(freezeManager));
    }

    IEnumerator StartFreezing(GameObject freezeManager)
    {
        ice_02.SetActive(true);

        while (currentMaterial.GetFloat("_CutOff_Height") < 0)
        {
            currentMaterial.SetFloat("_CutOff_Height", currentMaterial.GetFloat("_CutOff_Height") + (15f * Time.deltaTime));

            if (currentColor.a < 0.2f)
            {
                currentColor.a += (0.0125f * Time.deltaTime);
                ice_02.GetComponent<Renderer>().material.color = currentColor;
            }
            
            yield return null;
        }

        currentMaterial = solidMaterial;
        ice_01.GetComponent<Renderer>().material = currentMaterial;
        

        if (freezeManager != null)
            Destroy(freezeManager);
    }
}
