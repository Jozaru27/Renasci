using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool grounded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            GameManager.Instance.playerCannotMove = false;
            grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("SALE");

        if (other.gameObject.CompareTag("Ground"))
        {
            GameManager.Instance.playerCannotMove = true;
            grounded = false;
        }
    }
}
