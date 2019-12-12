using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;


public class SocketClient : MonoBehaviour
{
    Socket sock;
    byte[] receiverBuff;
    string ipAdress = "127.0.0.1";
    string[] parseString;
    public Button recbtn;
    float percent;
    string type;
    string originalType;
    int percentage;
    public Text res;
    public Text perce;

    void Start()
    {
        Button btn = recbtn.GetComponent<Button>();
        btn.onClick.AddListener(onClick);

        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var ep = new IPEndPoint(IPAddress.Parse(ipAdress), 5007);
        sock.Connect(ep);
        receiverBuff = new byte[8192];
    }

    void Update()
    {
        byte[] buff = Encoding.UTF8.GetBytes("0");
        sock.Send(buff, SocketFlags.None);

        int n = sock.Receive(receiverBuff);
        string data = Encoding.UTF8.GetString(receiverBuff, 0, n);

        

        if (data != "0")
        {
            
            if(data != "00")
            {
                parseString = data.Split(',');
                Debug.Log(parseString[0]);
                Debug.Log(parseString[1]);
                calculateResult();
                Debug.Log("result " + percentage + " type : " + type);
                //res.text = "type";
                //perce.text = percentage.ToString();
            }

        }

        //Debug.Log(data);
    }

    public void sendData(string path)
    {
        byte[] buff = Encoding.UTF8.GetBytes(path);
        sock.Send(buff, SocketFlags.None);
    }

    void onClick()
    {
        //recbtn.interactable = false;
        StartCoroutine(sendDelay());
        res.text = "계산중..";
        perce.text = "몇이나올까요~?";
    }

    IEnumerator sendDelay()
    {
        //Debug.Log(Time.time);
        //yield return new WaitForSeconds(4f);
        //Debug.Log(Time.time);
        //RecordAudio();
        // This line will be executed after 10 seconds passed
        yield return new WaitForSeconds(10.5f);
        string path = RecordButton.savepath;
        sendData(path);
        yield return new WaitForSeconds(12.5f);
       // string path = RecordButton.savepath;

        Debug.LogWarning(path);
        
        //sendData(SaveWavFile());       

    }



    void calculateResult()
    {
        percent = float.Parse(parseString[0]);
        type = parseString[1];
        originalType = parseString[1];
        percentage = (int)(percent * 100);
        if (type == "cat")
        {
            if (percentage < 30)
            {
                percentage = Random.Range(50, 70);
            }
            else if (percentage < 60)
            {
                percentage = Random.Range(70, 80);
            }
            else
            {
                percentage = Random.Range(80, 100);
            }
        }
        else
        {
            if (percentage < 10)
            {
                percentage = Random.Range(20, 30);
            }
            else if (percentage < 20)
            {
                percentage = Random.Range(40, 60);
            }
            else
            {
                percentage = Random.Range(60, 90);
            }
        }
        res.text = type;
        perce.text = string.Concat(percentage.ToString(), " % 일치!!");
    }





}