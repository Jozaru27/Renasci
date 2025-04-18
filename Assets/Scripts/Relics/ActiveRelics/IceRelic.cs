using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRelic : MonoBehaviour
{
    [SerializeField] float speed;

    Vector3 direction;

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void GetDirection(Vector3 setDirection)
    {
        direction = setDirection.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        StartCoroutine(AttackDissappears());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            other.gameObject.GetComponent<StateEffect>().GetFreeze();

        //if (!other.gameObject.CompareTag("Player"))
        //    Destroy(this.gameObject);
    }

    IEnumerator AttackDissappears()
    {
        yield return new WaitForSeconds(5);

        Destroy(this.gameObject);
    }
}
