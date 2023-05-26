using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float BossDamage = 2f;
    public float EnemyDamage = 0.5f;
    public float PlayerDamage = 1f;
    public bool IsPlayerDead = false;
    public bool bossHadSeenPlayer = false;
    public GameObject Player;
    public GameObject Boss;
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    public static GameManager Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (IsPlayerDead)
        {
            Player.SetActive(false);
            Player.GetComponent<PlayerMovement>().GameOverScreen.SetActive(true);

            bossHadSeenPlayer = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Player.transform.position = Player.GetComponent<PlayerMovement>().initialPosition;
                Player.GetComponent<PlayerMovement>().GameOverScreen.SetActive(false);
                Player.gameObject.SetActive(true);
                Player.GetComponent<PlayerMovement>().mHealthBar.value = 40f;
                IsPlayerDead = false;

                Boss.transform.position = Boss.GetComponent<EnemyController>().initialPosition;

                foreach (GameObject spawnedEnemy in spawnedEnemies)
                {
                    Destroy(spawnedEnemy.gameObject);
                }

                spawnedEnemies.Clear();
            }
        }
    }
}
