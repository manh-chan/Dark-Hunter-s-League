using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform Target;

    private float shootRate;
    private float shootTime;

    private void Update()
    {
        shootTime -= Time.deltaTime;
        if (shootTime <= 0) { 
            shootTime = shootRate;
            Instantiate(projectilePrefab,transform.position,Quaternion.identity);
        }
    }
}
