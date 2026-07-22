using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject panelPausa;

    private bool juegoPausado = false;

    void Start()
    {
        panelPausa.SetActive(false);
        AudioListener.pause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        panelPausa.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        juegoPausado = false;
    }

    void Pausar()
    {
        panelPausa.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        juegoPausado = true;
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuPrincipal()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("MenuPrincipal");
    }
}