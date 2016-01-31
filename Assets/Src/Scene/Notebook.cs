using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Notebook : MonoBehaviour {
    public bool notebookUp;
    public GameObject notebookGameObject;
    public Camera thisCamera;
    public float distance;
    public Transform [] pages;
    public int currentPage = 0;
    public GameObject leftArrow;
    public GameObject rightArrow;
    private bool firstOpen = true;
    // Use this for initialization
    void Start () {
        this.thisCamera = this.gameObject.GetComponent<Camera>();
        hideUIArrows();
    }

    // Update is called once per frame
    void Update()
    {
        if (notebookUp)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                moveNotepadDown();
                
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                moveNotepadUp();
            }
        }
    }

    public void openNoteBook()
    {
        currentPage = 10;
        showUIArrows();
        this.notebookUp = true;
    }

    public void closeNoteBook()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            movePageToTheRight(i);
        }

        this.notebookUp = false;
        hideUIArrows();
    }

    public void buttonPageToTheLeft()
    {
        if(currentPage - 1 >= 0)
        {
            movePageToTheLeft(currentPage);
            currentPage--;
        }
    }

    public void buttonPageToTheRight()
    {
        if (currentPage + 1 < pages.Length)
        {
            movePageToTheRight(currentPage);
            currentPage++;
        }
    }

    public void toggleJournal()
    {
        if (this.notebookUp)
        {
            moveNotepadDown();
        }
        else
        {
            moveNotepadUp();
        }

    }

    private void movePageToTheLeft(int pageNumber)
    {
        //pages[pageNumber].Rotate(new Vector3(0, 0, -140), Space.Self);
        iTween.RotateTo(pages[pageNumber].gameObject, iTween.Hash(
            "rotation", new Vector3(0, 0, 211.6651f - pageNumber*2),
            "time", 1 + pageNumber / 2,
            "islocal", true
            ));
    }

    private void movePageToTheRight(int pageNumber)
    {
        iTween.RotateTo(pages[pageNumber].gameObject, iTween.Hash(
            "y", -148.3349,
            "rotation", new Vector3(0, 0, 358.8168f - pageNumber),
            "time", 1 + pageNumber/2,
            "islocal", true
            ));
    }

    private void moveNotepadUp()
    {
        iTween.MoveTo(this.notebookGameObject, this.thisCamera.gameObject.transform.position + this.thisCamera.gameObject.transform.forward * distance, 1f);
        openNoteBook();
    }

    private void moveNotepadDown()
    {
        iTween.MoveTo(this.notebookGameObject, this.thisCamera.gameObject.transform.position + new Vector3(0, -10, 0), 1f);
        closeNoteBook();
    }

    private void hideUIArrows()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }

    private void showUIArrows()
    {
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
    }
}
