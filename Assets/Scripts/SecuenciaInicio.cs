using UnityEngine;
using System.Collections;

public class SecuenciaInicio : MonoBehaviour
{
    public GameObject[] imagenesIntro;
    public GameObject pantallaControles;

    public float tiempoEntreImagenes = 5f;
    public float tiempoControles = 5f;

    private bool esperandoEspacio = false;

    void Start()
    {
        Time.timeScale = 0f;

        foreach (GameObject img in imagenesIntro)
            img.SetActive(false);

        pantallaControles.SetActive(false);

        StartCoroutine(MostrarIntro());
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

    void Update()
    {
        if (esperandoEspacio && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(MostrarControlesYEmpezar());
        }
    }

    IEnumerator MostrarControlesYEmpezar()
    {
        esperandoEspacio = false;

        imagenesIntro[imagenesIntro.Length - 1].SetActive(false);

        pantallaControles.SetActive(true);

        yield return new WaitForSecondsRealtime(tiempoControles);

        pantallaControles.SetActive(false);

        Time.timeScale = 1f;
    }
}