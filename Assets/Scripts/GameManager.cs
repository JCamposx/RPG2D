using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float BossDamage = 2f;
    public float EnemyDamage = 1f;
    public float PlayerDamage = 1f;

    public static GameManager Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
    }
}
