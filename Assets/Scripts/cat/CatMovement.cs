using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 10f;
    private float currentSpeed;

    private IA_Patrol[] enemies;

    private Vector3 moveDirection;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Quaternion targetRotation;
    private bool facingRight = true;
    
    [SerializeField] private Transform zonaFinal;
    [SerializeField] private float distanciaMinima = 7f;
    [SerializeField] private GatoInteractuar gatoInteractuar; 

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSpeed = normalSpeed;

        enemies = FindObjectsOfType<IA_Patrol>();

        foreach (IA_Patrol enemy in enemies)
        {
            enemy.OnPlayerDetected += StartRunning;
            enemy.OnPlayerLost += StopRunning;
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(horizontal - vertical, 0, vertical + horizontal).normalized;

        animator.SetBool("isWalking", moveDirection != Vector3.zero);

        if (horizontal > 0 && vertical > 0)
        {
            animator.SetBool("backWalk", true);
            targetRotation = Quaternion.Euler(0, 140, 0);
        }
        else if (horizontal > 0)
        {
            facingRight = true;
            animator.SetBool("backWalk", false);
            targetRotation = Quaternion.Euler(0, -40, 0);
        }
        else if (vertical > 0)
        {
            if (horizontal < 0)
                targetRotation = Quaternion.Euler(0, -12, 0);
            else
                targetRotation = Quaternion.Euler(0, 140, 0);
            animator.SetBool("backWalk", true);
        }
        else if (horizontal < 0)
        {
            facingRight = false;
            animator.SetBool("backWalk", false);
            targetRotation = Quaternion.Euler(0, 140, 0);
        }
        else if (vertical < 0)
        {
            facingRight = true;
            animator.SetBool("backWalk", false);
            targetRotation = Quaternion.Euler(0, 140, 0);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position += moveDirection * currentSpeed * Time.deltaTime;
        
        if (gatoInteractuar != null && gatoInteractuar.hasFish == true && Vector3.Distance(transform.position, zonaFinal.position) < distanciaMinima)
        {
            SceneManager.LoadScene("Victoria");
        }
    }

    private void StartRunning()
    {
        currentSpeed = runSpeed;
    }

    private void StopRunning()
    {
        currentSpeed = normalSpeed;
    }

    void OnDestroy()
    {
        foreach (IA_Patrol enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.OnPlayerDetected -= StartRunning;
                enemy.OnPlayerLost -= StopRunning;
            }
        }
    }
}
