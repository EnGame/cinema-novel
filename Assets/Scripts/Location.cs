using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "Custom/Location", order = 0)]
public class Location : ScriptableObject
{
    public Sprite background;

    public List<Paragraph> paragraphs = new List<Paragraph>();
}

