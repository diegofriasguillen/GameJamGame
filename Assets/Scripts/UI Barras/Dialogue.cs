using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject ActionMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private float TypingTime = 0.05f; 

    private bool Inicializado = false;
    [SerializeField] private bool didDialogueStart;
    private int lineIndex;

    [SerializeField, TextArea(4,6)] private string[] dialoguelines;

    [Header("Es de mision?")]
    public bool isMision = false;
    public bool completeText = false;

    

    void Update()
    {
        if (Inicializado && Input.GetKeyDown(KeyCode.Space) && !completeText)
        {
            if (!didDialogueStart)
            {
                StartDialogue();

            }
            else if (dialogueText.text == dialoguelines[lineIndex])
            {
                NextDialogueLine();

            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialoguelines[lineIndex];
                 

            }
        }
    }
    void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        ActionMark.SetActive(false);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialoguelines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);

            if (isMision)
            {
                completeText = true;
            }else if (!isMision)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char ch in dialoguelines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(TypingTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Inicializado = true;
            if (!completeText)
            {
                ActionMark.SetActive(true);
            }
            Debug.Log("Rango para Hablar");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inicializado = false;
            ActionMark.SetActive(false);
            Debug.Log("Sin rango para Hablar");
        }
    }
}
