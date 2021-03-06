﻿using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D rgb;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private LayerMask groundLayer;
    private bool resetJumpNeeded = false;
    private PlayerAnimation playerAnimation;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer swordArcSpriteRenderer;
    private bool grounded = false;
    public bool isDead = false;
    public int diamondCount;
    [SerializeField]
    public int Health { get; set; }

    public bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        swordArcSpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    // Update is called once per frame

    void Update()
    {
        if (!isDead)
        {
            Movement();
            Attack();
        }
    }

    public void Movement()
    {
        float move = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        grounded = IsGrounded();
        Flip(move);
        playerAnimation.Move(move);

        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && IsGrounded())
        {
            rgb.velocity = new Vector2(rgb.velocity.x, jumpForce);
            playerAnimation.Jump(true);
            StartCoroutine(ResetJumpNeededCo());
        }
        rgb.velocity = new Vector2(move * speed, rgb.velocity.y);
    }

    public bool IsGrounded()
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down * 0.8f, Color.green);

        if (hit.collider != null)
        {
            if (!resetJumpNeeded)
            {
                playerAnimation.Jump(false);
                return true;
            }
        }
        return false;
    }

    public IEnumerator ResetJumpNeededCo()
    {
        resetJumpNeeded = true;
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }

    public void Flip(float move)
    {
        if (move < 0) {
            spriteRenderer.flipX = true;

            swordArcSpriteRenderer.flipX = true;
            swordArcSpriteRenderer.flipY = true;
            Vector3 newPos = swordArcSpriteRenderer.transform.localPosition;
            newPos.x = -1.01f;
            swordArcSpriteRenderer.transform.localPosition = newPos;
        }

        if (move > 0) {
            spriteRenderer.flipX = false;

            swordArcSpriteRenderer.flipX = false;
            swordArcSpriteRenderer.flipY = false;
            Vector3 newPos = swordArcSpriteRenderer.transform.localPosition;
            newPos.x = 1.01f;
            swordArcSpriteRenderer.transform.localPosition = newPos;
        }
    }

    public void Attack()
    {

        if(CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded() && canAttack)
        {
            playerAnimation.Attack();
        }
    }

    public void Damage()
    {
        Health--;
        UIManager.Instance.UpdateLives(Health);
        if (Health <= 0)
        {
            playerAnimation.Death();
            isDead = true;
        } else
        {
            playerAnimation.Hit();
        }
    }

    public void AddGems(int amount)
    {
        diamondCount += amount;
        UIManager.Instance.UpdateGemCount(diamondCount);
    }
}
