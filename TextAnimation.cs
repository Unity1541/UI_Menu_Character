using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 必須包含這個命名空間來操作 TextMeshPro
using DG.Tweening;  // 必須包含這個命名空間來使用 DOTween

public class TextAnimation : MonoBehaviour
{
    public TMP_Text marqueeTextMeshPro;  // 拖入你的 TextMeshPro 元素
    public string fullText;   // 你想顯示的完整文字
    public float duration = 2f;  // 控制文字逐字出現的時間
    void Start()
    {
       fullText = marqueeTextMeshPro.text;
       StartMarquee();
    }

    void StartMarquee()
    {
        // 使用 DOTween.To 模擬逐字顯示
        int textLength = fullText.Length;
        DOTween.To(() => 0, x =>
        {
            marqueeTextMeshPro.text = fullText.Substring(0, x);  // 每次更新文字
        }, textLength, duration)
        .SetEase(Ease.Linear);  // 逐字顯示的過渡效果
    }

   
}
