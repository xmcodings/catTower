using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCatClick : MonoBehaviour
{
    // Start is called before the first frame update

    Animator anima;
    //Animation ObjectAnimation;
    //public AudioClip ObjectAudio;

    void Start()
    {

        anima = this.GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {


            anima.SetTrigger("Active");
            Debug.Log("ssssdown");

        }

    }
}
