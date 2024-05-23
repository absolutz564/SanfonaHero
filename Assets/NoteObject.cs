using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    public Mask GameMask;
    public RectTransform myRect;
    // Start is called before the first frame update
    public GameObject HitEffect, GoodEffect, PerfectEffect;
    void Start()
    {
        
    }

    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }
    IEnumerator WaitDisableMask()
    {
        yield return new WaitForSeconds(2);
        GameMask.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (myRect != null)
        {
            if (Mathf.Abs(myRect.position.y) > 700f)
            {
                GameMask.enabled = false;
                Debug.Log("Desabilita mascara");
            }
        }
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.GetComponent<Image>().enabled = false;
                //GameManager.Instance.NoteHit();
               if (myRect != null)
                {
                    if (Mathf.Abs(myRect.position.y) > 778f)
                    {
                        Debug.Log("NORMAL HIT");
                        GameManager.Instance.NormalHit();
                        GameObject myObject = Instantiate(HitEffect, myRect.position, myRect.rotation, GameManager.Instance.CanvasTransform);
                    }
                    else if (Mathf.Abs(myRect.position.y) > 777f)
                    {
                        Debug.Log("GOOD HIT");
                        GameManager.Instance.GoodHit();
                        Instantiate(GoodEffect, myRect.position, myRect.rotation, GameManager.Instance.CanvasTransform);
                    }
                    else if (Mathf.Abs(myRect.position.y) < 774.2f)
                    {
                        Debug.Log("PERFECT HIT");
                        GameManager.Instance.PerfectHit();
                        Instantiate(PerfectEffect, myRect.position, myRect.rotation, GameManager.Instance.CanvasTransform);
                    }
                    else
                    {
                        Debug.Log("NORMAL HIT");
                        GameManager.Instance.NormalHit();
                        GameObject myObject = Instantiate(HitEffect, myRect.position, myRect.rotation, GameManager.Instance.CanvasTransform);
                    }

                    if (GameMask != null)
                    {
                        GameMask.enabled = true;
                        StartCoroutine(WaitDisableMask());
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
        if (other.tag == "Wrong" && this.GetComponent<Image>().enabled)
        {
            GameManager.Instance.NoteMissed();
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;
        }
    }
}
