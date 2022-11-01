using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Sprite[] idle, dead;
    SpriteRenderer sr;
    int rand;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        rand = Random.Range(0, 5);
        sr.sprite = idle[rand];
    }

    public void Dead()
    {
        sr.sprite = dead[rand];

        sr.sortingOrder = -1;
    }
}
