using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class validateRestrict : MonoBehaviour
{
    public void validateRest()
    {
        TMPro.TMP_InputField text = gameObject.GetComponent<TMPro.TMP_InputField>();
        int l = text.text.Length;
        if (l <= 0) return;
        char ch = text.text[l - 1];
        if (!(ch >= '0' && ch <= '9' || ch == '.' || ch == ' ' || ch=='-'))
        {
            text.text = text.text.Remove(l-1, 1);
        }
    }
}
