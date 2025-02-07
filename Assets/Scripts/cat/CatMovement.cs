using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 moveDirection;

    void Update()
    {
        // Leer input del teclado
        float horizontal = Input.GetAxisRaw("Horizontal"); // A (-1), D (+1)
        float vertical = Input.GetAxisRaw("Vertical");     // W (+1), S (-1)

        // Convertir input a coordenadas de una vista superior
        moveDirection = new Vector3(horizontal - vertical, 0, vertical + horizontal).normalized;

        // Aplicar el movimiento
        transform.position += moveDirection * speed * Time.deltaTime;
    }

}
