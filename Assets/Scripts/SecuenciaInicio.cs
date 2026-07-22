using UnityEngine;
using System.Collections;

public class SecuenciaInicio : MonoBehaviour
{
    public GameObject[] imagenesIntro;
    public GameObject pantallaControles;
    public GameObject corazon1;
    public GameObject corazon2;
    public GameObject corazon3;
    public GameObject indicadorMetros;
    public GameObject inventario;

    [Header("Referencia al Countdown del Nivel 1")]
    public IntroNivelSiguiente scriptCountdown; // <-- ¡ACÁ DECLARAMOS LA VARIABLE!

    public float tiempoEntreImagenes = 5f;
    public float tiempoControles = 5f;

    private bool esperandoEspacio = false;
    private bool introIniciada = false;
    private bool mostrandoControles = false;

    void Start()
    {
        foreach (GameObject img in imagenesIntro)
            img.SetActive(false);

        pantallaControles.SetActive(false);

        if (PlayerPrefs.GetInt("IntroVista", 0) == 1)
        {
            // Ya se vio la intro: no seguimos con el resto de la lógica
            return;
        }

        Time.timeScale = 0f;

        corazon1.SetActive(false);
        corazon2.SetActive(false);
        corazon3.SetActive(false);
        indicadorMetros.SetActive(false);
        inventario.SetActive(false);
    }

    public void IniciarIntro()
    {
        if (!introIniciada && !mostrandoControles)
        {
            introIniciada = true;
            StartCoroutine(MostrarIntro());
        }
    }

    public void SaltarIntro()
    {
        if (!mostrandoControles)
        {
            StopAllCoroutines();
            foreach (GameObject img in imagenesIntro)
                img.SetActive(false);
            StartCoroutine(MostrarControlesYEmpezar());
        }
    }

    void Update()
    {
        if (introIniciada && !mostrandoControles && Input.GetKeyDown(KeyCode.Return))
        {
            SaltarIntro();
        }

        if (esperandoEspacio && Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            StartCoroutine(MostrarControlesYEmpezar());
        }
    }

    IEnumerator MostrarIntro()
    {
        for (int i = 0; i < imagenesIntro.Length - 1; i++)
        {
            imagenesIntro[i].SetActive(true);
            if (i > 0)
                imagenesIntro[i - 1].SetActive(false);
            yield return new WaitForSecondsRealtime(tiempoEntreImagenes);
        }

        imagenesIntro[imagenesIntro.Length - 1].SetActive(true);
        if (imagenesIntro.Length > 1)
            imagenesIntro[imagenesIntro.Length - 2].SetActive(false);

        esperandoEspacio = true;
    }

    IEnumerator MostrarControlesYEmpezar()
    {
        mostrandoControles = true;
        esperandoEspacio = false;
        introIniciada = false;

        foreach (GameObject img in imagenesIntro)
            img.SetActive(false);

        pantallaControles.SetActive(true);
        yield return new WaitForSecondsRealtime(tiempoControles);
        pantallaControles.SetActive(false);

        ActivarGameplay();
        PlayerPrefs.SetInt("IntroVista", 1);
        mostrandoControles = false;
    }

    public void EmpezarDirecto()
    {
        ActivarGameplay();
    }

    void ActivarGameplay()
    {
        // Activamos la UI del jugador
        corazon1.SetActive(true);
        corazon2.SetActive(true);
        corazon3.SetActive(true);
        indicadorMetros.SetActive(true);
        inventario.SetActive(true);

        // Disparamos la cuenta regresiva antes de arrancar el tiempo
        if (scriptCountdown != null)
        {
            scriptCountdown.IniciarCountdown();
        }
        else
        {
            Time.timeScale = 1f; // Respaldo si no asignaste el script en Inspector
        }

        // Cargar música de fondo
        AudioClip musicaNivel = Resources.Load<AudioClip>("musica_fondo");
        if (musicaNivel != null)
        {
            GameObject emisorMusica = GameObject.Find("MusicaFondo");
            if (emisorMusica != null)
            {
                AudioSource fuenteMusica = emisorMusica.GetComponent<AudioSource>();
                if (fuenteMusica != null)
                {
                    fuenteMusica.clip = musicaNivel;
                    fuenteMusica.Play();
                }
            }
        }
    }
}