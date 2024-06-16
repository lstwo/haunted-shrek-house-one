using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool pauseGame = true;
    public Dialogue[] dialogues;

    public void TriggerDialogue(int i)
    {
        DialogManager.Instance.StartDialogue(dialogues[i], pauseGame);
    }
}
