using UnityEngine;
using System.Collections;

public class SecuenciaInicio : MonoBehaviour
{
    public GameObject[] imagenesIntro;
    public GameObject pantallaControles;

    public float tiempoEntreImagenes = 5f;
    public float tiempoControles = 5f;

    private bool esperandoEspacio = false;
    private bool introIniciada = false;
    private bool mostrandoControles = false;

    void Start()
    {
        Time.timeScale = 0f;

        foreach (GameObject img in imagenesIntro)
            img.SetActive(false);

        pantallaControles.SetActive(false);
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
        // ENTER saltea la intro desde cualquier imagen
        if (introIniciada && !mostrandoControles && Input.GetKeyDown(KeyCode.Return))
        {
            SaltarIntro();
        }

        // ESPACIO en la última imagen pasa a controles
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

        Time.timeScale = 1f;

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

        mostrandoControles = false;
    }
}