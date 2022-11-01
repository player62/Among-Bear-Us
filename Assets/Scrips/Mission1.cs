using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission1 : MonoBehaviour
{
    public Color red;
    public Image[] images;
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

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.white;
        }

        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, 7);

            images[rand].color = red;
        }
    }

    public void ClickCancel()
    {
        anim.SetBool("isUp", false);
        playerController_script.MissionEnd();
    }

    public void ClickButton()
    {
        Image img = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();

        if(img.color == Color.white)
        {
            img.color = red;
        }
        else
        {
            img.color = Color.white;
        }

        int count = 0;

        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].color == Color.white)
            {
                count++;
            }
        }

        if (count == images.Length)
        {
            Invoke("MissionSuccess", 0.2f);
        }
    }

    public void MissionSuccess()
    {
        ClickCancel();

        missionController_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
