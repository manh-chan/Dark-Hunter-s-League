using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosaryWeapon : MonoBehaviour
{
    public Transform player; // Tham chiếu đến người chơi
    public float orbitSpeed = 5f; // Tốc độ quay
    public float maxRadius = 3f; // Bán kính tối đa
    public float expansionSpeed = 2f; // Tốc độ bay ra
    private float currentRadius = 0f;
    private float angle = 0f;
    private bool expanding = true; // Đang bay ra hay bay vào

    void Update()
    {
        if (player == null) return;

        // Tính toán vị trí quay quanh người chơi
        angle += orbitSpeed * Time.deltaTime;
        float x = Mathf.Cos(angle) * currentRadius;
        float y = Mathf.Sin(angle) * currentRadius;
        transform.position = player.position + new Vector3(x, y, 0);

        // Điều chỉnh bán kính
        if (expanding)
        {
            currentRadius += expansionSpeed * Time.deltaTime;
            if (currentRadius >= maxRadius) expanding = false;
        }
        else
        {
            currentRadius -= expansionSpeed * Time.deltaTime;
            if (currentRadius <= 0) Destroy(gameObject); // Biến mất khi về 0
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Gây sát thương cho enemy
            //collision.GetComponent<Enemy>().TakeDamage(10);
            Debug.Log("damage");
        }
    }
}
