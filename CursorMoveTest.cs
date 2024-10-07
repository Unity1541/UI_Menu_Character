using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMoveTest : MonoBehaviour
{
    public float speed=0;
    public Vector3 worldSize;
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        transform.position += new Vector3(x, y, 0) * Time.deltaTime * speed;
        //Debug.Log("Transform"+transform.position);
        // worldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,Camera.main.nearClipPlane));
        // //如果取不到座標可以改用Vector3加上Camera.main.nearClipPlane當Z座標
        // transform.position = new Vector3(Mathf.Clamp(transform.position.x, -worldSize.x, worldSize.x),
        // Mathf.Clamp(transform.position.y, -worldSize.y+2.0f, worldSize.y),
        // transform.position.z);
        //如果是2D相機配合指標移動的話，用Orthographic相機比較合適
    }

// 座標系統的區別
// 螢幕空間（Screen Space）：以像素為單位，原點 (0, 0) 在螢幕的左下角，Screen.width 和 Screen.height 代表螢幕的寬度和高度。
// 例如，當你處理 UI 元素或偵測滑鼠點擊時，通常使用螢幕空間座標。
// 世界空間（World Space）：遊戲中的三維空間，以遊戲物件的實際位置為基準。
// 世界空間的原點 (0, 0, 0) 在遊戲場景的中心，所有物體的位置都是相對於這個原點的。
// 成對應的世界空間位置來進行限制，這時候你可以使用 ScreenToWorldPoint 來獲得螢幕邊界在世界空間中的坐標，從而在世界空間中限制物件的移動範圍。
// 螢幕到遊戲世界的互動：玩家與遊戲的交互，尤其是滑鼠或觸控輸入，通常是基於螢幕座標的。例如，當玩家點擊滑鼠時，你得到的是滑鼠在螢幕上的位置，但遊戲物件通常是在世界空間中。
// 如果你想讓玩家點擊遊戲世界中的一個物件，你需要把螢幕座標轉換成對應的世界座標，這就是 ScreenToWorldPoint 的用途。

// 例子：當玩家點擊一個 3D 物體時，你可以使用ScreenToWorldPoint把滑鼠點擊的螢幕座標轉換為世界座標，以計算點擊的物件或區域。
// 處理 3D 物體的移動或生成：如果你想根據螢幕上的某個位置來移動或生成 D物體，你需要將螢幕座標轉換成世界座標。
// 例子：假設你要讓遊戲中的角色走向滑鼠點擊的位置，或者在螢幕的一個點生成一個物件，這些動作都是基於世界空間中的座標進行的，這時你需要 ScreenToWorldPoint 來進行轉換。
// 限制物件在螢幕邊界內移動：當你處理遊戲物件移動並希望它們不超出螢幕邊界時，需要將螢幕的邊界像素轉換
}