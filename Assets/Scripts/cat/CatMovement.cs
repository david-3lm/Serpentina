using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f; // Para que rote suavemente
    private Vector3 moveDirection;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Quaternion targetRotation;
    private bool facingRight = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(horizontal - vertical, 0, vertical + horizontal).normalized;

        animator.SetBool("isWalking", moveDirection != Vector3.zero);
        
        if (horizontal > 0 && !facingRight)
        {
            facingRight = true;
            targetRotation = Quaternion.Euler(0, -40, 0);
        }
        else if (horizontal < 0 && facingRight)
        {
            facingRight = false;
            targetRotation = Quaternion.Euler(0, 140, 0);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
