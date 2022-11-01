using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission6 : MonoBehaviour
{
    bool isDrag;
    float leftY, rightY;
    public bool[] isColor = new bool[4];
    public RectTransform[] rights;
    public LineRenderer[] lines;
    Color leftC, rightC;
    Vector2 clickPos;
    LineRenderer line;
    Animator anim;
    PlayerController playerController_script;
    MissionController missionController_script;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        missionController_script = FindObjectOfType<MissionController>();
    }

    private void Update()
    {
        if (isDrag)
        {
            line.SetPosition(1, new Vector3((Input.mousePosition.x - clickPos.x) * 1920f / Screen.width, (Input.mousePosition.y - clickPos.y) * 1080f / Screen.height, -10));

            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit))
                {
                    GameObject rightLine = hit.transform.gameObject;

                    rightY = rightLine.GetComponent<RectTransform>().anchoredPosition.y;
                    rightC = rightLine.GetComponent<Image>().color;

                    line.SetPosition(1, new Vector3(500, rightY - leftY, -10));

                    if(leftC == rightC)
                    {
                        switch(leftY)
                        {
                            case 225:
                                isColor[0] = true; break;
                            case 75:
                                isColor[1] = true; break;
                            case -75:
                                isColor[2] = true; break;
                            case -225:
                                isColor[3] = true; break;
                        }
                    }
                    else
                    {
                        switch (leftY)
                        {
                            case 225:
                                isColor[0] = false; break;
                            case 75:
                                isColor[1] = false; break;
                            case -75:
                                isColor[2] = false; break;
                            case -225:
                                isColor[3] = false; break;
                        }
                    }

                    if (isColor[0] && isColor[1] && isColor[2] && isColor[3])
                        Invoke("MissionSuccess", 0.2f);
                }
                else
                    line.SetPosition(1, new Vector3(0, 0, -10));

                isDrag = false;
            }
        }
    }

    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerController_script = FindObjectOfType<PlayerController>();

        for (int i = 0; i < 4; i++)
        {
            isColor[i] = false;
            lines[i].SetPosition(1, new Vector3(0, 0, 10));
        }

        for (int i = 0; i < rights.Length; i++)
        {
            Vector3 temp = rights[i].anchoredPosition;

            int rand = Random.Range(0, 4);
            rights[i].anchoredPosition = rights[rand].anchoredPosition;

            rights[rand].anchoredPosition = temp;
        }
    }

    public void ClickCancel()
    {
        anim.SetBool("isUp", false);
        playerController_script.MissionEnd();
    }

    public void ClickLine(LineRenderer click)
    {
        clickPos = Input.mousePosition;
        line = click;

        leftY = click.transform.parent.GetComponent<RectTransform>().anchoredPosition.y;
        leftC = click.transform.parent.GetComponent<Image>().color;

        isDrag = true;
    }

    public void MissionSuccess()
    {
        ClickCancel();

        missionController_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
