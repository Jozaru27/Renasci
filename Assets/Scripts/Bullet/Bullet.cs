using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    Vector3 shootDirection;

    private void Update()
    {
        transform.Translate(shootDirection * speed * Time.deltaTime, Space.World);
    }

    public void GetMousePosition(Vector3 mousePosition)
    {
        shootDirection = mousePosition.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

        }
    }
}
