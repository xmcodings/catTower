using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class RecordButton : MonoBehaviour
{


    public Camera[] cameras;
    private int currentCameraIndex;

    float timeLeft = 5.0f;
    //public Text text;
    bool isRecord;
    public Button recBtn;

    private bool micConnected = false;
    //The maximum and minimum available recording frequencies
    private int minFreq;
    private int maxFreq;
    public Text displayText;

    //A handle to the attached AudioSource
    private AudioSource goAudioSource;

    //public AudioSource background;
    public AudioSource randCatSound;
    AudioSource backMusic;


    AudioClip audio;
    public int recordTime = 2;
    private const int sampleRate = 16000;

    public static string savepath;


    void Start()
    {
        Button btn = recBtn.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        isRecord = false;
        //background.mute = false;
        currentCameraIndex = 0;
        backMusic = GetComponent<AudioSource>();
        //Turn all cameras off, except the first default one
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        //If any cameras were added to the controller, enable the first one
        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
            Debug.Log("Camera with name: " + cameras[0].GetComponent<Camera>().name + ", is now enabled");
        }

        ////////////////////////////////////////////////////////mic
        if (Microphone.devices.Length <= 0)
        {
            //Throw a warning message at the console if there isn't
            Debug.LogWarning("Microphone not connected!");
        }
        else //At least one microphone is present
        {
            //Set 'micConnected' to true
            micConnected = true;

            //Get the default microphone recording capabilities
            Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

            //According to the documentation, if minFreq and maxFreq are zero, the microphone supports any frequency...
            if (minFreq == 0 && maxFreq == 0)
            {
                //...meaning 44100 Hz can be used as the recording sampling rate
                maxFreq = 44100;
            }

            //Get the attached AudioSource component
            goAudioSource = this.GetComponent<AudioSource>();
        }

    }
    void Update()
    {
        //Debug.Log("update");
        //text.text = Mathf.Round(timeLeft).ToString();
        if (isRecord)
        {
            timeLeft -= Time.deltaTime;
            //Debug.Log(timeLeft);
            //text.text = Mathf.Round(timeLeft).ToString();

            // 따라해 보세요~
            DisplayText();
            

            if (timeLeft < 0) // 서영이 고양이 record
            {
                //text.text = "record " + (-timeLeft) + "s";
          //      currentCameraIndex = 1;
         //       cameras[currentCameraIndex - 1].gameObject.SetActive(false);
         //       cameras[currentCameraIndex].gameObject.SetActive(true);
                
            }
            if (timeLeft < -5) // 지원이 고양이 slot
            {
                //text.text = "stop, analyzing...";
                //isRecord = false;
         //       currentCameraIndex = 2;
         //       cameras[currentCameraIndex - 1].gameObject.SetActive(false);
        //        cameras[currentCameraIndex].gameObject.SetActive(true);
            }

            if(timeLeft < -12) // 메인 화면 민지 playground
            {
        //        currentCameraIndex = 3;
               // Debug.Log("C button has been pressed. Switching to the next camera");
        //       cameras[currentCameraIndex - 1].gameObject.SetActive(false);
         //       cameras[currentCameraIndex].gameObject.SetActive(true);
                // Debug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
       //         isRecord = false;
       //         timeLeft = 5.0f;
            }
        }
        else
        {
            //text.text = Mathf.Round(timeLeft).ToString();
        }
    }
    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        StopBackMusic();


        PlayCatSound();
       
        if (micConnected)
        {
            isRecord = true;
            recBtn.interactable = false;
            StartCoroutine(ButtonDelay());
        }
        else // No microphone
        {
            //text.text = "no mic connected";

        }
        
    }
    IEnumerator ButtonDelay()
    {
        // 잘 들으세요 타임 따라해보세요
        yield return new WaitForSeconds(3f);
        Debug.Log(Time.time); // 녹음!!
        currentCameraIndex = 1; //서영
        cameras[currentCameraIndex - 1].gameObject.SetActive(false);
        cameras[currentCameraIndex].gameObject.SetActive(true);
        RecordAudio();
        yield return new WaitForSeconds(5f); // 5초 녹음      

        Debug.Log(Time.time); // 룰렛
        currentCameraIndex = 2;//지원
        cameras[currentCameraIndex - 1].gameObject.SetActive(false);
        cameras[currentCameraIndex].gameObject.SetActive(true);
        savepath = SaveWavFile();
        // This line will be executed after 3 seconds passed
        yield return new WaitForSeconds(14f); //5초간 룰렛
        //StopBackMusic(); // 배경음ㅁ악멈춰!!!
        //savepath = SaveWavFile();
        Debug.LogWarning(savepath);
        recBtn.interactable = true;
        //text.text = Mathf.Round(timeLeft).ToString();
        
        // 캣 플레이그라운드
        currentCameraIndex = 3; //민지
        // Debug.Log("C button has been pressed. Switching to the next camera");
        cameras[currentCameraIndex - 1].gameObject.SetActive(false);
        cameras[currentCameraIndex].gameObject.SetActive(true);
        // Debug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
        
        isRecord = false;
        timeLeft = 5.0f;
    }

    public void RecordAudio()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.LogWarning("No microphone found to record audio clip sample with.");
            return;
        }
        string mic = Microphone.devices[0];
        Debug.LogWarning("Microphone Recording in progress~~~~~~~~~~~~~~~");
        audio = Microphone.Start(mic, false, recordTime, sampleRate);
    }

    public string SaveWavFile()
    {
        string filepath = "sssss";
        Debug.LogWarning("Saving in progress~~~~~~~~~~~~~~~");
        byte[] bytes = WavUtility.FromAudioClip(audio, out filepath, true);
        return filepath;
    }

    public void DisplayText()
    {
        displayText.text = "잘 들으세요";
    }

    public void StopBackMusic()
    {
        if (backMusic.mute)
        { 
            backMusic.mute = false;
        }
        else
        {
            backMusic.mute = true;
        }
    }


    public void PlayCatSound()
    {
        randCatSound.Play();
    }





}
