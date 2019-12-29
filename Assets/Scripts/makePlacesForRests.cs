using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makePlacesForRests : MonoBehaviour
{

    public GameObject restWrap;
    public RectTransform content;

    void OnEnable()
    {
        if (content.transform.childCount > 0)
        {
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
        }
        int count = FindObjectOfType<mainController>().totalRestrictions;
        for (int i = 0; i < count; i++)
        {
            var instance = Instantiate(restWrap.gameObject, content);
            instance.transform.SetParent(content, false);
        }
    }

}
