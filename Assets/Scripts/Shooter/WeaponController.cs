using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform characterTransform; // Referencia al transform del personaje
    public Sprite[] sprites; // Arreglo de sprites para las distintas posiciones del arma
    public Vector3[] spriteLocalPositions; // Arreglo de posiciones locales personalizadas para cada sprite

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Obtener la dirección del movimiento del personaje
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcular la dirección diagonal del movimiento (si aplica)
        Vector2 direction = new Vector2(horizontal, vertical).normalized;

        // Calcular el ángulo de la dirección en grados
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Asegurarse de que el ángulo sea positivo
        if (angle < 0f)
        {
            angle += 360f;
        }

        // Calcular el índice del sprite según el ángulo
        int spriteIndex = 6;
        if ((angle >= 337.5f && angle <= 360f) || (angle >= 0f && angle < 22.5f))
        {
            spriteIndex = 0; // Derecha
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            spriteIndex = 1; // Diagonal arriba-derecha
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            spriteIndex = 2; // Arriba
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            spriteIndex = 3; // Diagonal arriba-izquierda
        }
        else if (angle >= 157.5f && angle < 202.5f)
        {
            spriteIndex = 4; // Izquierda
        }
        else if (angle >= 202.5f && angle < 247.5f)
        {
            spriteIndex = 5; // Diagonal abajo-izquierda
        }
        else if (angle >= 247.5f && angle < 292.5f)
        {
            spriteIndex = 6; // Abajo
        }
        else if (angle >= 292.5f && angle < 337.5f)
        {
            spriteIndex = 7; // Diagonal abajo-derecha
        }

        // Asignar el sprite correspondiente al spriteRenderer del arma
        spriteRenderer.sprite = sprites[spriteIndex];

        // Almacenar el último índice del sprite mostrado cuando el personaje estaba en movimiento
        if (direction.magnitude > 0.1f)
        {
            currentSpriteIndex = spriteIndex;
        }

        // Mostrar el último sprite mostrado si el personaje está quieto
        if (direction.magnitude < 0.1f)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }

        // Ajustar la posición local del arma según el sprite actual
        if (currentSpriteIndex >= 0 && currentSpriteIndex < spriteLocalPositions.Length)
        {
            // Sumar la posición local personalizada al transform.position del personaje
            transform.position = characterTransform.position + spriteLocalPositions[currentSpriteIndex];
        }
    }
}

