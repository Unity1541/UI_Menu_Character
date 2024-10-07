using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;//加上去才可以用PointerEventData
using UnityEngine.UI;//加上去才可以用GraphicRaycaster
using DG.Tweening;
using TMPro; // 確保導入這個命名空間

public class CursorDetectTest : MonoBehaviour
{
    public static int characterIndex=0;//用了static表示其他類別可以直接拿來用，不用再GetComponent<CursorDetectTest>()了
    public Transform raycastCharacter;
    private Image lastImage;
    public Image image;
    public GraphicRaycaster gr;
    private PointerEventData pointerEventData = new PointerEventData(null);
    //PointerEventData 包含了關於指針事件（例如滑鼠點擊、滑鼠移動、觸控等）的數據，包括指針位置、按鍵狀態、觸控信息等。
    //它在 UI 射線檢測（Raycasting）和其他 UI 交互事件中非常有用。
    void Start()
    {
        
    }
    void Update()
    {
//pointerEventData.position：這行代碼是將指針事件數據的 position 屬性設置為當前物件的螢幕座標。pointerEventData 包含關於指針（例如滑鼠或觸控）的事件數據，而 position 是指滑鼠或觸控點的位置。
//Camera.main.WorldToScreenPoint(transform.position)：這裡使用的是 Camera.main，即主攝影機，來將物件的世界座標 (transform.position) 轉換為螢幕座標（ScreenPoint）。
//WorldToScreenPoint 函數將世界空間的座標轉換為螢幕上的 2D 坐標，這樣才能在 UI 射線檢測中使用。
        pointerEventData.position = Camera.main.WorldToScreenPoint(transform.position);
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);
//也就是說，在pointerEventData中的位置，用gr.Raycast檢測附近有無UI相關訊息，然後傳給results
//List<RaycastResult>：這行代碼創建了一個空的 RaycastResult 列表，名為 results。
//RaycastResult 是 UI 射線檢測的結果，它包含了有關 UI 元素（例如按鈕或圖像）被檢測到的詳細信息。
//這裡的 results 將用來存儲所有被射線檢測到的 UI 元素
    if (results.Count > 0)//有偵測到東西
    {
        raycastCharacter = results[0].gameObject.transform;//第一個被偵測到的物件
        if (raycastCharacter!=null)//要多這個判斷，不然會直接出錯
        {
            Debug.Log("" + raycastCharacter.gameObject.name);
            Transform selectBorder = raycastCharacter.Find("selectBorder");
            //找尋邊框位置，等等要去找Image
            if (selectBorder!=null)//要加上這一段，不然會出錯
            {
                image = selectBorder.GetComponent<Image>();
                if (lastImage != image)//表示移標有變動，上一個圖片跟下一個圖片不同
                {
                    if (lastImage != null)//上一張停留在原地，因此游標要停止了
                    {
                        lastImage.DOKill(); // 停止之前的動畫
                        lastImage.color = Color.clear; // 重置顏色
                    }
                    // 開始新的顏色變化動畫
                    image.DOKill(); // 停止舊的動畫
                    image.color = Color.clear; // 確保從透明開始
                    image.DOColor(Color.red, 0.3f).SetLoops(-1, LoopType.Yoyo);
                    lastImage = image; // 更新上次選擇的邊框，暫時存入一個lastImage拿來判斷等等要移開
                }
            }
            else//加了這一段表示如果沒有偵測到image的話，該怎麼辦?如果不加上這一段會有錯誤
            {//沒有偵測到表示，游標移走了，如果上一個lastImage被移走了，然後還沒有更新
            //那就把lastImage動畫停止，並且清除
                if(lastImage!=null)
                {
                    lastImage.DOKill();
                    lastImage.color = Color.clear;
                    lastImage = null; // 重置最後的選中元素
                }
                    
            }
        characterIndex = DetectIndex(gr,pointerEventData,results,selectBorder);
        }
        
        // 取得最上層的 UI 元素
        //GameObject topUIElement = results[0].gameObject;
        //Debug.Log("Top-most UI Element: " + topUIElement.GetComponent<TextMeshProUGUI>().text);
        //發現只會找到最底層的 UI 元素，這可能是因為 RaycastResult 的排序方式導致的。RaycastResult 的結果列表是根據 UI 元素的繪製順序（depth）來排序的，通常最底層的 UI 元素會出現在列表的最前面。
        //如果你想確定檢測到的 UI 元素是最上層的，你可以根據 RaycastResult 的 depth 屬性來排序。
    }

  }

    public int DetectIndex(GraphicRaycaster gr,PointerEventData pointerEventData,List<RaycastResult> results,Transform selectBorder)
    {
        int index = -1; // 預設為無效的索引

            if (results.Count > 0) // 有偵測到東西
            {
                gr.Raycast(pointerEventData, results);
                RectTransform gridBG = results[0].gameObject.GetComponent<RectTransform>(); // 確保是 gameObject
                if (gridBG != null && selectBorder!=null )//多了一個selectBorder因為有時候會抓到text的RectTransform
                {//因為是要用找到該角色的動畫框撥放閃爍，以及回傳該角色的Index，所以設定這條件確保回傳的是border的index
                    index = gridBG.GetSiblingIndex(); // 取得索引
                }
            }

        return index; // 返回索引，如果沒有偵測到則返回 -1
    }   
}

#region 解釋程式碼        
// DOKill()：停止邊框的顏色動畫。
// DOColor()：啟動一個循環的顏色變化動畫，用來高亮選中的角色邊框。
// Mathf.Clamp()：用來限制游標的移動範圍，避免超出螢幕邊界。
// RaycastResult：
// RaycastResult 是 Unity 引擎內建的一個結構體，屬於 UnityEngine.EventSystems 命名空間。它存儲的是一個射線檢測結果的信息，專門用來處理 UI 元素的檢測。
// 當你使用 UI 射線檢測（例如使用 GraphicRaycaster）時，結果會以 RaycastResult 物件的形式存儲。
// 這個結構體包含了許多與射線檢測相關的信息，比如命中的 GameObject、UI 元素的深度、相交點等。
// List<T>：
// List<T> 是 C# 中通用的集合類型，來自 System.Collections.Generic 命名空間。它是一個可以動態擴展的陣列，這裡 T 是泛型類型參數。在這個例子中，T 是 RaycastResult，
//因此這個列表是用來存儲一系列的 RaycastResult 射線檢測結果。
//GraphicRaycaster.Raycast 方法的功能
//這個方法用來在 UI 介面 上進行射線檢測，特別是用於與 Canvas 上的圖形元素（如按鈕、圖片、面板等）
//進行互動檢測。它會基於指針的位置（滑鼠或觸控點），判斷是否有圖形元素位於該位置，並將結果（RaycastResult）存儲在一個列表中。
#endregion
