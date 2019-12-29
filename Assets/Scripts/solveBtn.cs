using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using TMPro;

public class solveBtn : MonoBehaviour
{
    public GameObject restrContent;
    public mainController mainCtrl;
    public GameObject mainFunc;
    public GameObject workScreen;
    public GameObject resultScreen;

    public void getValues()
    {
        string mFunc = mainFunc.transform.Find("aInp").GetComponent<TMP_InputField>().text;
        mFunc = mFunc.TrimEnd(' ');
        mFunc = mFunc.TrimStart(' ');
        mFunc = mFunc.Replace("−", "-").Replace(".", ",");
        double[] mFuncInd = new double[mainCtrl.totalVariables];
        //float mFuncConst = float.Parse(mainFunc.transform.Find("cInp").GetComponent<TMP_InputField>().text.Replace("−", "-").Replace(".", ","));
        //mainCtrl.mFunConst = mFuncConst;
        string tmp = "";
        for (int i = 0, k = 0, l = mFunc.Length; i < l && k < mainCtrl.totalVariables; i++)
        {
            if(mFunc[i] == ' ' && i != l-1)
            {
                mFuncInd[k] = double.Parse(tmp);
                tmp = "";
                k++;
            }
            else
            {
                tmp += mFunc[i];
            }
        }
        mFuncInd[mainCtrl.totalVariables-1] = double.Parse(tmp);
        mainCtrl.indexesMain = mFuncInd;
        TMP_Dropdown dir = mainFunc.transform.Find("direction").GetComponent<TMP_Dropdown>();
        if(dir.value > 0)
        {
            mainCtrl.toMax = false;
        }
        else
        {
            mainCtrl.toMax = true;
        }
        restriction[] restrictions = new restriction[mainCtrl.totalRestrictions];
        int it = 0;
        foreach(Transform child in restrContent.transform)
        {
            restrictions[it++] = parseRest(child);
        }
        mainCtrl.rests = restrictions;
        workScreen.SetActive(false);
        resultScreen.SetActive(true);
        //gameObject.SetActive(false);
    }

    restriction parseRest(Transform restriction)
    {
        
        string rest = restriction.Find("aInp").GetComponent<TMP_InputField>().text;
        rest = rest.TrimEnd(' ');
        rest = rest.TrimStart(' ');
        rest = rest.Replace("−", "-");
        rest = rest.Replace(".", ",");
        double[] restVars = new double[mainCtrl.totalVariables];
        string tmp = "" + rest[0];
        for (int i = 1, k = 0, l = rest.Length; i < l && k < mainCtrl.totalVariables; i++)
        {
            if (rest[i] == ' ' && i!=l-1)
            {
                restVars[k] = double.Parse(tmp);
                tmp = "";
                k++;
            }
            else
            {
                tmp += rest[i];
            }
        }
        
        restVars[mainCtrl.totalVariables-1] = double.Parse(tmp);
        TMP_Dropdown type = restriction.Find("type").GetComponent<TMP_Dropdown>();
        int nType = type.value;
        restriction res;
        res.indexes = restVars;
        res.type = nType;
        res.b = double.Parse(restriction.Find("bInp").GetComponent<TMP_InputField>().text.Replace("−", "-").Replace(".", ","));
        return res;
    }
}
