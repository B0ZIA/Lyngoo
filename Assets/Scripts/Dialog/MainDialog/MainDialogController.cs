using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MainDialogController : MonoBehaviour
{
    public static MainDialogController Instance;

    [SerializeField]
    private MainDialogComponents components;
    [SerializeField]
    private MainDialogTranslator translator;

    public Animator animator;

    private List<Sentence> dialogAnswers = new List<Sentence>();
    private int dialogPart;

    private Conversation conversation;

    public void Setup()
    {
        SetTranslator();
        conversation = DialogController.GetDialog().GetData().conversation;

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
        components.botText.text = item.Text;
        if (item.animator != null)
        {
            animator.runtimeAnimatorController = item.animator;
        }
        if (item.audioClip != null)
        {
            if (item.audioClip.Length == 1)
            {
                components.audioSource.clip = item.audioClip[0];
                components.audioSource.Play();
            }
            else
            {
                StartCoroutine(PlayingMusic(item));
            }
            
        }

        List<ConversationItem> children = conversation.FindItemChilds(item);

        for (int j = 0; j < children.Count; j++)
        {
            GameObject prefab = Resources.Load<GameObject>("Sentence");
            GameObject sentence = Instantiate(prefab, components.sentencesContent);
            sentence.GetComponent<Sentence>().Init(children[j]);
            sentence.GetComponent<Sentence>().ReplyToSim += ReplyToSim;
            dialogAnswers.Add(sentence.GetComponent<Sentence>());
        }
    }

    IEnumerator PlayingMusic(ConversationItem item)
    {
        for (int i = 0; i < item.audioClip.Length; i++)
        {
            float soundLength = item.audioClip[i].length;
            components.audioSource.clip = item.audioClip[i];
            components.audioSource.Play();

            yield return new WaitForSecondsRealtime(soundLength);
        }
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
            animator.runtimeAnimatorController = null;
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
