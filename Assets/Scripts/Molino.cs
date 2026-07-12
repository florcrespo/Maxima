using UnityEngine;

public class Molino : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControlMaxima control = other.GetComponent<ControlMaxima>();
            if (control != null)
            {
                // Si NO se está agachando, ralentizamos
                // (Asumiendo que tienes un parámetro en el animator o un bool para agacharse)
                if (!control.estaAgachada) 
                {
                    control.velocidad = control.velocidadOriginal / 2f; 
                }
                else 
                {
                    // Si se agacha, velocidad normal
                    control.velocidad = control.velocidadOriginal;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControlMaxima control = other.GetComponent<ControlMaxima>();
            if (control != null)
            {
                // Al salir del área, restauramos la velocidad original
                control.velocidad = control.velocidadOriginal;
            }
        }
    }
}