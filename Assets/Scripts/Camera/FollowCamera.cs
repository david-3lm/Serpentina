using UnityEngine;

using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;  // Objeto a seguir (asigna en el inspector)
    public Vector3 offset = new Vector3(0, 5, -10); // Posición relativa a la cámara
    public float smoothSpeed = 5f; // Velocidad de seguimiento

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posición deseada
            Vector3 desiredPosition = target.position + offset;

            // Interpola suavemente hacia la posición deseada
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Opcional: Hacer que la cámara mire al objetivo
            transform.LookAt(target);
        }
    }
}
