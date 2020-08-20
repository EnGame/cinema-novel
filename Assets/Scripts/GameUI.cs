using Nashim.UI;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image gameBackground;

    [Header("Panels")]
    public Panel ownPhrases;
    public Panel othersPhrases;
    public Panel choises;
    public Panel theEnd;

    [Header("Prefabs")]
    public GameObject choisePrefab;
}
