using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BeatScroller : MonoBehaviour
{

    public float beatTempo;
    public bool hasStarted;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        //beatTempo = beatTempo / 60f;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown && !GameManager.Instance.Menu.activeSelf && !GameManager.Instance.First.activeSelf && GameManager.Instance.theBS != null)
            {
                hasStarted = true;
            }
        }
        else
        {
            // Calcula a direção do movimento baseado na rotação do objeto
            Vector3 direction = transform.rotation * Vector3.down;
            rectTransform.localPosition += direction * beatTempo * Time.deltaTime;
        }
    }
}