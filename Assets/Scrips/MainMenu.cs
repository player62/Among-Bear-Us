using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject missionView, killView;

    // 게임 종료
    public void ClickQuit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void ClickMission()
    {
        gameObject.SetActive(false);
        missionView.SetActive(true);

        GameObject player = Instantiate(Resources.Load("Character")) as GameObject;
        player.GetComponent<PlayerController>().mainView = gameObject;
        player.GetComponent<PlayerController>().playView = missionView;
        player.GetComponent<PlayerController>().isMission = true;

        missionView.SendMessage("MissionReset");
    }

    public void ClickKill()
    {
        gameObject.SetActive(false);
        killView.SetActive(true);

        GameObject player = Instantiate(Resources.Load("Character")) as GameObject;
        player.GetComponent<PlayerController>().mainView = gameObject;
        player.GetComponent<PlayerController>().playView = killView;
        player.GetComponent<PlayerController>().isMission = false;

        killView.SendMessage("KillReset");
    }
}
