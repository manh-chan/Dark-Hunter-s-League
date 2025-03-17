using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedHPBar : MonoBehaviour
{
    public Transform player; // Gán Player vào đây
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
