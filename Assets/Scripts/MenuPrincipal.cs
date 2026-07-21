using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelComoJugar;

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
        Debug.Log("Abrir panel Créditos");
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