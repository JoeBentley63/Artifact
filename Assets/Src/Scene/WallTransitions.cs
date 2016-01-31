using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WallTransitions : MonoBehaviour
{

    public List<int> cameraPositions;
    public List<Vector3> cameraLocations;
    public int currentIndex = 3;
    public ObjectRotator objectRotator;
    void Start()
    {
        moveScreen();
        objectRotator = this.gameObject.GetComponent<ObjectRotator>();
    }

    public void moveRight()
    {
        if (currentIndex + 1 >= cameraPositions.Count) currentIndex = 0;
        else currentIndex++;

        moveScreen();
    }

    public void moveLeft()
    {
        if (currentIndex - 1 < 0) currentIndex = cameraPositions.Count - 1;
        else currentIndex--;

        moveScreen();
    }

    private void moveScreen()
    {
        iTween.RotateTo(this.gameObject, new Vector3(0, this.cameraPositions[currentIndex], 0), 1f);
        iTween.MoveTo(this.gameObject, this.cameraLocations[currentIndex], 1f);
        if(currentIndex == 4) objectRotator.atDesk = true; else objectRotator.atDesk = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.currentIndex = 0;
            moveScreen();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.currentIndex = 1;
            moveScreen();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            this.currentIndex = 2;
            moveScreen();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            this.currentIndex = 3;
            moveScreen();
        }
    }
}
