using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FicheController : MonoBehaviour
{
    public static FicheController Instance;

    private FicheComponents components;
    private FicheTranslator translator;

    private int part = 0;
    private List<Word> wordsToLern;
    private Word currentWord;



    void Awake()
    {
        if (Instance == null)
            Instance = this;

        components = GetComponent<FicheComponents>();
        translator = GetComponent<FicheTranslator>();
    }

    public void Setup(DictionaryData data)
    {
        SetTranslator();

        LoadWords(data);
        TryLoadWord(false);
    }

    private void SetTranslator()
    {
        Translator.translator = translator;
    }

    private void LoadWords(DictionaryData data)
    {
        List<Word> allWords = data.knowWords;

        wordsToLern = allWords;
    }

    private void TryLoadWord(bool animation)
    {
        Debug.Log("asd");
        if (wordsToLern.Count > part)
            LoadWord(animation, wordsToLern[part]);
        else
            Exit();
    }

    private void LoadWord(bool animationPlay, Word word)
    {
        currentWord = word;

        components.polishName.text = word.inPoland;
        components.spanishName.text = word.inSpanish;
        ShowInSpanish(animationPlay);

        part++;
    }

    private void Exit()
    {
        wordsToLern = new List<Word>();
        part = 0;

        LevelManager.Instance.ShowDialogs();
        HideWordOptions();
        DictionaryController.Instance.ShowList();
    }

    private void ShowWordOptions()
    {
        components.optionsFied.SetActive(true);
    }

    private void HideWordOptions()
    {
        components.optionsFied.SetActive(false);
    }

    public void ShowInPolish()
    {
        ShowWordOptions();
        GetComponent<Animator>().SetTrigger("Flip");
    }

    private void ShowInSpanish(bool animationPlay)
    {
        if (animationPlay)
            GetComponent<Animator>().SetTrigger("Unflip");
    }

    public void KnowThisWord_button()
    {
        PlayerData.Instance.SaveWod(currentWord);

        HideWordOptions();
        ShowInSpanish(true);
        TryLoadWord(true);
    }

    public void NotKnowThisWord_button()
    {
        HideWordOptions();
        ShowInSpanish(true);
        TryLoadWord(true);
    }
}
