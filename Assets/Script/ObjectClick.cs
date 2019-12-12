using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClick : MonoBehaviour
{
    // Start is called before the first frame update

    Animator anima;

    void Start()
    {
        
       // ObjectAudio = this.GetComponent<AudioSource>();




    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("ssssdown");
            anima.SetTrigger("CatPlay");
         

        }


    }
}
