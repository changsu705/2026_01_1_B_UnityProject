using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DiaogueDataSO", menuName = "Scriptable Objects/DiaogueDataSO")]
public class DialogueDataSO : ScriptableObject
{
    [Header ("캐릭터 정보")]
    public string characterName; // 캐릭터 이름
    public Sprite characterImage;

    [Header ("대화 내용")]
    [TextArea(3, 10)]
    public List<string> dialogueLines = new List<string>(); // 대화 내용 리스트


}
