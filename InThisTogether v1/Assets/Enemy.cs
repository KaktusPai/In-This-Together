using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public int MoveSpeed = 4;
    public int MinDist = 4;
    public float MaxDist = 0.1f;

    public int health = 20;

    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(player);

        if (Vector3.Distance(transform.position, player.position) <= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, player.position) <= MaxDist)
            {
                BottomPlayerMovement.health -= 1;
                Destroy(this.gameObject);
            }

        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "Bullet")
        {
            health -= 1;
            Destroy(other.gameObject);
        }
    }
}
