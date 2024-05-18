using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public Image theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode keyToPress;
    private bool isColliding;
    // Start is called before the first frame update
    void Start()
    {
     theSR = GetComponent<Image>();
        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress) && GameManager.Instance.startPlaying)
        {
            theSR.sprite = pressedImage;
            List<NoteObject> noteList = GetNoteListByKeyCode(keyToPress);
            if (!isColliding)
            {
                GameManager.Instance.PlayWrongSound();
                GameManager.Instance.NoteMissed();
            }
        }
        if (Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImage;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Notes")
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Notes")
        {
            isColliding = false;
        }
    }

    List<NoteObject> GetNoteListByKeyCode(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.A:
                return GameManager.Instance.NotesA;
            case KeyCode.S:
                return GameManager.Instance.NotesS;
            case KeyCode.D:
                return GameManager.Instance.NotesD;
            case KeyCode.J:
                return GameManager.Instance.NotesJ;
            case KeyCode.K:
                return GameManager.Instance.NotesK;
            default:
                return null;
        }
    }
}
