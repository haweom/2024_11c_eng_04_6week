using System.Collections;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private RectTransform dialogueBox;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float moveSpeed = 100f;
    
    [SerializeField] private DialogueObject testDialogue;

    private bool isUp = false;
    private TypeWriterEffect typeWriterEffect;

    private void Start()
    {
        StartCoroutine(MoveUp());
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        showDialogue(testDialogue);
    }

    public void showDialogue(DialogueObject dialogueObject)
    {
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

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isUp)
                StartCoroutine(MoveDown());
            else
            {
                StartCoroutine(MoveUp());
            }
        }
    }*/

    public IEnumerator MoveUp()
    {
        isUp = true;
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
        isUp = false;
        
        Vector2 startPosition = new Vector2(dialogueBox.anchoredPosition.x, dialogueBox.anchoredPosition.y - dialogueBox.rect.height);

        while (dialogueBox.anchoredPosition.y > startPosition.y)
        {
            dialogueBox.anchoredPosition = Vector2.MoveTowards(dialogueBox.anchoredPosition, startPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        dialogueBox.anchoredPosition = startPosition;
        textLabel.text = string.Empty;
    }
}
