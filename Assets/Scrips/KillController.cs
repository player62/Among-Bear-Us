using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillController : MonoBehaviour
{
    public Transform[] spawnPoint;
    public GameObject kill_anim, text_anim;
    public GameObject mainView;
    int count;
    List<int> number = new List<int>();

    public void KillReset()
    {
        kill_anim.SetActive(false);
        text_anim.SetActive(false);

        number.Clear();

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            if (spawnPoint[i].childCount != 0)
                Destroy(spawnPoint[i].GetChild(0).gameObject);
        }

        NPCSpawn();
    }

    public void NPCSpawn()
    {
        int rand = Random.Range(0, 10);

        for (int i = 0; i < 5;)
        {
            if(number.Contains(rand))
            {
                rand = Random.Range(0, 10);
            }
            else
            {
                number.Add(rand);
                i++;
            }
        }

        for (int i = 0; i < number.Count; i++)
        {
            Instantiate(Resources.Load("NPC"), spawnPoint[number[i]]);
        }
    }

    public void Kill()
    {
        count++;

        if(count == 5)
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
