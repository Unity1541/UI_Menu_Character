using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//用到Image




[CreateAssetMenu(fileName="Models", menuName="DemonSlayers")]
public class DemonSlayerAttributes : ScriptableObject
{
    public int characterIndex;
    public Image []image;
    public GameObject dialogWindows;
    public GameObject objCharacter;
    public enum handleStatus {normal,change};
    public handleStatus status;//把上面的enum變成一個可以接受狀態的變數類別
    
}
