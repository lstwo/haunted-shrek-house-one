using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI nameText;
    public AudioSource TypingAudioSource;

    public GameObject dialoguePanel;

    private static DialogManager _instance;

    public static DialogManager Instance
    {
        get { return _instance; }
    }

    private Queue<string> sentences;

    private string currentSentence;
    private Coroutine currentTypeSentenceCoroutine;
    private AudioClip currentTypingAudioClip;
    private float currentTypingSpeed = .1f;

    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(_instance);
        _instance = this;
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, bool pauseGame)
    {
        if (dialogue == null || dialogue.sentences.Length <= 0) 
        { 
            Debug.LogError("Dialog is null or has no content!");
            return;
        }

        if(dialoguePanel != null) dialoguePanel.SetActive(true);
        if (pauseGame) Time.timeScale = 0;

        InputManager.Instance.nextDialogueAction.performed += DisplayNextSentence;
        InputManager.Instance.skipDialogueAction.performed += SkipSentence;

        currentTypingAudioClip = dialogue.characterTypeSound;
        currentTypingSpeed = dialogue.typingSpeed;

        TypingAudioSource.clip = currentTypingAudioClip;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        nameText.text = dialogue.name;

        DisplayNextSentence();
    }

    private IEnumerator TypeSentence(string sentence)
    {
        messageText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            if(TypingAudioSource.clip != null)
            {
                TypingAudioSource.Play();
            }

            messageText.text += c;
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(currentTypingSpeed));
        }
    }

    private void SkipSentence(InputAction.CallbackContext cc)
    {
        StopCoroutine(currentTypeSentenceCoroutine);
        messageText.text = currentSentence;
    }

    public void DisplayNextSentence()
    {
        if(messageText.text != "" && messageText.text != currentSentence)
        {
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentence = sentences.Dequeue();

        currentTypeSentenceCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    public void DisplayNextSentence(InputAction.CallbackContext callbackContext)
    {
        DisplayNextSentence();
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        InputManager.Instance.nextDialogueAction.performed -= DisplayNextSentence;

        Time.timeScale = 1;
    }
}

public static class CoroutineUtilities
{
    public static IEnumerator WaitForRealTime(float delay)
    {
        while (true)
        {
            float pauseEndTime = Time.realtimeSinceStartup + delay;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return 0;
            }
            break;
        }
    }
}