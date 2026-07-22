using UnityEngine;
using UnityEngine.UI;

public class OpcionesAudio : MonoBehaviour
{
    [Header("Música")]
    public AudioSource musicaFondo;
    public Slider sliderMusica;

    [Header("Efectos de sonido")]
    public AudioSource[] fuentesEfectos;
    public Slider sliderEfectos;

    private const string CLAVE_MUSICA = "VolumenMusica";
    private const string CLAVE_EFECTOS = "VolumenEfectos";

    private void Start()
    {
        float volumenMusicaGuardado =
            PlayerPrefs.GetFloat(CLAVE_MUSICA, 1f);

        float volumenEfectosGuardado =
            PlayerPrefs.GetFloat(CLAVE_EFECTOS, 1f);

        if (sliderMusica != null)
        {
            sliderMusica.minValue = 0f;
            sliderMusica.maxValue = 1f;
            sliderMusica.wholeNumbers = false;
            sliderMusica.value = volumenMusicaGuardado;

            sliderMusica.onValueChanged.AddListener(
                CambiarVolumenMusica
            );
        }

        if (sliderEfectos != null)
        {
            sliderEfectos.minValue = 0f;
            sliderEfectos.maxValue = 1f;
            sliderEfectos.wholeNumbers = false;
            sliderEfectos.value = volumenEfectosGuardado;

            sliderEfectos.onValueChanged.AddListener(
                CambiarVolumenEfectos
            );
        }

        AplicarVolumenMusica(volumenMusicaGuardado);
        AplicarVolumenEfectos(volumenEfectosGuardado);
    }

    public void CambiarVolumenMusica(float valor)
    {
        AplicarVolumenMusica(valor);

        PlayerPrefs.SetFloat(CLAVE_MUSICA, valor);
        PlayerPrefs.Save();
    }

    public void CambiarVolumenEfectos(float valor)
    {
        AplicarVolumenEfectos(valor);

        PlayerPrefs.SetFloat(CLAVE_EFECTOS, valor);
        PlayerPrefs.Save();
    }

    private void AplicarVolumenMusica(float valor)
    {
        if (musicaFondo != null)
        {
            musicaFondo.volume = valor;
        }
    }

    private void AplicarVolumenEfectos(float valor)
    {
        if (fuentesEfectos == null)
            return;

        foreach (AudioSource fuente in fuentesEfectos)
        {
            if (fuente != null)
            {
                fuente.volume = valor;
            }
        }
    }

    private void OnDestroy()
    {
        if (sliderMusica != null)
        {
            sliderMusica.onValueChanged.RemoveListener(
                CambiarVolumenMusica
            );
        }

        if (sliderEfectos != null)
        {
            sliderEfectos.onValueChanged.RemoveListener(
                CambiarVolumenEfectos
            );
        }
    }
}