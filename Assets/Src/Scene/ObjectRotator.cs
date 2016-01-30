using UnityEngine;
using System.Collections;

public class ObjectRotator : MonoBehaviour {

    public GameObject modelToRotate;
    private Vector3 originalPosition;
    private Vector3 originalRotation;
    private Camera thisCamera;
    private bool isFocusedOnObject = false;
    private Vector3 previousMousePosition = Vector3.zero;
    void Start()
    {
        this.originalPosition = modelToRotate.transform.position;
        this.originalRotation = new Vector3(modelToRotate.transform.rotation.x, modelToRotate.transform.rotation.y, modelToRotate.transform.rotation.z);
        this.thisCamera = this.gameObject.GetComponent<Camera>();
        pickUpItem();
    }

    void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        if (isFocusedOnObject)
        {
            if (Input.GetMouseButton(0))
            {
                float RotationSpeed = 500;
                modelToRotate.transform.Rotate(0, (-Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime), (Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime), Space.World);
            }
        }
        previousMousePosition = currentMousePosition;
    }

    void pickUpItem()
    {
        this.thisCamera.orthographic = false;
        iTween.MoveTo(modelToRotate, this.gameObject.transform.position + this.gameObject.transform.forward * 10, 3f);
        iTween.RotateTo(modelToRotate, new Vector3(0, 0, 0), 3f);
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", 100,
            "to", 20,
            "time", 3f,
            "onupdate", "farPlaneChangeOnUpdate"
            ));
        isFocusedOnObject = true;
    }

    private void farPlaneChangeOnUpdate(int newValue)
    {
        this.thisCamera.farClipPlane = newValue;
    }

    public void onPutBackFinished()
    {
        this.thisCamera.orthographic = true;
    }

    public void putObjectBack()
    {
        iTween.MoveTo(modelToRotate, originalPosition * 1, 3f);
        iTween.RotateTo(modelToRotate, originalRotation, 3f);
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", 20,
            "to", 100,
            "time", .2f,
            "onupdate", "farPlaneChangeOnUpdate",
            "oncomplete", "onPutBackFinished"
            ));

        isFocusedOnObject = false;
    }
	   
}
