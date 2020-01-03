using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FicheTranslator : MonoBehaviour, ITranslator
{
    public void Translate()
    {
        FicheController.Instance.ShowInPolish();
    }
}
