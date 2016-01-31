using UnityEngine;
using System.Collections;

public class RenderingOrder : MonoBehaviour {
    public int sortingOrder;
	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Renderer>().sortingOrder = sortingOrder;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
