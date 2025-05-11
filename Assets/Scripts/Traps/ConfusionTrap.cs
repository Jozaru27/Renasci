using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfusionTrap : MonoBehaviour
{
    [SerializeField] float trapCooldown;
    [SerializeField] GameObject confussionDebuffIcon;
    
    bool activeTrap = true;
    GameObject playerObj;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        confussionDebuffIcon.SetActive(false);
    }

    private void Update()
    {
        if (Vector3.Distance(playerObj.transform.position, transform.position) < 3.5f && activeTrap)
            StartCoroutine(TrapCooldown());
    }

    IEnumerator TrapCooldown()
    {
        playerObj.GetComponent<PlayerMovement>().inputFactor = -1;
        confussionDebuffIcon.SetActive(true);
        activeTrap = false;

        yield return new WaitForSeconds(trapCooldown);

        playerObj.GetComponent<PlayerMovement>().inputFactor = 1;
        confussionDebuffIcon.SetActive(false);
        activeTrap = true;
    }
}
