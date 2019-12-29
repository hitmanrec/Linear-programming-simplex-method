using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showParsed : MonoBehaviour
{
    public TMP_Text mainF;
    public GameObject restContent;
    public GameObject restPrefab;
    public mainController mainCtrl;

    private void OnEnable()
    {
        string s = (mainCtrl.indexesMain[0] == 0) ? "" : "" + mainCtrl.indexesMain[0] + "*x1";
        for (int i = 1, l = mainCtrl.indexesMain.Length; i < l; i++)
        {
            string tmp = "";
            if (mainCtrl.indexesMain[i] == 0) continue;
            tmp += (mainCtrl.indexesMain[i] < 0) ? "-" + Mathf.Abs(mainCtrl.indexesMain[i]) : "+" + mainCtrl.indexesMain[i];
            s += tmp + "*x" + (i + 1);
        }
        s += (mainCtrl.mFunConst < 0) ? "-" + Mathf.Abs(mainCtrl.mFunConst) : "+" + mainCtrl.mFunConst;
        s += " для " + ((mainCtrl.toMax) ? "max" : "min");
        mainF.text = s;
        if (restContent.transform.childCount > 0)
        {
            foreach(Transform child in restContent.transform)
            {
                Destroy(child.gameObject);
            }
        }
        int count = FindObjectOfType<mainController>().totalRestrictions;
        for (int i = 0; i < count; i++)
        {
            var instance = Instantiate(restPrefab.gameObject, restContent.transform);
            instance.transform.SetParent(restContent.transform, false);
            instance.GetComponent<TMP_Text>().text = makeString(ref mainCtrl.rests[i]);
        }
        
    }

    private string makeString(ref restriction rest)
    {
        string s = (rest.indexes[0]==0)? "" : "" + rest.indexes[0] + "*x1";
        for(int i = 1, l = rest.indexes.Length; i < l; i++)
        {
            string tmp = "";
            if (rest.indexes[i] == 0) continue;
            tmp += (rest.indexes[i] < 0) ? "-" + Mathf.Abs(rest.indexes[i]) : "+" + rest.indexes[i];
            s += tmp + "*x" + (i + 1);
        }
        if(rest.type == 0)
        {
            s += ">=";
        }
        else
        {
            s += "=";
        }
        s += rest.b;
        return s;
    }
}
