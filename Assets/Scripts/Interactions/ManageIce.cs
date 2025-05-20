using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageIce : MonoBehaviour
{
    [SerializeField] GameObject[] iceBlocks;
    Collider collision;

    bool frozen;

    private void Start()
    {
        collision = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ice") && !frozen)
        {
            foreach (GameObject ice in iceBlocks)
            {
                ice.GetComponent<FreezableObjectScript>().Freeze(collision);
            }

            frozen = true;
        }
    }
}
