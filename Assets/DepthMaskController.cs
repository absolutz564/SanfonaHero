using UnityEngine;
using UnityEngine.UI;

public class DepthMaskController : MonoBehaviour
{
    private bool isCollidingWithNotes = false;
    private bool isCollidingWithDepth = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Notes"))
        {
            isCollidingWithNotes = true;
            Debug.Log("COLIDIU NOTES");
        }
        if (collision.CompareTag("Depth"))
        {
            Debug.Log("COLIDIU Depth");
            isCollidingWithDepth = true;
        }

        UpdateSortingOrder();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Notes"))
        {
            isCollidingWithNotes = false;
            Debug.Log("Saiu colisão Notes");
        }
        if (collision.CompareTag("Depth"))
        {
            isCollidingWithDepth = false;
            Debug.Log("Saiu colisão Depth");
        }

        UpdateSortingOrder();
    }

    private void UpdateSortingOrder()
    {
        if (isCollidingWithNotes && isCollidingWithDepth)
        {
            Debug.Log("Ta em colisao com os dois, deve esconder");
            GetComponent<Image>().canvas.overrideSorting = true;
            GetComponent<Image>().canvas.sortingOrder = -1;
        }
        else
        {
            Debug.Log("Não ta em colisao, exibe");
            GetComponent<Image>().canvas.overrideSorting = true;
            GetComponent<Image>().canvas.sortingOrder = 0;
        }
    }
}
