using UnityEngine;
using System.Collections;

public class IntroNivelSiguiente : MonoBehaviour
{
    public GameObject introNivel;
    public GameObject fondoOscuro;

    [Header("Números Countdown")]
    public GameObject numero3;
    public GameObject numero2;
    public GameObject numero1;

    [Header("Personajes")]
    public GameObject reinaPerseguidora;

    [Header("Audio")]
    public AudioSource musicaFondo;

    [Header("Configuración")]
    public bool arrancarAlInicio = false;

    private bool countdownIniciado = false;

    void Start()
    {
        if (reinaPerseguidora != null)
        {
            reinaPerseguidora.SetActive(false);
        }

        if (musicaFondo != null && arrancarAlInicio)
        {
            musicaFondo.Stop();
        }

        if (arrancarAlInicio)
        {
            IniciarCountdown();
        }
    }

    public void IniciarCountdown()
    {
        if (countdownIniciado)
            return;

        countdownIniciado = true;

        Time.timeScale = 0f;

        if (reinaPerseguidora != null)
        {
            reinaPerseguidora.SetActive(false);
        }

        if (introNivel != null)
            introNivel.SetActive(true);

        if (fondoOscuro != null)
            fondoOscuro.SetActive(true);

        if (musicaFondo != null)
            musicaFondo.Stop();

        StartCoroutine(CorrerCuentaRegresiva());
    }

    IEnumerator CorrerCuentaRegresiva()
    {
        if (numero3 != null)
            numero3.SetActive(false);

        if (numero2 != null)
            numero2.SetActive(false);

        if (numero1 != null)
            numero1.SetActive(false);

        if (numero3 != null)
            numero3.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        if (numero3 != null)
            numero3.SetActive(false);

        if (numero2 != null)
            numero2.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        if (numero2 != null)
            numero2.SetActive(false);

        if (numero1 != null)
            numero1.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        if (numero1 != null)
            numero1.SetActive(false);

        if (introNivel != null)
            introNivel.SetActive(false);

        if (fondoOscuro != null)
            fondoOscuro.SetActive(false);

        if (reinaPerseguidora != null)
        {
            reinaPerseguidora.SetActive(true);
        }

        Time.timeScale = 1f;

        if (musicaFondo != null && !musicaFondo.isPlaying)
        {
            musicaFondo.Play();
        }
    }
}