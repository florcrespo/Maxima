using UnityEngine;
using UnityEngine.InputSystem;

public class PantallaInicio : MonoBehaviour
{
    public GameObject pantallaInicio;
    public SecuenciaInicio secuenciaInicio;

    void Start()
    {
        if (PlayerPrefs.GetInt("IntroVista", 0) == 1)
        {
            // Ya se vio la intro en el Nivel 1:
            // Apagamos la pantalla de inicio y nos desactivamos SILENCIOSAMENTE,
            // dejando que IntroNivelSiguiente maneje el Time.timeScale y la cuenta regresiva.
            if (pantallaInicio != null) 
            {
                pantallaInicio.SetActive(false);
            }
            
            enabled = false;
            return;
        }

        // Si es la primerísima vez (Nivel 1 sin haber visto intro)
        Time.timeScale = 0f;
    }

    void Update()
    {
        // ESPACIO: ver introducción
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (pantallaInicio != null) pantallaInicio.SetActive(false);
            if (secuenciaInicio != null) secuenciaInicio.IniciarIntro();
            enabled = false;
        }

        // ENTER: saltear todo e ir directo a controles
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (pantallaInicio != null) pantallaInicio.SetActive(false);
            if (secuenciaInicio != null) secuenciaInicio.SaltarIntro();
            enabled = false;
        }
    }
}