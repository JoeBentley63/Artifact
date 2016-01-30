using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WallTransitions : MonoBehaviour
{

    public List<int> cameraPositions;
    public int currentIndex = 0;

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
