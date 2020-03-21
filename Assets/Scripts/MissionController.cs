using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    public static MissionController Instance;
    public static MissionData currentMisionData;

    private PersonDialog currentPersonDialog;
    private static Mission lastClicked;
    [SerializeField]
    private BackgroudSlider slider;

    [SerializeField]
    private GameObject missionListField;
    [SerializeField]
    private GameObject mainMissionField;
    [SerializeField]
    private GameObject mapPrefabField;

    [SerializeField]
    private GameObject sentencesPanel;

    private MapPerson currentPerson;
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
        mainMissionField.SetActive(true);
        GameObject map = Instantiate(data.mapPrefab, mapPrefabField.transform);
        slider = map.GetComponentInChildren<BackgroudSlider>();
        //MapController.Instance.Setup();
    }

    public void OpenDialog(MapPerson person)
    {
        currentPerson = person;
        sentencesPanel.SetActive(true);

        currentPersonDialog = currentMisionData.FindPersonDialog(person.gameObject.name);

        if (currentPersonDialog.conversation.Items.Count > 0 && currentPersonDialog.conversation.GetMasterItem() != null)
            SetDialogPart(currentPersonDialog.conversation.GetMasterItem());
        else
            Debug.LogWarning("Plik jest nieprawidłowy!");
    }

    public void SetDialogPart(ConversationItem item)
    {
        StopAllCoroutines();
        StartCoroutine(currentPerson.Saying(item.TextInPolish, 120));

        List<ConversationItem> children = currentPersonDialog.conversation.FindItemChilds(item);

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

        List<ConversationItem> newItem = currentPersonDialog.conversation.FindItemChilds(item);

        if (newItem.Count > 1)
        {
            int range = Random.Range(0, newItem.Count);
            SetDialogPart(newItem[range]);

            Debug.LogWarning(item.ID + " have more than one child!");
        }
        else if (newItem.Count == 0)
        {
            ExitMission();
        }
        else
        {
            SetDialogPart(newItem[0]);
        }
    }

    private void ExitMission()
    {
        currentPersonDialog.isComplete = true; //TODO: odkomentować jesli skończą się testy
        ExitDialog();
        slider.BreakZoom();
        StartCoroutine(currentPerson.Saying("", 0));
        dialogPart = 0;
        RemoveLastClickedMission();
    }

    public void ResetCurrentMission()
    {
        foreach (var dialog in currentMisionData.personDialogs)
        {
            dialog.isComplete = false;
        }

        LevelManager.Instance.ShowMissions();
        RemoveLastClickedMission();
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
