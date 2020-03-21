using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator anim;



    public void Exit_btn()
    {
        Application.Quit();
    }

    public void Start_btn()
    {
        anim.SetTrigger("in");
    }

    public void Settings_btn()
    {
        anim.SetTrigger("openSettings");
    }

    public void QuitSettings_btn()
    {
        anim.SetTrigger("closeSettings");
    }
}
