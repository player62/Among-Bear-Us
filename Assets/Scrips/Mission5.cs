using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission5 : MonoBehaviour
{
    public Transform rotate, handle;
    public Color blue, red;
    bool isDrag, isPlay;
    Animator anim;
    PlayerController playerController_script;
    MissionController missionController_script;
    RectTransform rectHandle;
    float rand;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rectHandle = handle.GetComponent<RectTransform>();
        missionController_script = FindObjectOfType<MissionController>();
    }

    private void Update()
    {
        if(isPlay)
        {
            if (isDrag)
            {
                handle.position = Input.mousePosition;
                rectHandle.anchoredPosition = new Vector2(184, Mathf.Clamp(rectHandle.anchoredPosition.y, -195, 195));

                if (Input.GetMouseButtonUp(0))
                {
                    if (rectHandle.anchoredPosition.y > -5 && rectHandle.anchoredPosition.y < 5)
                    {
                        Invoke("MissionSuccess", 0.2f);
                        isPlay = true;
                    }

                    isDrag = false;
                }
            }

            rotate.eulerAngles = new Vector3(0, 0, 90 * rectHandle.anchoredPosition.y / 195);

            if (rectHandle.anchoredPosition.y > -5 && rectHandle.anchoredPosition.y < 5)
            {
                rotate.GetComponent<Image>().color = blue;
            }
            else
            {
                rotate.GetComponent<Image>().color = red;
            }
        }
    }

    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerController_script = FindObjectOfType<PlayerController>();

        rand = 0;

        while(rand >= -10 && rand <= 10)
            rand = Random.Range(-195, 195);
        rectHandle.anchoredPosition = new Vector2(184, rand);

        isPlay = true;
    }

    public void ClickCancel()
    {
        anim.SetBool("isUp", false);
        playerController_script.MissionEnd();
    }

    public void ClickHandle()
    {
        isDrag = true;
    }

    public void MissionSuccess()
    {
        ClickCancel();

        missionController_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
