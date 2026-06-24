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
    public GameObject visualEscudo; // Arrastrá acá el objeto VisualEscudo (hijo de Máxima)

    [Header("Estado")]
    public bool estaEnBicicleta = false;
    public bool esInvencible = false;
    public bool aTomaMate = false;
    public bool aTomaTulipan = false;
    public bool tieneEscudoActivo = false;

    [Header("Tiempo del Escudo")]
    public float duracionEscudo = 5f; // Duración del escudo en segundos (cambialo a gusto)

    private float velocidadOriginal;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource; // Referencia para el sonido de salto
    private float inputX;
    private bool estaEnSuelo;

    void Start()
    {
        velocidadOriginal = velocidad;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>(); // Inicializamos el componente de audio

        // Nos aseguramos de que el escudo arranque apagado
        if (visualEscudo != null) visualEscudo.SetActive(false);

        

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

        // SALTO Y SONIDO DE SALTO
        if (Keyboard.current.upArrowKey.wasPressedThisFrame && estaEnSuelo && !estaEnBicicleta)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

            // Reproducimos el sonido si el componente y el audio clip están asignados
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }

        // USAR ITEM (en orden)
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (InventarioManager.instancia.TieneAsado())
                InventarioManager.instancia.UsarAsado(transform.position);
            else if (InventarioManager.instancia.TieneDulce())
                InventarioManager.instancia.UsarDulce(transform.position);
        }

        animator.SetBool("isJumping", !estaEnSuelo && !estaEnBicicleta);
    } // <--- ¡ESTA LLAVE DE CIERRE FALTABA O ESTABA MAL UBICADA Y ARROJABA EL ERROR CS1513!

    void FixedUpdate()
    {
        float velX = estaEnBicicleta ? velocidadBici : velocidad;
        rb.linearVelocity = new Vector2(inputX * velX, rb.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. AGARRAR MEDALLA ESCUDO
        if (collision.CompareTag("MedallaEscudo"))
        {
            ActivarEscudo();
            collision.gameObject.SetActive(false);
        }

        // 2. DETECCIÓN DE PELIGROS LETALES (Reina o Toro)
        if (collision.CompareTag("Reina") || collision.CompareTag("Toro"))
        {
            // REPRODUCIMOS SONIDO DE CHOQUE / GOLPE
            AudioClip clipChoque = Resources.Load<AudioClip>("danio");
            if (clipChoque != null)
            {
                AudioSource.PlayClipAtPoint(clipChoque, transform.position);
            }

            if (tieneEscudoActivo)
            {
                RomperEscudo();
            }
            else if (!esInvencible)
            {
                if (estaEnBicicleta)
                {
                    estaEnBicicleta = false;
                    StartCoroutine(InvencibilidadTemporal(1.0f));
                }
                else if (gestor != null)
                {
                    gestor.PerderVida(this.gameObject);
                }
            }
        }

        // 3. DETECCIÓN DE TULIPÁN (Obstáculo ralentizador que interactúa con el escudo)
        if (collision.CompareTag("Tulipan"))
        {
            collision.gameObject.SetActive(false); // El tulipán desaparece al tocarlo

            // REPRODUCIMOS SONIDO DE CHOQUE / GOLPE
            AudioClip clipChoque = Resources.Load<AudioClip>("danio");
            if (clipChoque != null)
            {
                AudioSource.PlayClipAtPoint(clipChoque, transform.position);
            }

            if (tieneEscudoActivo)
            {
                // Si tiene escudo, el escudo absorbe el golpe y se rompe. 
                // Máxima NO pierde velocidad.
                RomperEscudo();
            }
            else
            {
                // Si no tiene escudo, se activa la ralentización normalmente
                StartCoroutine(EfectoTulipan());
            }
        }

        // 4. MECÁNICAS GENERALES
        if (collision.CompareTag("Bicicleta"))
        {
            StartCoroutine(ActivarBicicletaTemporal(collision.gameObject));
        }

        if (collision.CompareTag("Trampolin"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * fuerzaTrampolin, ForceMode2D.Impulse);

            // Reproducimos el sonido de salto al usar el trampolín
            AudioClip clipSalto = Resources.Load<AudioClip>("salto"); // Cambiá "salto" por el nombre exacto de tu archivo de audio si es diferente
            if (clipSalto != null && audioSource != null)
            {
                audioSource.PlayOneShot(clipSalto);
            }
        }

        if (collision.CompareTag("Meta"))
        {
            if (gestor != null)
                gestor.NivelCompletado(this.gameObject);
        }

        if (collision.CompareTag("Mate"))
        {
            collision.gameObject.SetActive(false);

            // Carga el archivo llamado "sonido_mate" desde la carpeta Resources y lo reproduce
            AudioClip clipMate = Resources.Load<AudioClip>("mate");
            if (clipMate != null && audioSource != null)
            {
                audioSource.PlayOneShot(clipMate); // PlayOneShot permite reproducir este sonido sin interrumpir otros
            }

            StartCoroutine(EfectoMate());
        }
    }

    void ActivarEscudo()
    {
        tieneEscudoActivo = true;
        esInvencible = true;

        // Hacemos visible a Lady Di al lado de Máxima
        if (visualEscudo != null)
        {
            visualEscudo.SetActive(true);
        }

        // Iniciamos el temporizador para que se apague solo tras unos segundos
        StartCoroutine(TemporizadorEscudo());
    }

    void RomperEscudo()
    {
        tieneEscudoActivo = false;

        // Ocultamos la figura de Lady Di
        if (visualEscudo != null)
        {
            visualEscudo.SetActive(false);
        }

        // Damos un breve lapso de invencibilidad para que no la maten instantáneamente
        StartCoroutine(InvencibilidadTemporal(1.5f));
    }

    // Corutina que espera el tiempo configurado y quita el escudo
    System.Collections.IEnumerator TemporizadorEscudo()
    {
        yield return new WaitForSeconds(duracionEscudo);

        // Si el escudo sigue activo (no se rompió antes por un choque), lo quitamos
        if (tieneEscudoActivo)
        {
            RomperEscudo();
        }
    }

    System.Collections.IEnumerator ActivarBicicletaTemporal(GameObject bicicleta)
    {
        estaEnBicicleta = true;
        bicicleta.SetActive(false);

        yield return new WaitForSeconds(5f);

        estaEnBicicleta = false;
    }

    System.Collections.IEnumerator InvencibilidadTemporal(float duracion = 1.0f)
    {
        esInvencible = true;
        yield return new WaitForSeconds(duracion);

        // Solo quita la invencibilidad si el escudo ya no está activo
        if (!tieneEscudoActivo)
        {
            esInvencible = false;
        }
    }

    System.Collections.IEnumerator EfectoMate()
    {
        velocidad = velocidadOriginal * 2f;
        velocidadBici = velocidadBici * 2f;
        yield return new WaitForSeconds(4f);
        velocidad = velocidadOriginal;
        velocidadBici = velocidadBici / 2f;
    }

    System.Collections.IEnumerator EfectoTulipan()
    {
        velocidad = velocidadOriginal / 2f;
        velocidadBici = velocidadBici / 2f;
        yield return new WaitForSeconds(3f);
        velocidad = velocidadOriginal;
        velocidadBici = velocidadBici * 2f;
    }
}