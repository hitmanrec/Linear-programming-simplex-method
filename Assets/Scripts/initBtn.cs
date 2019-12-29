using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initBtn : MonoBehaviour
{
    public mainController mainCtrl;
    public TMPro.TMP_InputField VariableInp, RestrictionsInp;
    public GameObject initPage, workPage;

    public void initialize()
    {
        string varN=VariableInp.text, restN=RestrictionsInp.text;
        if(varN != "" && restN != "")
        {
            mainCtrl.totalVariables = int.Parse(varN);
            mainCtrl.totalRestrictions = int.Parse(restN);
            if(mainCtrl.totalVariables <= 10 && mainCtrl.totalRestrictions <= 10)
            {
                initPage.SetActive(false);
                workPage.SetActive(true);
            }
            else
            {
                mainCtrl.totalVariables = 0;
                mainCtrl.totalRestrictions = 0;
            }
        }
    }
}
