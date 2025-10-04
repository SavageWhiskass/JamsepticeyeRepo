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
    public Image photoBackground;
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
        switch (photoIndex)
        {
            case 0:
                newDialogue.backgroundColor = new Color(0.337f, 0.278f, 0.42f);
                break;
            case 1:
                newDialogue.backgroundColor = new Color(0.898f, 0.651f, 0.318f);
                break;
            default:
                newDialogue.backgroundColor = Color.white;
                break;
        }
        newDialogue.photoIndex = photoIndex;
        newDialogue.text = text;
        dialogueQueue.Enqueue(newDialogue);
    }

    void CreateDialogue(Color color, int photoIndex, string text)
    {
        photoBackground.color = color;
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
            CreateDialogue(dialogueQueue.Peek().backgroundColor, dialogueQueue.Peek().photoIndex, dialogueQueue.Peek().text);
        }
        else
        {
            ToggleBox(false);
            dialogueText = "";
            typeText = false;
            textIndex = 0;
            textTimer = 0;
        }
    }

    private void Update()
    {
        ////Test input
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddDialogueToQueue(1, "The quick brown fox jumped over the lazy dog.");
            AddDialogueToQueue(0, "What? What does that even mean?");
            AddDialogueToQueue(1, "Hello World!");
            AddDialogueToQueue(0, "STOP BEING WEIRD!");
        }
        ////Test input end


        if (dialogueQueue.Count > 0 && dialogueText == "")
        {
            CreateDialogue(dialogueQueue.Peek().backgroundColor, dialogueQueue.Peek().photoIndex, dialogueQueue.Peek().text);
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
