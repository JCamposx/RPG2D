using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Transform player;
    private Animator mAnimator;
    private float enemiesQuantity = 4f;
    private bool spawnDiagonal = false;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemiesQuantity; i++)
        {
            if (spawnDiagonal)
            {
                Vector2 posicionSpawn = GetSpawnPositionDiagonal(i);

                GameObject prefabInstance = Instantiate(enemy, posicionSpawn, Quaternion.identity);

                prefabInstance.transform.Find("HitBox").gameObject.layer = LayerMask.NameToLayer("EnemyHitbox");

                prefabInstance.GetComponent<EnemyController>().Player = player;
            }
            else
            {
                Vector2 posicionSpawn = GetSpawnPositionVerticalAndHorizontal(i);
                GameObject prefabInstance = Instantiate(enemy, posicionSpawn, Quaternion.identity);
                prefabInstance.GetComponent<EnemyController>().Player = player;
            }
        }

        spawnDiagonal = !spawnDiagonal;
    }

    Vector2 GetSpawnPositionVerticalAndHorizontal(int index)
    {
        float distancia = 0.95f;
        Vector2 bossPosition = transform.position;

        switch (index)
        {
            case 0: // Up
                return new Vector2(bossPosition.x, bossPosition.y + distancia);
            case 1: // Right
                return new Vector2(bossPosition.x + distancia, bossPosition.y);
            case 2: // Down
                return new Vector2(bossPosition.x, bossPosition.y - distancia);
            case 3: // Left
                return new Vector2(bossPosition.x - distancia, bossPosition.y);
            default:
                return bossPosition;
        }
    }

    Vector2 GetSpawnPositionDiagonal(int index)
    {
        float distancia = 0.95f;
        Vector2 bossPosition = transform.position;

        switch (index)
        {
            case 0: // Up left
                return new Vector2(bossPosition.x - distancia, bossPosition.y + distancia);
            case 1: // Up Right
                return new Vector2(bossPosition.x + distancia, bossPosition.y + distancia);
            case 2: // Down Right
                return new Vector2(bossPosition.x + distancia, bossPosition.y - distancia);
            case 3: // Down Left
                return new Vector2(bossPosition.x - distancia, bossPosition.y - distancia);
            default:
                return bossPosition;
        }
    }
}
