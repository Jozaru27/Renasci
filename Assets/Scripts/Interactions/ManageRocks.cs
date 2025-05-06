using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageRocks : MonoBehaviour
{
    [SerializeField] GameObject[] rocks;

    bool pushed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wind") && !pushed)
        {
            foreach (GameObject rock in rocks)
            {
                rock.GetComponent<Rigidbody>().isKinematic = false;
                rock.GetComponent<PushableObjectScript>().PushRock(other.gameObject.transform.position, this.gameObject);
            }

            pushed = true;
        }
    }
}
