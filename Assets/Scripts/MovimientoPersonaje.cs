using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    public float VelocidadRotacion = 20f; 

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    public AudioSource sonidoPasos;

    void Start()
    {
        
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        sonidoPasos = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Define la dirección del movimiento respecto al escenario
        m_Movement.Set(-horizontal, 0f, -vertical);
        m_Movement.Normalize(); // Evita que corra más rápido en diagonal

        // Detecta si el jugador se está moviendo
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        // Activa la animación de caminar si hay movimiento
        m_Animator.SetBool("IsWalking", isWalking);

        // Calcula la rotación hacia la dirección del movimiento
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, VelocidadRotacion * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        // CONTROL DEL SONIDO DE PASOS
        if (isWalking)
        {
            if (!sonidoPasos.isPlaying)
                sonidoPasos.Play();
        }
        else
        {
            sonidoPasos.Stop();
        }

    }

    void OnAnimatorMove()
    {
        // Aplica el movimiento y rotación físicos sincronizados
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}