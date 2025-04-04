using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class DialogueScript : MonoBehaviour
{
    [SerializeField] public GameObject dialogueBox;
    [SerializeField] public TMP_Text dialogueText;
    [SerializeField] public TMP_Text dialogueName;
    [SerializeField] public Image dialogueImage;
    [SerializeField] public DialogueStep[] dialogue;
    private int index;

    [SerializeField] public float textSpeed;

    private void Reset() {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 9;

    }

    public void Interact()
    {
        dialogueBox.SetActive(true);
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        dialogueName.text = dialogue[index].speakerName;
        dialogueImage.sprite = dialogue[index].speakerImage;
        foreach (var letter in dialogue[index].dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            dialogueName.text = "";
            dialogueImage.sprite = null;
            StartCoroutine(Typing());
        }
        else
        {
            endDialogue();
        }
    }

    public void endDialogue()
    {
        dialogueText.text = "";
        dialogueName.text = "";
        dialogueImage.sprite = null;
        index = 0;
        dialogueBox.SetActive(false);
        PlayerController.inDialogue = false;
    }
}
