using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission4 : MonoBehaviour
{
    public Transform numbers;
    public Color blue;
    Animator anim;
    PlayerController playerController_script;
    MissionController missionController_script;
    int count;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        missionController_script = FindObjectOfType<MissionController>();
    }

    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerController_script = FindObjectOfType<PlayerController>();

        for (int i = 0; i < numbers.childCount; i++)
        {
            numbers.GetChild(i).GetComponent<Image>().color = Color.white;
            numbers.GetChild(i).GetComponent<Button>().enabled = true;
        }

        for (int i = 0; i < 10; i++)
        {
            Sprite temp = numbers.GetChild(i).GetComponent<Image>().sprite;

            int rand = Random.Range(0, 10);
            numbers.GetChild(i).GetComponent<Image>().sprite = numbers.GetChild(rand).GetComponent<Image>().sprite;

            numbers.GetChild(rand).GetComponent<Image>().sprite = temp;
        }

        count = 1;
    }

    public void ClickCancel()
    {
        anim.SetBool("isUp", false);
        playerController_script.MissionEnd();
    }

    public void ClickNumber()
    {
        if(count.ToString() == EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name)
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = blue;
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;

            count++;

            if(count == 11)
            {
                Invoke("MissionSuccess", 0.2f);
            }
        }
    }

    public void MissionSuccess()
    {
        ClickCancel();

        missionController_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
