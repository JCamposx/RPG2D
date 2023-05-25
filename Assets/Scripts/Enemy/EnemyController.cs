using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    #region Public Properties
    public float WakeDistance = 5f;
    public float Speed = 2f;
    public float AttackDistance = 0.7f;
    public bool AttackingEnd { set; get; } = false;
    public bool InvokingEnd { set; get; } = false;
    public float InvokingInterval { set; get; } = 4f;
    public float InvokerTime { set; get; } = 0f;
    public bool IsBoss;
    private float health;
    private int hitCount;
    #endregion

    #region Components
    public Transform Player;
    public SpriteRenderer spriteRenderer { private set; get; }
    public Rigidbody2D rb { private set; get; }
    public Animator animator { private set; get; }
    public SpawnObjects spawnObjects { set; get; }
    public Transform hitBox { private set; get; }
    public CapsuleCollider2D mCollider;
    #endregion

    #region Private Properties
    private FSM<EnemyController> mFSM;
    #endregion

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawnObjects = GetComponent<SpawnObjects>();
        hitBox = transform.Find("HitBox");
        mCollider = GetComponent<CapsuleCollider2D>();

        animator.SetFloat("Horizontal", 0f);
        animator.SetFloat("Vertical", -1f);

        health = (IsBoss) ? 1000f : 1f;

        // Creo la maquina de estado finita
        mFSM = new FSM<EnemyController>(new Enemy.IdleState(this));
        mFSM.Begin();  // prendo la mquina de estados
    }

    private void Update()
    {
        if (mCollider.IsTouchingLayers(LayerMask.GetMask("PlayerHitbox")))
        {
            if (!IsBoss)
            {
                health -= GameManager.Instance.PlayerDamage;

                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        mFSM.Tick(Time.fixedDeltaTime);
    }

    public void SetAttackingEnd()
    {
        AttackingEnd = true;
    }

    public void SetInvokingEnd()
    {
        InvokingEnd = true;
    }
}
