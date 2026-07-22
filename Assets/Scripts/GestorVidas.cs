using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestorVidas : MonoBehaviour
{
    public static GestorVidas instancia;

    public int vidas = 3;

    public Image corazon1;
    public Image corazon2;
    public Image corazon3;

    public Sprite corazonLleno;
    public Sprite corazonVacio;

    public Vector3 puntoInicio;

    public GameObject mensajeNivelCompletado;
    public GameObject cartelGameOver;
    public GameObject botonSiguienteNivel;
    public GameObject fondoOscuro;
    public GameObject reinaPerseguidora;
    public GameObject generadorToros;

    public GameObject maximaNormal;
    public GameObject maximaConZuecos;

    public bool nivelCompletado = false;

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (mensajeNivelCompletado != null)
            mensajeNivelCompletado.SetActive(false);

        if (cartelGameOver != null)
            cartelGameOver.SetActive(false);

        if (botonSiguienteNivel != null)
            botonSiguienteNivel.SetActive(false);

        if (fondoOscuro != null)
            fondoOscuro.SetActive(false);

        ActualizarCorazones();
    }

    void ActualizarCorazones()
    {
        corazon1.sprite = (vidas >= 1) ? corazonLleno : corazonVacio;
        corazon2.sprite = (vidas >= 2) ? corazonLleno : corazonVacio;
        corazon3.sprite = (vidas >= 3) ? corazonLleno : corazonVacio;
    }

    void DetenerToros()
    {
        if (generadorToros != null)
        {
            Generador generador = generadorToros.GetComponent<Generador>();

            if (generador != null)
                generador.DetenerGenerador();
        }
    }

    public void NivelCompletado(GameObject maxima)
    {
        nivelCompletado = true;

        DetenerToros();

        if (maxima == maximaConZuecos && maximaNormal != null)
        {
            Vector3 posicionFinal = maximaConZuecos.transform.position;

            maximaConZuecos.SetActive(false);
            maximaNormal.transform.position = posicionFinal;
            maximaNormal.SetActive(true);

            maxima = maximaNormal;
        }

        if (mensajeNivelCompletado != null)
            mensajeNivelCompletado.SetActive(true);

        if (botonSiguienteNivel != null)
            botonSiguienteNivel.SetActive(true);

        if (fondoOscuro != null)
            fondoOscuro.SetActive(true);

        AudioClip clipVictoria = Resources.Load<AudioClip>("victoria");

        if (clipVictoria != null)
            AudioSource.PlayClipAtPoint(clipVictoria, maxima.transform.position);

        if (reinaPerseguidora != null)
            reinaPerseguidora.SetActive(false);

        ControlMaxima control = maxima.GetComponent<ControlMaxima>();
        Animator anim = maxima.GetComponent<Animator>();
        Rigidbody2D rb = maxima.GetComponent<Rigidbody2D>();

        if (control != null)
        {
            control.estaEnBicicleta = false;
            control.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }

        if (anim != null)
        {
            anim.speed = 1f;
            anim.SetFloat("velocidad", 0f);
            anim.SetBool("enBicicleta", false);
            anim.SetBool("isJumping", false);
            anim.CrossFade("maxima_idle", 0f, 0);
        }

        GameObject emisorMusica = GameObject.Find("MusicaFondo");

        if (emisorMusica != null)
            emisorMusica.GetComponent<AudioSource>().Stop();

        Time.timeScale = 0f;
    }

    public void PerderVida(GameObject maxima)
    {
        vidas--;
        ActualizarCorazones();

        if (vidas > 0)
        {
            AudioClip clipPerderVida = Resources.Load<AudioClip>("perder vida");

            if (clipPerderVida != null)
                AudioSource.PlayClipAtPoint(clipPerderVida, maxima.transform.position);

            Rigidbody2D rb = maxima.GetComponent<Rigidbody2D>();

            if (rb != null)
                rb.linearVelocity = Vector2.zero;

            SpriteRenderer sr = maxima.GetComponent<SpriteRenderer>();

            if (sr != null)
                StartCoroutine(EfectoParpadeo(sr));
        }
        else
        {
            AudioClip clipDerrota = Resources.Load<AudioClip>("derrota");

            if (clipDerrota != null)
                AudioSource.PlayClipAtPoint(clipDerrota, maxima.transform.position);

            StartCoroutine(GameOver(maxima));
        }
    }

    public void RecuperarVida()
    {
        if (vidas < 3)
        {
            vidas++;
            ActualizarCorazones();

            AudioClip clipRecuperarVida = Resources.Load<AudioClip>("recuperar vida");

            if (clipRecuperarVida != null && Camera.main != null)
                AudioSource.PlayClipAtPoint(
                    clipRecuperarVida,
                    Camera.main.transform.position
                );
        }
    }

    System.Collections.IEnumerator GameOver(GameObject maxima)
    {
        DetenerToros();

        if (fondoOscuro != null)
            fondoOscuro.SetActive(true);

        if (cartelGameOver != null)
            cartelGameOver.SetActive(true);

        if (maxima == maximaConZuecos && maximaNormal != null)
        {
            Vector3 posicionMuerte = maximaConZuecos.transform.position;

            maximaConZuecos.SetActive(false);
            maximaNormal.transform.position = posicionMuerte;
            maximaNormal.SetActive(true);

            maxima = maximaNormal;
        }

        ControlMaxima control = maxima.GetComponent<ControlMaxima>();
        Animator anim = maxima.GetComponent<Animator>();
        Rigidbody2D rb = maxima.GetComponent<Rigidbody2D>();

        if (control != null)
        {
            control.estaEnBicicleta = false;
            control.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        if (anim != null)
        {
            anim.speed = 1f;
            anim.SetBool("enBicicleta", false);
            anim.SetBool("isJumping", false);
            anim.SetFloat("velocidad", 0f);
            anim.SetTrigger("Morir");
        }

        if (reinaPerseguidora != null)
        {
            Rigidbody2D rbReina =
                reinaPerseguidora.GetComponent<Rigidbody2D>();

            Animator animReina =
                reinaPerseguidora.GetComponent<Animator>();

            if (rbReina != null)
                rbReina.linearVelocity = Vector2.zero;

            if (animReina != null)
                animReina.enabled = false;
        }

        GameObject emisorMusica = GameObject.Find("MusicaFondo");

        if (emisorMusica != null)
            emisorMusica.GetComponent<AudioSource>().Stop();

        yield return new WaitForSeconds(1f);

        Time.timeScale = 0f;
    }

    public void ReintentarNivel()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;

        SceneManager.LoadScene("MenuPrincipal");
    }

    public void PerderBici(GameObject maxima)
    {
        ControlMaxima control = maxima.GetComponent<ControlMaxima>();

        if (control != null)
            control.estaEnBicicleta = false;
    }

    System.Collections.IEnumerator EfectoParpadeo(SpriteRenderer sr)
    {
        ControlMaxima control = sr.GetComponent<ControlMaxima>();

        if (control == null)
            yield break;

        control.esInvencible = true;

        for (int i = 0; i < 5; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.2f);

            sr.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        control.esInvencible = false;
    }
}