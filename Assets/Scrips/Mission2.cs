using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission2 : MonoBehaviour
{
    public Transform trash, handle;
    public GameObject bottom;
    public Animator animShake;
    bool isDrag, isPlay;
    Animator anim;
    PlayerController playerController_script;
    MissionController missionController_script;
    RectTransform rectHandle;
    Vector2 originPos;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rectHandle = handle.GetComponent<RectTransform>();
        originPos = rectHandle.anchoredPosition;
        missionController_script = FindObjectOfType<MissionController>();
    }

    private void Update()
    {
        if(isPlay)
        {
            if (isDrag)
            {
                handle.position = Input.mousePosition;
                rectHandle.anchoredPosition = new Vector2(originPos.x, Mathf.Clamp(rectHandle.anchoredPosition.y, -135, -47));

                animShake.enabled = true;

                if (Input.GetMouseButtonUp(0))
                {
                    rectHandle.anchoredPosition = originPos;
                    isDrag = false;
                    animShake.enabled = false;
                }
            }

            if (rectHandle.anchoredPosition.y <= -130)
                bottom.SetActive(false);
            else
                bottom.SetActive(true);

            // trash destroy
            for (int i = 0; i < trash.childCount; i++)
            {
                if (trash.GetChild(i).GetComponent<RectTransform>().anchoredPosition.y <= -600)
                    Destroy(trash.GetChild(i).gameObject);
            }

            if (trash.childCount == 0)
            {
                MissionSuccess();
                isPlay = false;
                animShake.enabled = false;
            }
        }
    }

    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerController_script = FindObjectOfType<PlayerController>();

        for (int i = 0; i < trash.childCount; i++)
        {
            Destroy(trash.GetChild(i).gameObject);
        }

        for (int i = 0; i < 10; i++)
        {
            // Apple
            GameObject trash4 = Instantiate(Resources.Load("Trash/Trash4"), trash) as GameObject;
            trash4.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
            trash4.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));

            // Can
            GameObject trash5 = Instantiate(Resources.Load("Trash/Trash5"), trash) as GameObject;
            trash5.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
            trash5.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));
        }

        for (int i = 0; i < 3; i++)
        {
            // Bottle
            GameObject trash1 = Instantiate(Resources.Load("Trash/Trash1"), trash) as GameObject;
            trash1.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
            trash1.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));

            // Fish
            GameObject trash2 = Instantiate(Resources.Load("Trash/Trash2"), trash) as GameObject;
            trash2.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
            trash2.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));

            // Vynil
            GameObject trash3 = Instantiate(Resources.Load("Trash/Trash3"), trash) as GameObject;
            trash3.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
            trash3.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));
        }

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
