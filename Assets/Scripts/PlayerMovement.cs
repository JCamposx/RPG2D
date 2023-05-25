using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private float speed = 4f;
    [SerializeField] private Image fireIcon;
    [SerializeField] private Image swordIcon;
    #endregion

    #region ObjectComponents
    private Rigidbody2D mRb;
    private Vector3 mDirection = Vector3.zero;
    private Animator mAnimator;
    private PlayerInput mPlayerInput;
    private Transform hitBox;
    public CapsuleCollider2D mCollider;
    #endregion

    #region AttackFields
    private bool swordAttack = true;
    #endregion

    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootController;
    public char lastKey = 'S';

    private void Start()
    {
        mRb = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mPlayerInput = GetComponent<PlayerInput>();
        mCollider = GetComponent<CapsuleCollider2D>();

        hitBox = transform.Find("HitBox");

        ConversationManager.Instance.OnConversationStop += OnConversationStopDelegate;
    }

    private void OnConversationStopDelegate()
    {
        mPlayerInput.SwitchCurrentActionMap("Player");
    }

    private void Update()
    {
        if (mDirection != Vector3.zero)
        {
            mAnimator.SetBool("IsMoving", true);
            mAnimator.SetFloat("Horizontal", mDirection.x);
            mAnimator.SetFloat("Vertical", mDirection.y);
        }
        else
        {
            // Quieto
            mAnimator.SetBool("IsMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.W)) lastKey = 'W';
        else if (Input.GetKeyDown(KeyCode.A)) lastKey = 'A';
        else if (Input.GetKeyDown(KeyCode.S)) lastKey = 'S';
        else if (Input.GetKeyDown(KeyCode.D)) lastKey = 'D';

        if (Input.GetKeyDown(KeyCode.LeftControl)) toggleAttack();

        if (mCollider.IsTouchingLayers(LayerMask.GetMask("BossHitbox")))
        {
            Debug.Log("RECEIVE BOSS DAMAGE " + GameManager.Instance.BossDamage);
        }

        if (mCollider.IsTouchingLayers(LayerMask.GetMask("EnemyHitbox")))
        {
            Debug.Log("RECEIVE ENEMY DAMAGE " + GameManager.Instance.EnemyDamage);
        }
    }

    private void FixedUpdate()
    {
        mRb.MovePosition(
            transform.position + (mDirection * speed * Time.fixedDeltaTime)
        );
    }

    public void OnMove(InputValue value)
    {
        mDirection = value.Get<Vector2>().normalized;
    }

    public void OnNext(InputValue value)
    {
        if (value.isPressed)
        {
            ConversationManager.Instance.NextConversation();
        }
    }

    public void OnCancel(InputValue value)
    {
        if (value.isPressed)
        {
            ConversationManager.Instance.StopConversation();
        }
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            mAnimator.SetTrigger("Attack");
            hitBox.gameObject.SetActive(true);

            if (!swordAttack)
            {
                shoot();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Conversation conversation;
        if (other.transform.TryGetComponent<Conversation>(out conversation))
        {
            mPlayerInput.SwitchCurrentActionMap("Conversation");
            ConversationManager.Instance.StartConversation(conversation);
        }
    }

    public void DisableHitBox()
    {
        hitBox.gameObject.SetActive(false);
    }

    private void toggleAttack()
    {
        swordIcon.enabled = !swordAttack;
        fireIcon.enabled = swordAttack;

        swordAttack = !swordAttack;

        GameManager.Instance.PlayerDamage = (swordAttack) ? 1f : 0.5f;
    }

    public void shoot()
    {
        Instantiate(bullet, shootController.position, shootController.rotation);
    }
}
