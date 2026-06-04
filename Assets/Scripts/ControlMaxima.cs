using UnityEngine;
using UnityEngine.InputSystem;

public class ControlMaxima : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 10f;

    [Header("Referencias")]
    public Transform sensorSuelo;
    public LayerMask capaSuelo;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float inputX;
    private bool estaEnSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Movimiento Horizontal
        inputX = 0f;
        if (Keyboard.current.rightArrowKey.isPressed) inputX = 1f;
        else if (Keyboard.current.leftArrowKey.isPressed) inputX = -1f;

        // Orientación del Sprite
        if (inputX > 0) spriteRenderer.flipX = false;
        else if (inputX < 0) spriteRenderer.flipX = true;

        // Animación
        animator.SetFloat("Speed", Mathf.Abs(inputX));

        // Salto
        estaEnSuelo = Physics2D.OverlapCircle(sensorSuelo.position, 0.2f, capaSuelo);
        if (Keyboard.current.upArrowKey.wasPressedThisFrame && estaEnSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputX * velocidad, rb.linearVelocity.y);
    }
}