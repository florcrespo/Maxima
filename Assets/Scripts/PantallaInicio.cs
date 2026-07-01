using UnityEngine;
using UnityEngine.InputSystem;

public class PantallaInicio : MonoBehaviour
{
    public GameObject pantallaInicio;
    public SecuenciaInicio secuenciaInicio;

    void Start()
    {
        Time.timeScale = 0f;
    }

    void Update()
    {
        // ESPACIO: ver introducción
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            pantallaInicio.SetActive(false);
            secuenciaInicio.IniciarIntro();
            enabled = false;
        }

        // ENTER: saltear todo e ir directo a controles
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            pantallaInicio.SetActive(false);
            secuenciaInicio.SaltarIntro();
            enabled = false;
        }
    }
}