using UnityEngine;
using System.Collections;

public class FadeAndDelete : MonoBehaviour {
    public float timeToFade;
    private float startTime;
    private bool shouldCount = false;

    public void StartFade() {
        startTime = Time.time;
        shouldCount = true;
	}
	
	void Update () {
        if (shouldCount) {
            if(Time.time - startTime > 3.8f)
            {
                iTween.ScaleTo(this.gameObject, Vector3.zero, 1f);
            }
            if (Time.time - startTime > 6)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
