using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    Vector3 shotDirection;

    private void Update()
    {
        transform.Translate(shotDirection * speed * Time.deltaTime, Space.World);
    }

    public void GetMousePosition(Vector3 worldShotDirection)
    {
        shotDirection = worldShotDirection.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            other.gameObject.GetComponent<IDamageable>().TakeDamage(-0.5f);

        Destroy(gameObject);
    }
}
