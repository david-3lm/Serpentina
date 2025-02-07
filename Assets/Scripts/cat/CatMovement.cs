using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 moveDirection;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        moveDirection = new Vector3(horizontal - vertical, 0, vertical + horizontal).normalized;
        transform.position += moveDirection * speed * Time.deltaTime;

        if (moveDirection != Vector3.zero)
        {
            
        }
    }
}
