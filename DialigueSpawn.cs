using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialigueSpawn : MonoBehaviour
{

    public GameObject []dialogueWindows;
    public int currentIndex = 0;
    public float targetDirection=-580f;
    public float timeDuration;
    //採用繼承的方式來呼叫對話框
    void Start()
    {
       //RectTransform anchorRectTransform = GameObject.Find("Anchor").GetComponent<RectTransform>();
       //targetDirection = anchorRectTransform.anchoredPosition.x;
       //anchoredPosition =Screen.width-targetDirection;
    }

    // Update is called once per frame
    void Update()
    {
        currentIndex = CursorDetectTest.characterIndex;
        if (currentIndex >= 0 && currentIndex < dialogueWindows.Length)
        {
            for (int i =0;i<dialogueWindows.Length;i++)
            {
                if ( i==currentIndex )
                {
                    dialogueWindows[i].SetActive(true);
                    dialogueWindows[i].GetComponent<RectTransform>().DOAnchorPosX(targetDirection, timeDuration).SetEase(Ease.OutQuad);
                }
                else
                {
                    dialogueWindows[i].SetActive(false);
                    dialogueWindows[i].GetComponent<RectTransform>().DOAnchorPosX(300, timeDuration).SetEase(Ease.OutQuad);
                }
            }
            
        }
    }
}
