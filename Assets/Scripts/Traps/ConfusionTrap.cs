using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfusionTrap : MonoBehaviour
{
    [SerializeField] float trapCooldown;
    [SerializeField] GameObject confussionDebuffIcon;
    [SerializeField] GameObject confussionRay;
    [SerializeField] LineRenderer rayRend;
    
    bool activeTrap = true;
    bool usingRay;
    GameObject playerObj;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        confussionDebuffIcon.SetActive(false);
    }

    private void Update()
    {
        if (Vector3.Distance(playerObj.transform.position, transform.position) < 3.5f && activeTrap)
        {
            StopAllCoroutines();
            StartCoroutine(TrapCooldown());
            StartCoroutine(GenerateRay());
        }   

        if (usingRay)
        {
            float rayLenght = Vector3.Distance(playerObj.transform.position, transform.position);
            Vector3 finalPoint = new Vector3(0, 0, rayLenght);
            rayRend.SetPosition(1, finalPoint);

            Vector3 playerVector = playerObj.transform.position - confussionRay.transform.position;
            confussionRay.transform.rotation = Quaternion.LookRotation(playerVector);
        }
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

    IEnumerator GenerateRay()
    {
        confussionRay.SetActive(true);

        usingRay = true;

        yield return new WaitForSeconds(0.25f);

        confussionRay.SetActive(false);

        usingRay = false;
    }
}
