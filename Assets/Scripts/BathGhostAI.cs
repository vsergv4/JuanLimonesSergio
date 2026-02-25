using UnityEngine;
using UnityEngine.AI;

public class BathGhostAI : MonoBehaviour
{
    public Transform player;         // Arrastra a John Lemon aquí
    public Animator ghostAnimator;   // Arrastra al Fantasma aquí
    public NavMeshAgent ghostAgent;  // Arrastra al Fantasma aquí también

    private bool isChasing = false;

    void Start()
    {
        // Al inicio, el fantasma no se mueve
        if (ghostAgent != null) ghostAgent.isStopped = true;
    }

    void Update()
    {
        // Si el jugador fue detectado, el fantasma lo sigue
        if (isChasing && player != null && ghostAgent != null)
        {
            ghostAgent.SetDestination(player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si lo que entró al cubo es el jugador
        if (other.CompareTag("Player") || other.gameObject.name == "JohnLemon")
        {
            isChasing = true;
            if (ghostAgent != null) ghostAgent.isStopped = false;
            if (ghostAnimator != null) ghostAnimator.SetBool("IsChasing", true);

            Debug.Log("¡Detección exitosa! Persiguiendo...");
        }
    }
}