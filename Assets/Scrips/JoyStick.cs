using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    public RectTransform stick;
    public RectTransform backGround;
    PlayerController playerController;
    Animator anim;

    bool isDrag;
    float limit;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        limit = backGround.rect.width * 0.5f;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isDrag)
        {
            Vector2 vec = Input.mousePosition - backGround.position;
            stick.localPosition = Vector2.ClampMagnitude(vec, limit);

            Vector3 dir = (stick.position - backGround.position).normalized;
            transform.position += dir * playerController.speed * Time.deltaTime;

            anim.SetBool("isWalk", true);

            if (dir.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);

            if (Input.GetMouseButtonUp(0))
            {
                stick.localPosition = new Vector3(0, 0, 0);

                anim.SetBool("isWalk", false);
                isDrag = false;
            }
        }
    }

    public void ClickStick()
    {
        isDrag = true;
    }
}
