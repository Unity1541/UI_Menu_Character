using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;//加上去才可以用PointerEventData
using UnityEngine.UI;//加上去才可以用GraphicRaycaster
using DG.Tweening;
using TMPro; // 確保導入這個命名空間
using UnityEngine.SceneManagement;//切換場景

public class CursorMenuDetect : MonoBehaviour
{
    public Transform raycast;
    private Image lastImage;
    public Image image;
    public GraphicRaycaster gr;
    private PointerEventData pointerEventData = new PointerEventData(null); 
    void Update()
    {
        pointerEventData.position = Camera.main.WorldToScreenPoint(transform.position);
        List<RaycastResult> rayCastResult = new List<RaycastResult>();
        gr.Raycast(pointerEventData,rayCastResult);
        
        if (rayCastResult.Count>0)
        {
            raycast= rayCastResult[0].gameObject.transform;//第一個被偵測到的物件
            if(raycast!=null && raycast.gameObject.GetComponent<Image>() != null)
            {
                image = raycast.gameObject.GetComponent<Image>();
                // 如果有前一個 Image 仍然在閃爍，停止它的動畫
                if (lastImage != null)
                {
                    lastImage.DOKill(); // 停止之前的閃爍
                    lastImage.DOFade(1f, 0.2f); // 將透明度恢復到 1f
                }

                // 對新的目標啟動閃爍效果
                image.DOFade(0f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

                // 更新 lastImage 為當前的 Image
                lastImage = image;
            }    
        }
        else
        {
            // 如果沒有偵測到物件，停止前一個 Image 的動畫
            if (lastImage != null)
            {
                lastImage.DOKill(); // 停止閃爍
                lastImage.DOFade(1f, 0.2f); // 將透明度恢復到 1f
                lastImage = null;   // 重置 lastImage
            }
        }
    }

    public void startGame(string menu)
    {
         SceneManager.LoadScene(menu); // 加載指定名稱的場景
         Debug.Log("有按下按鈕");
    }

}
