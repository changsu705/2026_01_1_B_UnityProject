using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("UI 요소 - 인스펙터 창에서 연결")]
    public GameObject DialoguePanel;            // 대화 패널 UI
    public Image characterImage;                // 캐릭터 이미지 UI
    public TextMeshProUGUI characterNameText;   // 캐릭터 이름 텍스트
    public TextMeshProUGUI dialogueText;        // 대화 내용 텍스트
    public Button nextButton;                   // 다음 대화 버튼

    [Header("기본 설정")]
    public Sprite defaultCharacterImage;        // 기본 캐릭터 이미지

    [Header("타이핑 효과 설정")]
    public float typingSpeed = 0.05f;           // 타이핑 효과 속도
    public bool skipTypingOnClick = true;       // 클릭시 타이핑 즉시 완료할 지 여부

    //내부 변수들
    private DialogueDataSO currentDialogue;     // 현재 대화 데이터
    private int currentLineIndex = 0;           // 현재 대화 라인 인덱스
    private bool isDialogueActive = false;      // 대화 활성화 여부
    private bool isTyping = false;              // 타이핑 중 여부
    private Coroutine typingCoroutine;          // 타이핑 코루틴 참조



    void Start()
    {
        // 대화 패널 초기화
        DialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(HandleNextInput);
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            HandleNextInput();
        }
    }
    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        dialogueText.text = "";
        for (int i = 0; i < textToType.Length; i++)
        {
            dialogueText.text += textToType[i];
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void CompleteTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        isTyping = false;

        if (currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)
        {
            dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];
        }
    }

    void ShowCurrentLine()
    {
        if (currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
        string currentText = currentDialogue.dialogueLines[currentLineIndex];
        typingCoroutine = StartCoroutine(TypeText(currentText));
    }

    public void ShowNextLine()
    {
        currentLineIndex++;
        if (currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)
        {
            ShowCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        isDialogueActive = false;
        isTyping = false;
        DialoguePanel.SetActive(false);
        currentLineIndex = 0;
    }

    public void HandleNextInput()
    {

        if (isTyping && skipTypingOnClick)
        {
            CompleteTyping();
        }
        else if (!isTyping)
        {
            ShowNextLine();
        }
    }


    public void SkipDialogue()
    {
        EndDialogue();
    }
    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }

    public void StartDialogue(DialogueDataSO dialogue)
    {
        if (dialogue == null || dialogue.dialogueLines.Count == 0) return;


        currentDialogue = dialogue;
        currentLineIndex = 0;
        isDialogueActive = true;
        // UI 업데이트
        DialoguePanel.SetActive(true);
        characterNameText.text = dialogue.characterName;
        if (characterImage != null)
        {
            if (dialogue.characterImage != null)
            {
                characterImage.sprite = dialogue.characterImage;
            }
            else
            {
                characterImage.sprite = defaultCharacterImage;
            }
        }
        ShowCurrentLine();
    }
}
