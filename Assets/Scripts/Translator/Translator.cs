using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour
{
    private static ITranslator _translator;
    public static ITranslator translator
    {
        private get { return _translator; }
        set { _translator = value; translate = true; }
    }

    private static bool translate = false;



    public void Translate()
    {
        if (translate)
            translator.Translate();
    }

    public static void Disable()
    {
        translate = false;
    }
}
