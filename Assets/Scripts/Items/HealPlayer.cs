using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    [SerializeField] float heal;

    [SerializeField] AudioClip healClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(heal, transform.position, 0);
            UIManager.Instance.gameObject.GetComponent<AudioSource>().PlayOneShot(healClip, 0.5f);
            Destroy(this.gameObject);
        }
    }
}
