using UnityEngine;
using System.Collections;

public class SecuenciaInicio : MonoBehaviour
{
    [Header("Imágenes de la introducción")]
    public GameObject[] imagenesIntro;

    [Header("Interfaz del jugador")]
    public GameObject corazon1;
    public GameObject corazon2;
    public GameObject corazon3;
    public GameObject indicadorMetros;
    public GameObject inventario;

    [Header("Countdown del Nivel 1")]
    public IntroNivelSiguiente scriptCountdown;

    [Header("Configuración")]
    public float tiempoEntreImagenes = 5f;

    private bool esperandoEspacio = false;
    private bool introIniciada = false;
    private bool gameplayActivado = false;

    void Start()
    {
        // Ocultamos todas las imágenes de la introducción.
        if (imagenesIntro != null)
        {
            foreach (GameObject img in imagenesIntro)
            {
                if (img != null)
                    img.SetActive(false);
            }
        }

        // PantallaInicio maneja la pantalla InicioJuego_0.
        // El nivel permanece pausado durante la pantalla y la intro.
        Time.timeScale = 0f;

        // Ocultamos la interfaz del jugador.
        if (corazon1 != null)
            corazon1.SetActive(false);

        if (corazon2 != null)
            corazon2.SetActive(false);

        if (corazon3 != null)
            corazon3.SetActive(false);

        if (indicadorMetros != null)
            indicadorMetros.SetActive(false);

        if (inventario != null)
            inventario.SetActive(false);
    }

    void Update()
    {
        // ENTER salta las imágenes de la introducción.
        if (introIniciada && Input.GetKeyDown(KeyCode.Return))
        {
            SaltarIntro();
        }

        // En la última imagen, ESPACIO comienza el countdown.
        if (esperandoEspacio && Input.GetKeyDown(KeyCode.Space))
        {
            ActivarGameplay();
        }
    }

    public void IniciarIntro()
    {
        if (introIniciada || gameplayActivado)
            return;

        introIniciada = true;
        esperandoEspacio = false;

        StartCoroutine(MostrarIntro());
    }

    public void SaltarIntro()
    {
        if (gameplayActivado)
            return;

        StopAllCoroutines();

        introIniciada = false;
        esperandoEspacio = false;

        OcultarImagenesIntro();
        ActivarGameplay();
    }

    IEnumerator MostrarIntro()
    {
        if (imagenesIntro == null || imagenesIntro.Length == 0)
        {
            Debug.LogWarning(
                "No hay imágenes asignadas en SecuenciaInicio."
            );

            ActivarGameplay();
            yield break;
        }

        // Muestra Intro1, Intro2, etc.
        for (int i = 0; i < imagenesIntro.Length - 1; i++)
        {
            if (imagenesIntro[i] != null)
                imagenesIntro[i].SetActive(true);

            if (i > 0 && imagenesIntro[i - 1] != null)
                imagenesIntro[i - 1].SetActive(false);

            yield return new WaitForSecondsRealtime(
                tiempoEntreImagenes
            );
        }

        // Mostrar la última imagen.
        int ultimaImagen = imagenesIntro.Length - 1;

        if (imagenesIntro[ultimaImagen] != null)
            imagenesIntro[ultimaImagen].SetActive(true);

        if (imagenesIntro.Length > 1 &&
            imagenesIntro[ultimaImagen - 1] != null)
        {
            imagenesIntro[ultimaImagen - 1].SetActive(false);
        }

        // Espera ESPACIO en la última imagen.
        esperandoEspacio = true;
    }

    public void EmpezarDirecto()
    {
        ActivarGameplay();
    }

    void OcultarImagenesIntro()
    {
        if (imagenesIntro == null)
            return;

        foreach (GameObject img in imagenesIntro)
        {
            if (img != null)
                img.SetActive(false);
        }
    }

    void ActivarGameplay()
    {
        if (gameplayActivado)
            return;

        gameplayActivado = true;
        introIniciada = false;
        esperandoEspacio = false;

        StopAllCoroutines();
        OcultarImagenesIntro();

        // Mostrar la interfaz del jugador.
        if (corazon1 != null)
            corazon1.SetActive(true);

        if (corazon2 != null)
            corazon2.SetActive(true);

        if (corazon3 != null)
            corazon3.SetActive(true);

        if (indicadorMetros != null)
            indicadorMetros.SetActive(true);

        if (inventario != null)
            inventario.SetActive(true);

        /*
         * IMPORTANTE:
         * Descongelamos antes de iniciar el countdown.
         * Así sus corrutinas pueden avanzar normalmente.
         */
        Time.timeScale = 1f;

        if (scriptCountdown != null)
        {
            scriptCountdown.IniciarCountdown();
        }
        else
        {
            Debug.LogWarning(
                "No está asignado Script Countdown en SecuenciaInicio."
            );
        }

        // Música del nivel.
        AudioClip musicaNivel =
            Resources.Load<AudioClip>("musica_fondo");

        if (musicaNivel != null)
        {
            GameObject emisorMusica =
                GameObject.Find("MusicaFondo");

            if (emisorMusica != null)
            {
                AudioSource fuenteMusica =
                    emisorMusica.GetComponent<AudioSource>();

                if (fuenteMusica != null)
                {
                    fuenteMusica.clip = musicaNivel;
                    fuenteMusica.Play();
                }
            }
        }
    }
}