using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    public CircleCollider2D[] colls;
    public Slider guage;
    public GameObject text_anim, mainView;
    int missionCount;

    public void MissionReset()
    {
        guage.value = 0;
        missionCount = 0;

        for (int i = 0; i < colls.Length; i++)
        {
            colls[i].enabled = true;
        }

        text_anim.SetActive(false);
    }

    public void MissionSuccess(CircleCollider2D coll)
    {
        missionCount++;

        guage.value = missionCount / 7f;

        coll.enabled = false;

        if (guage.value == 1)
        {
            text_anim.SetActive(true);

            Invoke("Change", 2f);
        }
    }

    public void Change()
    {
        mainView.SetActive(true);
        gameObject.SetActive(false);

        FindObjectOfType<PlayerController>().DestroyPlayer();
    }
}
