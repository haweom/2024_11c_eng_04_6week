using System.Collections;
using Cinemachine;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private RectTransform dialogueBox;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private PlayerMovement playerMovement;
    
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float zoomedInSize = 3f;
    [SerializeField] private float zoomSpeed = 2f;
    
    public bool IsOpen {get; private set;}
    
    private TypeWriterEffect typeWriterEffect;
    private float originalSize;

    private void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        originalSize = virtualCamera.m_Lens.OrthographicSize;
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
            textLabel.text = string.Empty;
            yield return typeWriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.R));
        }
        StartCoroutine(MoveDown());
    }

    public IEnumerator MoveUp()
    {
        StartCoroutine(ZoomIn());
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
        StartCoroutine(ZoomOut());
        Vector2 startPosition = new Vector2(dialogueBox.anchoredPosition.x, dialogueBox.anchoredPosition.y - dialogueBox.rect.height);

        while (dialogueBox.anchoredPosition.y > startPosition.y)
        {
            dialogueBox.anchoredPosition = Vector2.MoveTowards(dialogueBox.anchoredPosition, startPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        dialogueBox.anchoredPosition = startPosition;
        textLabel.text = string.Empty;
        IsOpen = false;
        playerMovement._enabled = true;
    }
    
    private IEnumerator ZoomIn()
    {
        while (virtualCamera.m_Lens.OrthographicSize > zoomedInSize)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(virtualCamera.m_Lens.OrthographicSize, zoomedInSize, zoomSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator ZoomOut()
    {
        while (virtualCamera.m_Lens.OrthographicSize < originalSize)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(virtualCamera.m_Lens.OrthographicSize, originalSize, zoomSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
