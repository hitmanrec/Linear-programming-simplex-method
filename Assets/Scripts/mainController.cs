using System;
using UnityEngine;

[Serializable]
public struct restriction
{
    public float[] indexes;
    public int type;
    public float b;
}

public class mainController : MonoBehaviour
{
    public int totalVariables = 0;
    public int totalRestrictions = 0;
    public restriction[] rests;
    public float[] indexesMain;
    public float mFunConst = 0;
    public bool toMax;

    public void eraseIfNotDouble(TMPro.TMP_InputField textInp)
    {
        string text = textInp.text;
        foreach (var t in text)
        {
            if (!(t >= '0' && t <= '9' || t == '.'))
            {
                textInp.text = "";
                break;
            }
        }
    }

    public void eraseIfNotInt(TMPro.TMP_InputField textInp)
    {
        string text = textInp.text;
        foreach (var t in text)
        {
            if (!(t >= '0' && t <= '9'))
            {
                textInp.text = "";
                break;
            }
        }
    }
}
