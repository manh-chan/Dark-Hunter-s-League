using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleEffect : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1);
    }
}
