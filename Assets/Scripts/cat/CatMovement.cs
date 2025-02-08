using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float originalSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float speedBoost = 10f; // Aumento de velocidad
    [SerializeField] private IA_Patrol ia_patrol;
    
    private Vector3 moveDirection;
    private Quaternion targetRotation;

    void Start()
    {
        if (ia_patrol != null)
        {
            ia_patrol.OnPlayerDetected += IncreaseSpeed;
            ia_patrol.OnPlayerLost += ResetSpeed;
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(horizontal - vertical, 0, vertical + horizontal).normalized;

        if (horizontal != 0)
        {
            float angle = horizontal < 0 ? 132f : -48f;
            targetRotation = Quaternion.Euler(0, angle, 0);
        }

        if (Quaternion.Angle(transform.rotation, targetRotation) > 180f)
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y - 360f, 0);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void IncreaseSpeed()
    {
        speed = speedBoost;
    }

    void ResetSpeed()
    {
        speed = originalSpeed;
    }
    
    void OnDestroy()
    {
        if (ia_patrol != null)
        {
            ia_patrol.OnPlayerDetected -= IncreaseSpeed;
        }
    }
}