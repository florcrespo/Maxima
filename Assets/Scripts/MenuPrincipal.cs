using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelComoJugar;
    public GameObject panelCreditos;
    public GameObject panelOpciones;

    public void ComenzarPartida()
    {
        SceneManager.LoadScene("Escena1");
    }

    public void ComoJugar()
    {
        if (panelComoJugar != null)
        {
            panelComoJugar.SetActive(true);
        }
    }

    public void VolverDesdeComoJugar()
    {
        if (panelComoJugar != null)
        {
            panelComoJugar.SetActive(false);
        }
    }

    public void Creditos()
    {
        if (panelCreditos != null)
        {
            panelCreditos.SetActive(true);
        }
    }

    public void VolverDesdeCreditos()
    {
        if (panelCreditos != null)
        {
            panelCreditos.SetActive(false);
        }
    }

    public void Opciones()
    {
        if (panelOpciones != null)
        {
            panelOpciones.SetActive(true);
        }
    }

    public void VolverDesdeOpciones()
    {
        if (panelOpciones != null)
        {
            panelOpciones.SetActive(false);
        }
    }

    public void SalirJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}