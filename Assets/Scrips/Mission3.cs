using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission3 : MonoBehaviour
{
    public Text inputText, KeyCode;
    Animator anim;
    PlayerController playerController_script;
    MissionController missionController_script;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        missionController_script = FindObjectOfType<MissionController>();
    }

    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerController_script = FindObjectOfType<PlayerController>();

        inputText.text = "";
        KeyCode.text = "";

        for (int i = 0; i < 5; i++)
        {
            KeyCode.text += Random.Range(0, 10);
        }
    }

    public void ClickCancel()
    {
        anim.SetBool("isUp", false);
        playerController_script.MissionEnd();
    }

    public void ClickNumber()
    {
        if(inputText.text.Length <= 4)
            inputText.text += EventSystem.current.currentSelectedGameObject.name;
    }

    public void ClickDelete()
    {
        if(inputText.text != "")
            inputText.text = inputText.text.Substring(0, inputText.text.Length - 1);
    }

    public void ClickCheck()
    {
        if (inputText.text == KeyCode.text)
            MissionSuccess();
    }

    public void MissionSuccess()
    {
        ClickCancel();

        missionController_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
