using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    Vector3 shotDirection;

    private void Update()
    {
        transform.Translate(shotDirection * speed * Time.deltaTime, Space.World);
    }

    public void GetShotDirection(Vector3 worldShotDirection)
    {
        shotDirection = worldShotDirection.normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float randomNum = Random.Range(0, 100);

            if (randomNum <= StatsManager.Instance.criticalChance)
                collision.gameObject.GetComponent<IDamageable>().TakeDamage((-0.5f * StatsManager.Instance.damageMultiplyer), false);
            else
                collision.gameObject.GetComponent<IDamageable>().TakeDamage(-0.5f, false);
        }

        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Trap"))
            Destroy(gameObject);
    }
}
