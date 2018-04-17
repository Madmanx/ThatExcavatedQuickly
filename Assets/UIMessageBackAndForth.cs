using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMessageBackAndForth : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("ToogleMessage", Random.Range(2, 3));
	}
	
    void ToogleMessage()
    {
        transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
        Invoke("ToogleMessage", Random.Range(2, 3));
    }
}
