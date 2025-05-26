using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfusionTrap : MonoBehaviour
{
    [SerializeField] float trapCooldown;
    [SerializeField] GameObject confussionRay;
    [SerializeField] LineRenderer rayRend;
    
    bool activeTrap = true;
    bool usingRay;
    GameObject playerObj;

    AudioSource audioSource;
    public AudioClip confusionSound;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Vector3.Distance(playerObj.transform.position, transform.position) < 3.5f && activeTrap && playerObj.GetComponent<PlayerMovement>().inputFactor == 1)
        {
            StopAllCoroutines();
            StartCoroutine(TrapCooldown());
            StartCoroutine(GenerateRay());

            audioSource.PlayOneShot(confusionSound, 5f);
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
        UIManager.Instance.ActiveConfussionCooldown(trapCooldown);
        activeTrap = false;
        GetComponent<FirstConfussionInteraction>().OpenConfussionPanel();

        yield return new WaitForSeconds(trapCooldown);

        playerObj.GetComponent<PlayerMovement>().inputFactor = 1;
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
