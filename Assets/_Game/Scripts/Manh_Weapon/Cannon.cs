using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Cannon : MonoBehaviour
{
    public float orbitRadius = 2f; // Bán kính quay quanh player
    public float orbitSpeed = 2f; // Tốc độ quay quanh player

    private float orbitAngle = 0f;
    private PlayerMovement player; // Tham chiếu đến player
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    protected  void Update()
    {
        OrbitAroundPlayer();
    }

    private void OrbitAroundPlayer()
    {
        if (player == null) return;
        orbitAngle += orbitSpeed * Time.deltaTime;
        float x = player.gameObject.transform.position.x + Mathf.Cos(orbitAngle) * orbitRadius;
        float y = player.gameObject.transform.position.y + Mathf.Sin(orbitAngle) * orbitRadius;
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
