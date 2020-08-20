using Nashim.UI;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using Web;

public class GameManager : MonoBehaviour
{
    public GameUI gameUi;
    public Paragraph currentParagraph;
    private int currentMessageId;

    Panel characterPanel;
    private void Start()
    {
        //Start game
        ViewportToDefault();
    }

    private void ViewportToDefault()
    {
        var choisesContainer = gameUi.choises.GetObjectByKey("container").transform; //Get choises container
        UI_Works.ClearViewport(); //Disable all panels
        UI_Works.ClearLayout(choisesContainer); //Chear choises ui
        gameUi.gameBackground.sprite = currentParagraph.paragraphBackground; //Set background

        if (currentParagraph.character == null) return;

        characterPanel = Instantiate(currentParagraph.character.prefab, gameUi.transform).GetComponent<Panel>();

        StartCoroutine(NetworkMainClass.GetImageFromServer($"{currentParagraph.characterEmotionLink}", success =>
        {
            var emotion = Sprite.Create(success, new Rect(0.0f, 0.0f, success.width, success.height), new Vector2(0.5f, 0.5f), 100.0f);
            characterPanel.GetComponentByKey<Image>("emotion").sprite = emotion;
        }));
    }


    public void ShowNextMessage()
    {
        if (currentMessageId < currentParagraph.phrases.Count)
        {
            if (currentParagraph.phrases[currentMessageId].type == Phrase.PhraseType.system)
                ShowOwnPhrases();
            else
                ShowOtherPhrases();
        }
        else
        {
            ShowChoises();
        }

        currentMessageId++;
    }

    private void ShowOwnPhrases()
    {
        UI_Works.ClearViewport();
        UI_Works.AddPanelsToViewport(gameUi.ownPhrases);

        var ownPhraseText = gameUi.ownPhrases.GetComponentByKey<Text>("phrase_text"); //Get text component by key
        ownPhraseText.text = currentParagraph.phrases[currentMessageId].text;
    }

    private void ShowOtherPhrases()
    {
        //Add character
        UI_Works.ClearViewport();
        UI_Works.AddPanelsToViewport(gameUi.othersPhrases);

        var othersPhraseText = gameUi.othersPhrases.GetComponentByKey<Text>("phrase_text"); //Get text component by key
        var characterName = gameUi.othersPhrases.GetComponentByKey<Text>("name"); //Get text component by key
        othersPhraseText.text = currentParagraph.phrases[currentMessageId].text;
        characterName.text = currentParagraph.character.characterName;
    }

    public void ShowChoises()
    {
        UI_Works.ClearViewport();

        if (currentParagraph.endChoises.Count > 0)
        {
            var choisesContainer = gameUi.choises.GetObjectByKey("container").transform;
            UI_Works.ClearLayout(choisesContainer);
            UI_Works.AddPanelsToViewport(gameUi.choises);
            for (int i = 0; i < currentParagraph.endChoises.Count; i++)
            {
                var nextParagraphId = currentParagraph.endChoises[i].nextParagraphId;
                GameObject go = Instantiate(gameUi.choisePrefab, choisesContainer);
                go.GetComponentInChildren<Text>().text = currentParagraph.endChoises[i].text;
                go.GetComponent<Button>().onClick.AddListener(delegate
                {
                    NextParagraph(nextParagraphId);
                });
            }
        }
        else
        {
            UI_Works.ClearViewport();
            UI_Works.AddPanelsToViewport(gameUi.theEnd);
        }
    }

    public void NextParagraph(int paragraphId)
    {
        Destroy(characterPanel?.gameObject);
        Paragraph[] paragraphs = Resources.LoadAll<Paragraph>("Paragraphs");
        currentParagraph = paragraphs.ToList().Find(x => x.paragraphId == paragraphId);
        currentMessageId = 0;

        ViewportToDefault();
    }
}
