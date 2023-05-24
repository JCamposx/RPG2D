using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Transform player;
    private Animator mAnimator;
    private float spawnInterval = 8f;
    private float enemiesQuantity = 4f;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        InvokeRepeating("SpawnEnemies", spawnInterval, spawnInterval);
    }

    void SpawnEnemies()
    {
        mAnimator.SetTrigger("Invoke");
        for (int i = 0; i < enemiesQuantity; i++)
        {
            Vector2 posicionSpawn = GetSpawnPosition(i);
            GameObject prefabInstance = Instantiate(enemy, posicionSpawn, Quaternion.identity);
            prefabInstance.GetComponent<EnemyController>().Player = player;
        }
    }

    Vector2 GetSpawnPosition(int indice)
    {
        float distancia = 0.95f;
        Vector2 posicionPadre = transform.position;

        switch (indice)
        {
            case 0: // Arriba
                return new Vector2(posicionPadre.x, posicionPadre.y + distancia);
            case 1: // Derecha
                return new Vector2(posicionPadre.x + distancia, posicionPadre.y);
            case 2: // Abajo
                return new Vector2(posicionPadre.x, posicionPadre.y - distancia);
            case 3: // Izquierda
                return new Vector2(posicionPadre.x - distancia, posicionPadre.y);
            default:
                return posicionPadre;
        }
    }
}
