using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Referencia al transform del personaje a seguir
    public float smoothSpeed = 0.125f; // Velocidad de suavizado del seguimiento
    public Vector3 offset; // Desplazamiento de la cámara respecto al personaje

    private void FixedUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Mantenemos la coordenada Z de la cámara sin cambios
        smoothedPosition.z = transform.position.z;

        transform.position = smoothedPosition;
    }
}
