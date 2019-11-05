using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RawImage>().material = new Material(GetComponent<RawImage>().material);

        var rect = (RectTransform)transform;
        // GetComponent<BoxCollider2D>().size = rect.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
