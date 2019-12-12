﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tranlate : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    public static bool isActive;
    public Button recbtn;

    void Start()
    {
        // Keep a note of the time the movement started.
        Button btn = recbtn.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
        

        startTime = Time.time;
      
        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Move to the target end position.
    void Update()
    {
        // Distance moved equals elapsed time times speed..
        if (isActive)
        {
        
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
        
        }
    }




    void onClick()
    {
        StartCoroutine(ButtonDelay());
    }

    IEnumerator ButtonDelay()
    {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(24f);
        Debug.Log(Time.time);
        startTime = Time.time;
        setActive();
    }


    public static void setActive()
    {
        isActive = true;
    }

    public static void setDefault()
    {

    }
}


