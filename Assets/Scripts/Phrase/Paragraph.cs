using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Paragraph", menuName = "Custom/Paragraph", order = 0)]
public class Paragraph : ScriptableObject
{
    public Sprite paragraphBackground;
    public Character character;
    public string characterEmotionLink;

    public int paragraphId;
    public List<Phrase> phrases = new List<Phrase>();
    public List<EndChoise> endChoises = new List<EndChoise>();
}

[System.Serializable]
public struct EndChoise
{
    public string text;
    public int nextParagraphId;
}
