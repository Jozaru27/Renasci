using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfusionTrap : MonoBehaviour
{
    [SerializeField] float trapCooldown;

    bool activeTrap = true;
    GameObject playerObj;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Vector3.Distance(playerObj.transform.position, transform.position) < 3.5f && activeTrap)
            StartCoroutine(TrapCooldown());
    }

    IEnumerator TrapCooldown()
    {
        playerObj.GetComponent<PlayerMovement>().inputFactor = -1;
        activeTrap = false;

        yield return new WaitForSeconds(trapCooldown);

        playerObj.GetComponent<PlayerMovement>().inputFactor = 1;
        activeTrap = true;
    }
}
