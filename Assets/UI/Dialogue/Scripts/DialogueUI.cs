using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private RectTransform dialogueBox;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private PlayerMovement playerMovement;
    //[SerializeField] private DialogueObject testDialogue;
    
    public bool IsOpen {get; private set;}
    
    private TypeWriterEffect typeWriterEffect;

    private void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
    }

    public void showDialogue(DialogueObject dialogueObject)
    {
        StartCoroutine(MoveUp());
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typeWriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.R));
        }
        StartCoroutine(MoveDown());
    }

    public IEnumerator MoveUp()
    {
        IsOpen = true;
        playerMovement._enabled = false;
        float canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        float dialogueBoxHeight = dialogueBox.rect.height;
        
        Vector2 targetPosition = new Vector2(dialogueBox.anchoredPosition.x, dialogueBox.anchoredPosition.y + dialogueBoxHeight);

        while (dialogueBox.anchoredPosition.y < targetPosition.y)
        {
            dialogueBox.anchoredPosition = Vector2.MoveTowards(dialogueBox.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        dialogueBox.anchoredPosition = targetPosition;
    }

    public IEnumerator MoveDown()
    {
        playerMovement._enabled = true;
        Vector2 startPosition = new Vector2(dialogueBox.anchoredPosition.x, dialogueBox.anchoredPosition.y - dialogueBox.rect.height);

        while (dialogueBox.anchoredPosition.y > startPosition.y)
        {
            dialogueBox.anchoredPosition = Vector2.MoveTowards(dialogueBox.anchoredPosition, startPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        dialogueBox.anchoredPosition = startPosition;
        textLabel.text = string.Empty;
        IsOpen = false;
    }
}
