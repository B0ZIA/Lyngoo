using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    public static MissionController Instance;

    private static MissionData currentMisionData;
    private Conversation currentConversation;
    private static Mission lastClicked;
    [SerializeField]
    private BackgroudSlider slider;

    [SerializeField]
    private GameObject missionListField;
    [SerializeField]
    private GameObject mapField;

    [SerializeField]
    private GameObject sentencesPanel;

    private Text currentPersonText;
    [SerializeField]
    private Transform sentencesContent;
    private List<Sentence> missionAnswers = new List<Sentence>();
    private int dialogPart;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public static void LoadMisionData(MissionData data)
    {
        currentMisionData = data;
    }

    public static Mission GetLatClickedMision()
    {
        return lastClicked;
    }

    public static void SetLastClickedMission(Mission mission)
    {
        lastClicked = mission;
    }

    public static void RemoveLastClickedMission()
    {
        lastClicked = null;
    }

    public void LoadMap(MissionData data)
    {
        currentMisionData = data;
        missionListField.SetActive(false);
        mapField.SetActive(true);
        //MapController.Instance.Setup();
    }

    public void OpenDialog(string personName, Text personText)
    {
        sentencesPanel.SetActive(true);
        currentPersonText = personText;

        foreach (var person in currentMisionData.personDialogs)
        {
            if (person.personName == personName)
                currentConversation = person.conversation;
        }

        if (currentConversation.Items.Count > 0)
            SetDialogPart(currentConversation.Items[0]);
    }

    public void SetDialogPart(ConversationItem item)
    {
        currentPersonText.text = item.Text;
        if (item.animator != null)
        {
            //animator.runtimeAnimatorController = item.animator;
        }
        if (item.audioClip != null)
        {
            //if (item.audioClip.Length == 1)
            //{
            //    components.audioSource.clip = item.audioClip[0];
            //    components.audioSource.Play();
            //}
            //else
            //{
            //    StartCoroutine(PlayingMusic(item));
            //}

        }

        List<ConversationItem> children = currentConversation.FindItemChilds(item);

        for (int j = 0; j < children.Count; j++)
        {
            GameObject prefab = Resources.Load<GameObject>("Sentence");
            GameObject sentence = Instantiate(prefab, sentencesContent);
            sentence.GetComponent<Sentence>().Init(children[j]);
            sentence.GetComponent<Sentence>().ReplyToSim += ReplyToSim;
            missionAnswers.Add(sentence.GetComponent<Sentence>());
        }
    }

    public void ReplyToSim(ConversationItem item)
    {
        Instance.RemoweAnswers();
        dialogPart++;

        List<ConversationItem> newItem = currentConversation.FindItemChilds(item);

        if (newItem.Count > 1)
        {
            Debug.LogError(item.ID + " have more than one child!");
        }
        else if (newItem.Count == 0)
        {
            ExitDialog();
            slider.BreakZoom();
            currentPersonText.text = "";
            //animator.runtimeAnimatorController = null;
            //ShowDialogs();
            //LevelManager.Instance.ShowMissions();
            dialogPart = 0;
            //Translator.Disable();
        }
        else
        {
            SetDialogPart(newItem[0]);
        }
    }

    public void RemoweAnswers()
    {
        for (int i = 0; i < missionAnswers.Count; i++)
        {
            Destroy(missionAnswers[i].gameObject);
        }
        missionAnswers.Clear();
    }

    public void ExitDialog()
    {
        sentencesPanel.SetActive(false);
    }
}
