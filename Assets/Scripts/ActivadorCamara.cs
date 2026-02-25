using UnityEngine;
using Unity.Cinemachine;

public class ActivadorCamara : MonoBehaviour
{
    
    public CinemachineCamera camaraDeEstaZona;

    private void OnTriggerEnter(Collider other)
    {
        // Detecta a John Lemon
        if (other.CompareTag("Player") || other.name.Contains("John"))
        {
            camaraDeEstaZona.Priority = 20; // Activa esta cámara
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.name.Contains("John"))
        {
            camaraDeEstaZona.Priority = 10; // Devuelve el control a la cámara anterior
        }
    }
}