using UnityEngine;

[System.Serializable]
public class DialogueSentences : MonoBehaviour
{
    public int[] Name;
    [TextArea(1,2)]
    public string[] Sentences;
    public string[] CharacterNames;
}
