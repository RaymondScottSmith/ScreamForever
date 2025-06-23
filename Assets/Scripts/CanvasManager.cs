using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    public GameObject textPane;
    public TMP_Text subText;
    
    
    public float typeDelay = 0.1f;
    private string currentText = "";
    private bool typingDialogue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //will not interrupt current message
    public void DisplayText(string newMessage, float duration = 2f)
    {
        if (!textPane.activeSelf)
        {
            StartCoroutine(ShowTextOnScreen(newMessage, duration));
        }
    }
    //will interrupt current message but not typewritten message
    public void InterruptDisplay(string newMessage, float duration = 2f)
    {
        if (!typingDialogue)
            StartCoroutine(ShowTextOnScreen(newMessage, duration));
    }

    public void WriteText(string message, float duration = 3f)
    {
        if (!typingDialogue)
            StartCoroutine(WriteTextOnScreen(message, duration));
    }

    private IEnumerator ShowTextOnScreen(string message, float duration)
    {
        textPane.SetActive(true);
        subText.text = message;
        yield return new WaitForSeconds(duration);
        subText.text = "";
        textPane.SetActive(false);
    }

    private IEnumerator WriteTextOnScreen(string fullText, float displayTime)
    {
        textPane.SetActive(true);
        typingDialogue = true;
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            subText.text = currentText;
            yield return new WaitForSeconds(typeDelay);
        }

        yield return new WaitForSeconds(displayTime);
        subText.text = "";
        textPane.SetActive(false);
        typingDialogue = false;
    }


}
