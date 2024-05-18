using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCurrentRectPos : MonoBehaviour
{
    public RectTransform myRect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("POS " + myRect.position.y);
        Debug.Log("LOCALPOS " + myRect.localPosition.y);
    }
}
