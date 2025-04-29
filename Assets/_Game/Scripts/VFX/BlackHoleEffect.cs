using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleEffect : MonoBehaviour
{
    public float start = 1;
    void Start()
    {
        Destroy(gameObject, start);
    }
}
