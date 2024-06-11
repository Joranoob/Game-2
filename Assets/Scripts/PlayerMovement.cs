using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D me;
    public SpriteRenderer SpriteRenderer;
    public Sprite Standing;
    public CapsuleCollider2D Collider;
    public Vector2 StandingSize;

    private Rigidbody2D rb;
    private CapsuleCollider2D collCap;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] public float moveSpeed = 6f;
    [SerializeField] public float jumpForce = 12f;
    [SerializeField] private AudioSource jumpSoundEffect;

    private float dirX = 0f;
    private bool DoubleJump = false;
    private bool isGrounded = false;
    private bool doubleJumpEnabled = false;

    private enum MovementState { idle, run, jump, fall }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collCap = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = Standing;
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (IsGrounded())
        {
            isGrounded = true;
            DoubleJump = doubleJumpEnabled;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (DoubleJump)
            {
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                DoubleJump = false;
            }
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.run;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.run;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.fall;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.CapsuleCast(collCap.bounds.center, collCap.bounds.size, CapsuleDirection2D.Vertical, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void EnableDoubleJump(float duration)
    {
        StartCoroutine(DoubleJumpPowerUp(duration));
    }

    private IEnumerator DoubleJumpPowerUp(float duration)
    {
        doubleJumpEnabled = true;
        DoubleJump = true;

   
        yield return new WaitForSeconds(duration);


        doubleJumpEnabled = false;
        DoubleJump = false;
    }
}