using UnityEngine;
using UnityEngine.InputSystem;

public class ControlMaxima : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float velocidadBici = 9f;
    public float fuerzaSalto = 10f;
    public float fuerzaTrampolin = 20f;

    [Header("Referencias")]
    public Transform sensorSuelo;
    public LayerMask capaSuelo;
    public GestorVidas gestor;

    [Header("Estado")]
    public bool estaEnBicicleta = false;
    public bool esInvencible = false;

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
        inputX = 0f;

        if (Keyboard.current.rightArrowKey.isPressed) inputX = 1f;
        else if (Keyboard.current.leftArrowKey.isPressed) inputX = -1f;

        if (inputX > 0) spriteRenderer.flipX = false;
        else if (inputX < 0) spriteRenderer.flipX = true;

        float vel = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("velocidad", vel);
        animator.SetBool("enBicicleta", estaEnBicicleta);

        if (estaEnBicicleta)
            animator.speed = (vel > 0.1f) ? 1.0f : 0.0f;
        else
            animator.speed = 1.0f;

        estaEnSuelo = Physics2D.OverlapCircle(sensorSuelo.position, 0.1f, capaSuelo);

        if (Keyboard.current.upArrowKey.wasPressedThisFrame && estaEnSuelo && !estaEnBicicleta)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }

        // USAR ASADO
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            InventarioManager.instancia.UsarAsado(transform.position);
        }

        animator.SetBool("isJumping", !estaEnSuelo && !estaEnBicicleta);
    }

    void FixedUpdate()
    {
        float velX = estaEnBicicleta ? velocidadBici : velocidad;
        rb.linearVelocity = new Vector2(inputX * velX, rb.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Reina") || collision.CompareTag("Toro")) && !esInvencible)
        {
            if (estaEnBicicleta)
            {
                estaEnBicicleta = false;
                StartCoroutine(InvencibilidadTemporal());
            }
            else if (gestor != null)
            {
                gestor.PerderVida(this.gameObject);
            }
        }

        if (collision.CompareTag("Bicicleta"))
        {
            StartCoroutine(ActivarBicicletaTemporal(collision.gameObject));
        }

        if (collision.CompareTag("Trampolin"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * fuerzaTrampolin, ForceMode2D.Impulse);
        }

        if (collision.CompareTag("Meta"))
        {
            if (gestor != null)
                gestor.NivelCompletado(this.gameObject);
        }
    }

    System.Collections.IEnumerator ActivarBicicletaTemporal(GameObject bicicleta)
    {
        estaEnBicicleta = true;
        bicicleta.SetActive(false);

        yield return new WaitForSeconds(5f);

        estaEnBicicleta = false;
    }

    System.Collections.IEnumerator InvencibilidadTemporal()
    {
        esInvencible = true;
        yield return new WaitForSeconds(1.0f);
        esInvencible = false;
    }
}