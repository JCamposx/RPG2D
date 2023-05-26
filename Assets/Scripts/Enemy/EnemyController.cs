using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    #region Public Properties
    public float WakeDistance = 5f;
    public float Speed = 2f;
    public float AttackDistance = 0.7f;
    public bool AttackingEnd { set; get; } = false;
    public bool TpingEnd { set; get; } = false;
    public bool InvokingEnd { set; get; } = false;
    public float TpingInterval { set; get; } = 6f;
    public float TpTime { set; get; } = 0f;
    public bool IsTping { set; get; } = false;
    public float InvokingInterval { set; get; } = 4f;
    public float InvokerTime { set; get; } = 0f;
    public bool IsBoss;
    private float health;
    public Vector3 initialPosition;
    #endregion

    #region Components
    public Transform Player;
    public SpriteRenderer spriteRenderer { private set; get; }
    public Rigidbody2D rb { private set; get; }
    public Animator animator { private set; get; }
    public SpawnObjects spawnObjects { set; get; }
    public Transform hitBox { private set; get; }
    public CapsuleCollider2D mCollider;
    public Slider bossHealthBar;
    public bool bossCanReceiveDamage = true;
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

        rb.isKinematic = true;

        if (IsBoss)
        {
            bossHealthBar.value = 30f;
            health = 10000f;
            initialPosition = new Vector3(-7.768f, -0.93f, 0f);
        }
        else
        {
            health = 1f;
        }

        // Creo la maquina de estado finita
        mFSM = new FSM<EnemyController>(new Enemy.IdleState(this));
        mFSM.Begin();  // prendo la mquina de estados
    }

    private void Update()
    {
        if (mCollider.IsTouchingLayers(LayerMask.GetMask("PlayerHitbox")))
        {
            GameManager.Instance.bossHadSeenPlayer = true;
            ManageGetDamage();
        }
    }

    private void ManageGetDamage()
    {
        if (IsBoss)
        {
            if (!bossCanReceiveDamage) return;

            bossHealthBar.value -= GameManager.Instance.PlayerDamage;
            bossCanReceiveDamage = false;
            Invoke("SetBossCanReceiveDamage", 0.5f);
        }
        else
        {
            health -= GameManager.Instance.PlayerDamage;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

    private void SetBossCanReceiveDamage()
    {
        bossCanReceiveDamage = true;
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

    public void SetTpingEnd()
    {
        TpingEnd = true;
    }
}
