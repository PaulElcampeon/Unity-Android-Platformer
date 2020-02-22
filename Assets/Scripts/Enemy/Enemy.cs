using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    protected bool isHit;
    protected bool isDead = false;

    protected Player player;

    public virtual void Initialize()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        Initialize();
    }

    public virtual void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle") && animator.GetBool("InCombat") == false)
        {
            return;
        }

        if (!isDead)
        {
            Movement();
        }

        ComeOutOfCombatIfPlayerIsDead();
    }

    public virtual void Movement()
    {
        if (currentTarget == pointA.position)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            animator.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            animator.SetTrigger("Idle");
        }
        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        float distance = Vector3.Distance(player.transform.localPosition, transform.localPosition);
        Vector3 direction = player.transform.localPosition - transform.localPosition;

        if (direction.x > 0 && animator.GetBool("InCombat"))
        {
            spriteRenderer.flipX = false;

        }
        else if (direction.x < 0 && animator.GetBool("InCombat"))
        {
            spriteRenderer.flipX = true;
        }

        CheckIfPlayerIsStillInCombatZone();
    }

    private void CheckIfPlayerIsStillInCombatZone()
    {
        float distance = Vector3.Distance(transform.localPosition, player.gameObject.transform.localPosition);
        if (distance >= 2)
        {
            ComeOutOfCombat();
        }
    }

    private void ComeOutOfCombatIfPlayerIsDead()
    {
        if(player.isDead)
        {
            ComeOutOfCombat();
        }
    }

    private void ComeOutOfCombat()
    {
        if (animator.GetBool("InCombat"))
        {
            isHit = false;
            animator.SetBool("InCombat", false);
        }
    }
}
