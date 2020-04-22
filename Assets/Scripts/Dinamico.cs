using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using UnityEngine.Windows.Speech;
using System;
using Vuforia;


public class Dinamico : MonoBehaviour, ITrackableEventHandler
{
    /*parte voz*/
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, int> actions = new Dictionary<string, int>();
    /*termina*/

    private TrackableBehaviour mTrackableBehaviour;

    public GameObject[] userPowers;
    private int actualPowerIndex = 0;



    // Use this for initialization
    void Start()
    {

        actions.Add("fuego", 0);
        actions.Add("agua", 1);
        actions.Add("tierra", 2);
        actions.Add("viento", 3);
        userPowers[actualPowerIndex].active = true;
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();


        mTrackableBehaviour = GetComponent<TrackableBehaviour>();

        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            print("space key was pressed");
            palabra = "Prefabs/agua";

        }*/

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        userPowers[actualPowerIndex].active = false;
        actualPowerIndex = actions[speech.text];
        userPowers[actualPowerIndex].active = true;
    }




    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
           
            
                OnTrackingFound();
            
        }
        else
        {
            OnTrackingLost();
        }
    }

    private void OnTrackingLost()
    {

    }

    private void OnTrackingFound()
    {

        Debug.Log("Image Target Found!");
        
    }

}