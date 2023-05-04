using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        private float moveInput;

        private bool facingRight = false;
        [HideInInspector]
        public bool deathState = false;

        private bool isGrounded;
        public Transform groundCheck;

        private Rigidbody2D rigidbody;
        private Animator animator;
        private GameManager gameManager;

        public float dashForce = 10f;
        public float dashDuration = 0.2f;
        private bool isDashing;
        private float dashTime;

        public float extraJumpForce = 3f;
        private bool extraJumpUsed;

        public TrailRenderer trailRenderer;

        private bool doubleJumpUnlocked = false;
        private bool dashUnlocked = false;

       

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        private void FixedUpdate()
        {
            CheckGround();
            
        }


        void Update()
        {
            if (Input.GetButton("Horizontal"))
            {
                moveInput = Input.GetAxis("Horizontal");
                Vector3 direction = transform.right * moveInput;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                animator.SetInteger("playerState", 1); // Turn on run animation
            }
            else
            {
                if (isGrounded) animator.SetInteger("playerState", 0); // Turn on idle animation
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }

            if (!isGrounded) animator.SetInteger("playerState", 2); // Turn on jump animation

            if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput < 0)
            {
                Flip();
            }

            // Double jump logic
            if (isGrounded)
            {
                extraJumpUsed = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && !extraJumpUsed && doubleJumpUnlocked)
            {
                extraJumpUsed = true;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                rigidbody.AddForce(transform.up * (jumpForce * 1.2f), ForceMode2D.Impulse);
            }

            // Dash logic
            if (Input.GetKeyDown(KeyCode.LeftShift) && dashUnlocked)
            {
                StartCoroutine(Dash());
            }

           
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);
            isGrounded = colliders.Length > 1;
        }

        private IEnumerator Dash()
        {
            isDashing = true;
            trailRenderer.emitting = true;
            float dashDirection = facingRight ? 1 : -1;
            rigidbody.velocity = new Vector2(dashForce * dashDirection, rigidbody.velocity.y);
            dashTime = dashDuration;

            while (dashTime > 0)
            {
                dashTime -= Time.deltaTime;
                yield return null;
            }

            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            trailRenderer.emitting = false;
            isDashing = false;
        }

        public void UnlockSkill(string skillName)
        {
            switch (skillName)
            {
                case "DoubleJump":
                    doubleJumpUnlocked = true;
                    break;
                case "Dash":
                    dashUnlocked = true;
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                deathState = true; // Say to GameManager that player is dead
            }
            else
            {
                deathState = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                CoinCollector coinCollector = FindObjectOfType<CoinCollector>();
                coinCollector.CollectCoin();
                Destroy(other.gameObject);
            }
        }

    }
}
