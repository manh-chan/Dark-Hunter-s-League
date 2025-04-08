using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public Texture2D[] textures; // Mảng chứa các texture
    public int brickCount = 16; // Số lượng viên gạch
    public float radius = 5f; // Bán kính vòng tròn

    void Start()
    {
        SpawnCircle();
    }

    void SpawnCircle()
    {
        if (textures.Length == 0)
        {
            Debug.LogError("Bạn chưa gán Texture2D nào!");
            return;
        }

        for (int i = 0; i < brickCount; i++)
        {
            float angle = i * Mathf.PI * 2 / brickCount; // Góc tính theo radian
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            Vector3 position = new Vector3(x, y, 0) + transform.position; // Tọa độ

            // Tạo GameObject mới
            GameObject brick = new GameObject("Brick_" + i);
            brick.transform.position = position;
            brick.transform.parent = transform;

            // Thêm SpriteRenderer và gán texture
            SpriteRenderer renderer = brick.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite.Create(textures[i % textures.Length],
                                            new Rect(0, 0, textures[i % textures.Length].width, textures[i % textures.Length].height),
                                            new Vector2(0.5f, 0.5f));
        }
    }
}
