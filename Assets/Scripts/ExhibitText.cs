using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhibitText : MonoBehaviour
{
    public static ExhibitText Instance;

    [SerializeField]
    private Text leftText;
    [SerializeField]
    private Text centerText;
    [SerializeField]
    private Text rightText;
    [SerializeField]
    private Image backgroundPanel;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Text GetText(Position textPos)
    {
        switch (textPos)
        {
            case Position.Left:
                return leftText;
            case Position.Center:
                return centerText;
            case Position.Right:
                return rightText;
            default:
                Debug.LogWarning("Nieobsługiwany typ danych!");
                break;
        }

        return null;
    }

    public Image GetBackground()
    {
        return backgroundPanel;
    }

    public enum Position
    {
        Left,
        Center,
        Right
    }
}


