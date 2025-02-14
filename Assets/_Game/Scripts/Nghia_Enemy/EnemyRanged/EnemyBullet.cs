using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 10f;

    private Vector2 targetDirection;

    public void Initialize(Vector2 targetPosition)
    {
        targetDirection = (targetPosition - (Vector2)transform.position).normalized;
        Destroy(gameObject, 5f); // Hủy đạn sau 5 giây nếu không trúng mục tiêu
    }

    void Update()
    {
        transform.position += (Vector3)targetDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
