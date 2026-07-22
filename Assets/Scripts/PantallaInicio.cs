using UnityEngine;
using UnityEngine.InputSystem;

public class PantallaInicio : MonoBehaviour
{
    [Header("Pantalla inicial")]
    public GameObject pantallaInicio;

    [Header("Secuencia de imágenes")]
    public SecuenciaInicio secuenciaInicio;

    void Start()
    {
        // Pausa el nivel mientras está visible la pantalla inicial.
        Time.timeScale = 0f;

        // Mostrar siempre InicioJuego_0 al entrar al Nivel 1.
        if (pantallaInicio != null)
        {
            pantallaInicio.SetActive(true);
        }
        else
        {
            Debug.LogWarning(
                "No está asignada la Pantalla Inicio en PantallaInicio."
            );
        }
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        // ESPACIO: comienza la secuencia Intro1 → Intro6.
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (pantallaInicio != null)
                pantallaInicio.SetActive(false);

            if (secuenciaInicio != null)
            {
                secuenciaInicio.IniciarIntro();
            }
            else
            {
                Debug.LogWarning(
                    "No está asignado SecuenciaInicio en PantallaInicio."
                );
            }

            enabled = false;
            return;
        }

        // ENTER: salta las imágenes y comienza el countdown.
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (pantallaInicio != null)
                pantallaInicio.SetActive(false);

            if (secuenciaInicio != null)
            {
                secuenciaInicio.SaltarIntro();
            }
            else
            {
                Debug.LogWarning(
                    "No está asignado SecuenciaInicio en PantallaInicio."
                );
            }

            enabled = false;
        }
    }
}