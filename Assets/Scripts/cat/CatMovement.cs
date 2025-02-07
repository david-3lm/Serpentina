using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;
    private Vector3 moveDirection;
    private Quaternion targetRotation;

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
}
