using UnityEngine;
using System.Collections;

public class ToggleVisibility : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        float pause = Input.GetAxisRaw("Pause");
        if (pause > 0)
        {
            // toggle visibility:
            if(gameObject.activeSelf == false)
            {
                gameObject.SetActive(true);
            }
            else if (gameObject.activeSelf == true)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
