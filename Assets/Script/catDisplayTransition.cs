using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class catDisplayTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public Button catbutton;
    public Camera[] cameras;
    public Button backButton;
    private int currentCameraIndex;
    
    
    void Start()
    {
        Button btn = catbutton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

       Button backbtn = backButton.GetComponent<Button>();
        backbtn.onClick.AddListener(goBack);

        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        cameras[0].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void TaskOnClick()
    {

        currentCameraIndex = 0; //서영
        cameras[0].gameObject.SetActive(false);
        cameras[1].gameObject.SetActive(true);
    }
    void goBack()
    {
        currentCameraIndex = 1; 
        cameras[1].gameObject.SetActive(false);
        cameras[0].gameObject.SetActive(true);
        
    }

}
