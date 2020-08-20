using UnityEngine;

[System.Serializable]
public class Phrase
{
    public string text;
    public PhraseType type;

    public enum PhraseType
    {
        system, other 
    }
}
