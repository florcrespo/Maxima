using UnityEngine;
using System.Collections;

public class IntroNivelSiguiente : MonoBehaviour
{
    public GameObject introNivel;        // Cartel del nivel (Nivel 1 o Nivel 2)
    public GameObject fondoOscuro;       // Fondo oscuro
    
    [Header("Números Countdown")]
    public GameObject numero3;
    public GameObject numero2;
    public GameObject numero1;

    [Header("Audio")]
    public AudioSource musicaFondo;      // Arrastrá acá el objeto que tiene la música de fondo

    [Header("Configuración")]
    public bool arrancarAlInicio = false; // TRUE para Nivel 2, FALSE para Nivel 1

    void Start()
    {
        // Nos aseguramos de que la música empiece apagada durante la intro
        if (musicaFondo != null && arrancarAlInicio)
        {
            musicaFondo.Stop();
        }

        // Si está marcado para arrancar al inicio (como en el Nivel 2), corre la cuenta ya mismo
        if (arrancarAlInicio)
        {
            IniciarCountdown();
        }
    }

    // Este método se llama solo o desde SecuenciaInicio
    public void IniciarCountdown()
    {
        Time.timeScale = 0f;

        if (introNivel != null) introNivel.SetActive(true);
        if (fondoOscuro != null) fondoOscuro.SetActive(true);

        // Aseguramos que la música no suene durante la intro
        if (musicaFondo != null) musicaFondo.Stop();

        StartCoroutine(CorrerCuentaRegresiva());
    }

    IEnumerator CorrerCuentaRegresiva()
    {
        if (numero3 != null) numero3.SetActive(false);
        if (numero2 != null) numero2.SetActive(false);
        if (numero1 != null) numero1.SetActive(false);

        // --- 3 ---
        if (numero3 != null) numero3.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        if (numero3 != null) numero3.SetActive(false);

        // --- 2 ---
        if (numero2 != null) numero2.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        if (numero2 != null) numero2.SetActive(false);

        // --- 1 ---
        if (numero1 != null) numero1.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        if (numero1 != null) numero1.SetActive(false);

        // Ocultar al terminar
        if (introNivel != null) introNivel.SetActive(false);
        if (fondoOscuro != null) fondoOscuro.SetActive(false);

        Time.timeScale = 1f;

        // --- REPRODUCIR MÚSICA AL TERMINAR LA INTRO ---
        if (musicaFondo != null && !musicaFondo.isPlaying)
        {
            musicaFondo.Play();
        }
    }
}