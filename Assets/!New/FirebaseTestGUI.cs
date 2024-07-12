using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class FirebaseTestGUI : MonoBehaviour
{
    GameObject obj;
    private Dictionary<int, int> map = new Dictionary<int, int>();

    // Update is called once per frame
    void OnGUI()
    {
        int w = Screen.width / 5;
        int h = Screen.width / 20;
        int i = 0;
        int fontSizeOld =   GUI.skin.button.fontSize;
        GUI.skin.button.fontSize = 3*Screen.width / (20*4);
        if (GUI.Button(new Rect(0, h*i++, w, h), "null ref exp"))
            MakeNullRefException();
        if (GUI.Button(new Rect(0, h*i++, w, h), "key exist exp"))
            MakeKeyExistException();
        GUI.skin.button.fontSize = fontSizeOld;
    }

    void MakeNullRefException()
    {
        obj.name = "null ref here";
    }
    
    void MakeKeyExistException()
    {
        map.Add(1,1);
        map.Add(1,1);
    }
}
