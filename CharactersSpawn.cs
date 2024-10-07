using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//用到Image
using DG.Tweening;

public class CharactersSpawn : MonoBehaviour
{
    public DemonSlayerAttributes []charactersAttributes;
    public List<GameObject> characterObj;
    public GameObject currentObject;
    public int currentIndex;
    void Start()
    {
        characterObj=new List<GameObject>();
        
        foreach (var characterAttribute in charactersAttributes)
        {//一開始先在場景創造全部角色
            currentObject = Instantiate(characterAttribute.objCharacter,transform.position,Quaternion.identity);
            currentObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            currentObject.transform.SetParent(this.transform);
            currentObject.SetActive(false);
            characterObj.Add(currentObject);
        }
        currentObject=null;//一開始先建立完所有的角色後塞到list，最後初始值為null;
    }

    void Update()
    {
        currentIndex = CursorDetectTest.characterIndex;
        //用了static表示其他類別可以直接拿來用，不用再GetComponent<CursorDetectTest>()了
        if (currentIndex >= 0 && currentIndex < characterObj.Count)
        {
            if (currentObject != characterObj[currentIndex])
            {
                // 先將所有角色設置為非啟用狀態
                foreach (var obj in characterObj)
                {
                    obj.SetActive(false);
                }

                // 設置 currentObject 為當前索引對應的物件
                currentObject = characterObj[currentIndex];

                // 啟用當前索引的物件
                currentObject.SetActive(true);
            }
        }    
    }

    
}
