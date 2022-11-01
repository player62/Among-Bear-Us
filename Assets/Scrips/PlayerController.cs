using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public GameObject joyStick, mainView, playView;
    public float speed;
    public Setting setting_script;
    public bool isCantMove, isMission;
    float timer;
    bool isCool;
    bool isAnim;
    public Sprite use, kill;
    public Button Btn;
    public Text coolText;
    GameObject coll;
    Animator anim;
    KillController killController_script;

    private void Start()
    {
        Camera.main.transform.parent = transform;
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        anim = GetComponent<Animator>();

        if (isMission)
        {
            Btn.GetComponent<Image>().sprite = use;

            coolText.text = "";
        }
        else
        {
            killController_script = FindObjectOfType<KillController>();

            Btn.GetComponent<Image>().sprite = kill;

            timer = 5;
            isCool = true;
        }
    }

    private void Update()
    {
        if(isCool)
        {
            timer -= Time.deltaTime;
            coolText.text = Mathf.Ceil(timer).ToString();

            if (coolText.text == "0")
            {
                coolText.text = "";
                isCool = false;
            }
        }

        if (isCantMove)
            joyStick.SetActive(false);
        else
            Move();

        // 애니메이션이 끝났다면
        if(isAnim && killController_script.kill_anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            killController_script.kill_anim.SetActive(false);
            killController_script.Kill();
            isCantMove = false;
            isAnim = false;
        }
    }

    void Move()
    {
        if (setting_script.isJoyStick)
        {
            joyStick.SetActive(true);
        }
        else
        {
            joyStick.SetActive(false);

            if (Input.GetMouseButton(0))
            {
#if UNITY_EDITOR
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;

                    transform.position += dir * speed * Time.deltaTime;

                    anim.SetBool("isWalk", true);

                    if (dir.x < 0)
                        transform.localScale = new Vector3(-1, 1, 1);
                    else
                        transform.localScale = new Vector3(1, 1, 1);
                }
            }
#else
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;

                    transform.position += dir * speed * Time.deltaTime;

                    anim.SetBool("isWalk", true);

                    if (dir.x < 0)
                        transform.localScale = new Vector3(-1, 1, 1);
                    else
                        transform.localScale = new Vector3(1, 1, 1);
                }
#endif
            else
                anim.SetBool("isWalk", false);
        }
    }

    public void DestroyPlayer()
    {
        Camera.main.transform.parent = null;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Mission" && isMission)
        {
            coll = collision.gameObject;
            Btn.interactable = true;
        }

        if(collision.tag == "NPC" && !isMission && !isCool)
        {
            coll = collision.gameObject;
            Btn.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mission" && isMission)
        {
            coll = null;
            Btn.interactable = false;
        }

        if (collision.tag == "NPC" && !isMission)
        {
            coll = null;
            Btn.interactable = false;
        }
    }

    public void ClickButton()
    {
        // 미션일 때
        if (isMission)
        {
            coll.SendMessage("MissionStart");
        }
        // 킬 퀘스트일 때
        else
        {
            Kill();
        }
        
        isCantMove = true;
        Btn.interactable = false;
    }

    void Kill()
    {
        // 죽이는 애니메이션
        killController_script.kill_anim.SetActive(true);
        isAnim = true;

        // 죽은 이미지 변경
        coll.SendMessage("Dead");

        // 죽은 NPC를 다시 죽일 수 없도록 설정
        coll.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void MissionEnd()
    {
        isCantMove = false;
    }
}
