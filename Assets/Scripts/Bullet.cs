using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 8f;
    private PlayerMovement player;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        if (player.lastKey == 'W')
        {
            transform.position = player.transform.position + new Vector3(0f, 0.95f, 0f);
            transform.Rotate(0f, 0f, 180f);
        }
        else if (player.lastKey == 'A')
        {
            transform.Rotate(0f, 0f, -90f);
            transform.position = player.transform.position + new Vector3(-0.95f, 0f, 0f);
        }
        else if (player.lastKey == 'S')
        {
            transform.position = player.transform.position + new Vector3(0f, -0.95f, 0f);
            transform.Rotate(0f, 0f, 0f);
        }
        else if (player.lastKey == 'D')
        {
            transform.position = player.transform.position + new Vector3(0.95f, 0f, 0f);
            transform.Rotate(0f, 0f, 90f);
        }

        Invoke("DestroyGameObject", 10f);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        Invoke("DestroyGameObject", 0.01f);
    }
}
