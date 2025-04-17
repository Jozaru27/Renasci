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
        direction = setDirection;
    }
}
