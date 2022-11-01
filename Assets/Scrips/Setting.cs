using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public bool isJoyStick;
    public Image touchBtn, joyStickBtn;
    public Color blue;
    public PlayerController playerController_script;
    GameObject mainView, playView;

    public void Start()
    {
        mainView = playerController_script.mainView;
        playView = playerController_script.playView;
    }

    public void ClickSetting()
    {
        gameObject.SetActive(true);
        playerController_script.isCantMove = true;
    }

    public void ClickBack()
    {
        gameObject.SetActive(false);
        playerController_script.isCantMove = false;
    }

    public void ClickTouch()
    {
        isJoyStick = false;
        touchBtn.color = blue;
        joyStickBtn.color = Color.white;
    }

    public void ClickJoystick()
    {
        isJoyStick = true;
        touchBtn.color = Color.white;
        joyStickBtn.color = blue;
    }

    public void ClickQuit()
    {
        mainView.SetActive(true);
        playView.SetActive(false);

        playerController_script.DestroyPlayer();
    }
}
