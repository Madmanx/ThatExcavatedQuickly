using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public Cinemachine.CinemachineVirtualCamera[] cameras = new Cinemachine.CinemachineVirtualCamera[4];
    public int currentCamera = -1;

	// Use this for initialization
	void Start () {
        foreach (Cinemachine.CinemachineVirtualCamera camera in cameras)
        {
            if(currentCamera == -1)
            {
                if (camera.gameObject.activeSelf)
                {
                    camera.gameObject.SetActive(true);
                    currentCamera = camera.transform.GetSiblingIndex() - 1;
                }
            }
            else
                camera.gameObject.SetActive(false);
        }
     

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            PreviousCamera();
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextCamera();
        }
    }

    void NextCamera()
    {

        cameras[currentCamera].gameObject.SetActive(false);
        currentCamera++;
        if (currentCamera >= cameras.Length) currentCamera = 0;
        cameras[currentCamera].gameObject.SetActive(true);
    }

    void PreviousCamera()
    {
        cameras[currentCamera].gameObject.SetActive(false);
        currentCamera--;
        if (currentCamera < 0) currentCamera = cameras.Length-1;
        cameras[currentCamera].gameObject.SetActive(true);
    }
}
