using UnityEngine;

public class PersecucionReina : MonoBehaviour
{
    [Header("Configuración")]
    public Transform objetivo; // Acá vamos a arrastrar a Máxima
    public float velocidad = 3.5f; // Velocidad constante de la Reina

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Obtenemos los componentes de la Reina al arrancar
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // Si por alguna razón Máxima no está asignada, no hace nada para evitar errores
        if (objetivo == null) return;

        // 1. Calculamos la dirección resta de vectores: (Posición Destino - Posición Actual)
        Vector2 direccion = (objetivo.position - transform.position).normalized;

        // 2. Si el juego es de plataformas (2D lateral), ignoramos el eje Y para que no vuele.
        // Descomentá la línea de abajo borrando las dos barras "//" si solo querés que se mueva de izquierda a derecha.
         direccion.y = 0;

        // 3. Aplicamos el movimiento constante usando el Rigidbody
        rb.linearVelocity = new Vector2(direccion.x * velocidad, 0);

        // 4. Voltear el sprite de la Reina para que mire a donde camina
        if (direccion.x > 0)
        {
            spriteRenderer.flipX = false; // Mira a la derecha
        }
        else if (direccion.x < 0)
        {
            spriteRenderer.flipX = true; // Mira a la izquierda
        }
    }
}