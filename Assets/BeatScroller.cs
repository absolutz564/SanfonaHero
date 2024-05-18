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
    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo / 60f;
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

        } else
        {
            this.GetComponent<RectTransform>().position -= new Vector3 (0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
