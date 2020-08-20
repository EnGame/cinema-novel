using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Custom/Character", order = 1)]
public class Character : ScriptableObject
{
    public GameObject prefab;
    public string characterName;
}
