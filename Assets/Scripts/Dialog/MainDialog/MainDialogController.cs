using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDialogController : MonoBehaviour
{
    public static MainDialogController Instance;

    [SerializeField]
    private MainDialogComponents components;
    [SerializeField]
    private MainDialogTranslator translator;

    private List<Sentence> dialogAnswers = new List<Sentence>();
    private int dialogPart;

    private Conversation conversation;

    public void Setup()
    {
        SetTranslator();
        conversation = DialogController.GetDialog().GetData().conversation;

        if (DialogController.GetDialog().GetData().idleVid != null)
        {
            components.viodePlayer.clip = DialogController.GetDialog().GetData().idleVid;
            //components.personImage.sprite = null;
        }
        else
            components.personImage.sprite = DialogController.GetDialog().GetData().teksture;

        if (DialogController.GetDialog().GetData().conversation.Items.Count > 0)
            SetDialogPart(DialogController.GetDialog().GetData().conversation.Items[0]);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public List<Sentence> GetSentences()
    {
        return dialogAnswers;
    }

    public void RemoweAnswers()
    {
        for (int i = 0; i < dialogAnswers.Count; i++)
        {
            Destroy(dialogAnswers[i].gameObject);
        }
        dialogAnswers.Clear();
    }

    public void SetTranslator()
    {
        Translator.translator = translator;
    }

    public void SetDialogPart(ConversationItem item)
    {
        components.botText.text = item.Text;// dialogPart[part].sentense;
        if (item.videoClip != null)
        {
            components.viodePlayer.clip = item.videoClip;
            components.viodePlayer.loopPointReached += EndReached;
        }
        if (item.audioClip != null)
        {
            components.audioSource.clip = item.audioClip;
            components.audioSource.Play();
        }

        List<ConversationItem> children = conversation.FindItemChilds(item);

        for (int j = 0; j < children.Count; j++)
        {
            GameObject prefab = Resources.Load<GameObject>("Sentence");
            GameObject sentence = Instantiate(prefab, components.sentencesContent);
            sentence.GetComponent<Sentence>().Init(children[j]);
            dialogAnswers.Add(sentence.GetComponent<Sentence>());
        }
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.clip = vp.clip = DialogController.GetDialog().GetData().idleVid;
    }


    public void ReplyToSim(ConversationItem item)
    {
        Instance.RemoweAnswers();
        dialogPart++;

        List<ConversationItem> newItem = conversation.FindItemChilds(item);

        if (newItem.Count > 1)
        {
            Debug.LogError(item.ID + " have more than one child!");
        }
        else if (newItem.Count == 0)
        {
            DialogController.Instance.ShowDialogs();
            LevelManager.Instance.ShowMissions();
            dialogPart = 0;
            Translator.Disable();
        }
        else
        {
            SetDialogPart(newItem[0]);
        }
    }
}
