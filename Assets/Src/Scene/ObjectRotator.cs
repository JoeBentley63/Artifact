using UnityEngine;
using System.Collections;

public class ObjectRotator : MonoBehaviour {

    public GameObject modelToRotate;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;
    private Transform originalTransform;
    private Camera thisCamera;
    private bool isFocusedOnObject = false;
    private Vector3 previousMousePosition = Vector3.zero;
    private int newFarClippingPlane = 100;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject showHideButton;
    public float zoomOffset = 0f;
    public bool atDesk = false;

    public GameObject cube;
    public FadeAndDelete fadeAndDelete;
    void Start()
    {
        this.thisCamera = this.gameObject.GetComponent<Camera>();
        showUIArrows();
    }

    void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        if (isFocusedOnObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "endGame")
                    {
                        cube.GetComponent<Animator>().enabled = true;
                        fadeAndDelete.StartFade();
                    }
                }
            }
            if (Input.GetMouseButton(0))
            {
                float RotationSpeed = 500;
                modelToRotate.transform.Rotate(0, (-Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime), (Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime), Space.World);
            }
            if((Input.mouseScrollDelta.y < 0f && zoomOffset + Input.mouseScrollDelta.y > -20) || (Input.mouseScrollDelta.y + Input.mouseScrollDelta.y > 0f && zoomOffset < 40f))
            {
                modelToRotate.transform.localScale = modelToRotate.transform.localScale * (1 + Input.mouseScrollDelta.y * .04f);
                zoomOffset += Input.mouseScrollDelta.y;
            }
            
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                findClickedGameObject();
            }
        }
        previousMousePosition = currentMousePosition;
    }

    void findClickedGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Interactable"){
                this.modelToRotate = hit.transform.gameObject;
                this.originalPosition = modelToRotate.transform.position;
                this.originalRotation = modelToRotate.transform.localRotation;
                this.originalScale = modelToRotate.transform.localScale;
                this.originalTransform = modelToRotate.transform;
                pickUpItem();
            }
        }
    }

    void pickUpItem()
    {
        iTween.MoveTo(modelToRotate, this.gameObject.transform.position + this.gameObject.transform.forward * 4, 1f);
        iTween.RotateTo(modelToRotate, new Vector3(0, 0, 0), 1f);
        iTween.ScaleBy(modelToRotate, new Vector3(.20f, .20f, .20f), 1f);
        newFarClippingPlane = 35;
        if (atDesk) newFarClippingPlane = 100;
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", 100,
            "to", newFarClippingPlane,
            "time", .1f,
            "onupdate", "farPlaneChangeOnUpdate"
            ));
        isFocusedOnObject = true;
        hideUIArrows();
        this.zoomOffset = 0f;
    }

    private void farPlaneChangeOnUpdate(Quaternion newValue)
    {
        this.modelToRotate.transform.localRotation = newValue;
    }

    private void farPlaneChangeOnUpdate(int newValue)
    {
        this.thisCamera.farClipPlane = newValue;
    }

    public void putObjectBack()
    {
        iTween.MoveTo(modelToRotate, originalPosition * 1, 1f);
        modelToRotate.transform.localRotation = originalRotation;
        iTween.ScaleTo(modelToRotate, originalScale, 1f);
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", 60,
            "to", 100,
            "time", .5f,
            "onupdate", "farPlaneChangeOnUpdate"
            ));
        isFocusedOnObject = false;
        showUIArrows();
    }

    private void hideUIArrows()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        showHideButton.SetActive(true);
    }

    private void showUIArrows()
    {
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
        showHideButton.SetActive(false);
    }
}
