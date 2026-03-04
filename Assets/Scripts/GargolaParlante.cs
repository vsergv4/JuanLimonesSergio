using UnityEngine;

public class GargolaParlante : MonoBehaviour
{
    [Header("Ink")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private AudioSource audioGargola;

    private bool puedeInteractuar = false;
    private bool dialogoActivo = false;

    private void Start()
    {
        audioGargola = GetComponent<AudioSource>();
        audioGargola.loop = true;
        audioGargola.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puedeInteractuar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puedeInteractuar = false;

            if (dialogoActivo)
            {
                DialogoManager.Instance.ModoDialogoOFF();
                dialogoActivo = false;
            }
        }
    }

    private void Update()
    {
        if (puedeInteractuar && Input.GetKeyDown(KeyCode.E))
        {
            DialogoManager.Instance.ModoDialogoON(inkJSON);
            DialogoManager.Instance.ModoDialogoRUN();
            dialogoActivo = true;
        }
        if (DialogoManager.Instance.derrotaGargola == true)
            Destroy(gameObject);
    }
}