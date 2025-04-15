using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletDarkness : MonoBehaviour
{
    public float cooldownTime = 0f;
    public float speed = 0f;
    public float damageAmount = 20f;

    private Vector3 moveDirection;
    void Start()
    {
        PlayerMovement[] allPlayers = FindObjectsOfType<PlayerMovement>();
        if (allPlayers.Length > 0)
        {
            Transform selectedPlayer = allPlayers[Random.Range(0, allPlayers.Length)].transform;

            moveDirection = (selectedPlayer.position - transform.position).normalized;

            transform.right = -moveDirection;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTime -= Time.deltaTime;

        BulletSpeed();

        if (cooldownTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
    private void BulletSpeed()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats character = collision.GetComponent<PlayerStats>();
        if (character != null) 
        {
            character.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
