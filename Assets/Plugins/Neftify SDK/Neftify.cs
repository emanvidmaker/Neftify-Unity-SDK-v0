using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;


public class Neftify : MonoBehaviour
{
    public class AcountData {
    
    }
    AcountData data;
    [DllImport("__Internal")]
    public static extern void NeftifySetThemeDefault();
    [DllImport("__Internal")]
    public static extern void NeftifySetThemeBranded();
    [DllImport("__Internal")]
    public static extern void NeftifySetThemeDark();//
    [DllImport("__Internal")]
    public static extern void NeftifyGetConnectionDetails();
    [DllImport("__Internal")]
    public static extern void NeftifyToggleEmoji();//
    [DllImport("__Internal")]
    public static extern void NeftifyToggleModal();//
    [DllImport("__Internal")]
    public static extern void NeftifyGetBalance();//
    [DllImport("__Internal")]
    public static extern void NeftifyUserIsConnected();//
    
    [DllImport("__Internal")]
    public static extern void NeftifyConnectWithCustomButton();
    [DllImport("__Internal")]
    public static extern void NeftifyDisconnectWithCustomButton();

    [DllImport("__Internal")]
    public static extern void NeftifyWindowAlert(string str);

    public  Text Display_connection;
    // Start is called before the first frame update
    void Start()
    {
    }
 
    public void UpdateText(string t)
    {
        Display_connection.text = t;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
