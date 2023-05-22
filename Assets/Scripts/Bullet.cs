using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 8f;
    private Vector2 direction;
    private PlayerMovement player;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        if (player.lastKey == 'W')
        {
            direction = Vector2.up;
            transform.position = player.transform.position + new Vector3(0f, 0.95f, 0f);
        }
        else if (player.lastKey == 'A')
        {
            direction = Vector2.left;
            transform.position = player.transform.position + new Vector3(-0.95f, 0f, 0f);
        }
        else if (player.lastKey == 'S')
        {
            direction = Vector2.down;
            transform.position = player.transform.position + new Vector3(0f, -0.95f, 0f);
        }
        else if (player.lastKey == 'D')
        {
            direction = Vector2.right;
            transform.position = player.transform.position + new Vector3(0.95f, 0f, 0f);
        }

        Invoke("DestroyGameObject", 10f);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        Destroy(gameObject);
    }
}
