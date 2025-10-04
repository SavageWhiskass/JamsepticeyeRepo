using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public GameObject dialogueUI;
    public Image dialoguePhoto;
    public Sprite[] characterPhotos;
    public TMP_Text textBox;
    Queue<Dialogue> dialogueQueue;
    string dialogueText = "";
    bool typeText = false;
    int textIndex = 0;
    float textTimer = 0;

    private void Start()
    {
        dialogueQueue = new Queue<Dialogue>();
    }

    public void AddDialogueToQueue(int photoIndex, string text)
    {
        Dialogue newDialogue = new Dialogue();
        newDialogue.photoIndex = photoIndex;
        newDialogue.text = text;
        dialogueQueue.Enqueue(newDialogue);
    }

    void CreateDialogue(int photoIndex, string text)
    {
        SetPhoto(photoIndex);
        dialogueText = text;
        textBox.text = "";
        ToggleBox(true);
        typeText = true;
    }

    void ToggleBox(bool state)
    {
        dialogueUI.SetActive(state);
    }

    void SetPhoto(int index)
    {
        if(index < characterPhotos.Length)
        {
            dialoguePhoto.sprite = characterPhotos[index];
        }
    }

    IEnumerator DialogueTimer()
    {
        yield return new WaitForSeconds(3);
        dialogueQueue.Dequeue();
        if(dialogueQueue.Count > 0)
        {
            CreateDialogue(dialogueQueue.Peek().photoIndex, dialogueQueue.Peek().text);
        }
        else
        {
            ToggleBox(false);
        }
    }

    private void Update()
    {
        ////Test input
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddDialogueToQueue(0, "The quick brown fox jumped over the lazy dog.");
            AddDialogueToQueue(1, "What? What does that even mean?");
            AddDialogueToQueue(0, "Hello World!");
            AddDialogueToQueue(1, "STOP BEING WEIRD!");
        }
        ////Test input end


        if (dialogueQueue.Count > 0 && dialogueText == "")
        {
            CreateDialogue(dialogueQueue.Peek().photoIndex, dialogueQueue.Peek().text);
        }

        if (typeText)
        {
            if(textTimer > 0)
            {
                textTimer -= Time.deltaTime;
            }
            else
            {
                textBox.text += dialogueText[textIndex];
                ++textIndex;

                if(textIndex == dialogueText.Length)
                {
                    typeText = false;
                    textIndex = 0;
                    StartCoroutine(DialogueTimer());
                }
                else
                {
                    textTimer = 0.025f;
                }
            }
        }
    }
}
