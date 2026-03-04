using Ink.Runtime;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogoManager : MonoBehaviour
{
    public static DialogoManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        InicializarArray();
    }

     public bool derrotaGargola = false;

    [SerializeField] private GameObject panelDialogo;

    private Story historia;

    [SerializeField] private TextMeshProUGUI textoDialogo;

    private bool dialogoEnMarcha;

    [SerializeField] private GameObject[] botonesOpciones;
    private TextMeshProUGUI[] textoOpciones;
    
    private void InicializarArray()
    {
        textoOpciones = new TextMeshProUGUI[botonesOpciones.Length];
        int index = 0;
        foreach(GameObject opciones in botonesOpciones)
        {
            textoOpciones[index] = opciones.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    public void ModoDialogoON(TextAsset inkJSON)
    {
        historia = new Story(inkJSON.text);
        dialogoEnMarcha = true;
        panelDialogo.SetActive(true);
    }

    public void ModoDialogoRUN()
    {
        if (historia.canContinue == true)
        {
            textoDialogo.text = historia.Continue();
            ListarOpciones();
        }
        else 
            ModoDialogoOFF();
        
    }
    private void ListarOpciones()
    {
        List<Choice> opcionesActuales = historia.currentChoices;
        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            botonesOpciones[i].SetActive(false);
        }
        int index = 0;
        foreach(Choice opciones in opcionesActuales)
        {
            botonesOpciones[index].SetActive(true);
            textoOpciones[index].text = opciones.text;
        }
    }

    public void ModoDialogoOFF()
    {
        derrotaGargola = (bool)historia.variablesState["acertijoCorrecto"];
        dialogoEnMarcha = false;
        panelDialogo.SetActive(false);
        textoDialogo.text = "";
    }

    public void Click(int seleccionIndice)
    {
        historia.ChooseChoiceIndex(seleccionIndice);
        ModoDialogoRUN();
    }

    private void Update()
    {
        if (dialogoEnMarcha)
            return;
        if (Input.GetButtonDown("Fire2"))
            ModoDialogoRUN();
    }
}
