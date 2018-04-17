using HoloToolkit.Unity.InputModule;
using System.Collections.Generic;
using UnityEngine;

#if false
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Security;
using UnityEngine.UI;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Amqp;
#if !UNITY_WSA
using System.Security.Cryptography.X509Certificates;
#endif
#endif


public class DetectClick : MonoBehaviour, IInputClickHandler // right click error and say implement interface
{
    // Game object prefab to assign to marker of where photo is taken
    //public GameObject markerObject;

    //List<GameObject> list = new List<GameObject>();

    //public CapturePhoto capturePhoto; // set up handler to CapturePhoto instance

    // Try creating object -- new no supported
    //public EventHubsSender eventHubsSender = new EventHubsSender(); // set up handler to send out event



    // Try assigning listener -- seems to only work with buttons
    //GameObject cylinder = GameObject.Find("Cylinder");
    //cyliner.onClick.AddListener(delegate { ContinueDialogue();



    public void OnInputClicked(InputClickedEventData eventData) // used to be OnInputClicked(InputEventData eventData)
    {
        //throw new System.NotImplementedException();
        //Debug.Log("Air Tap!");

        //GameObject marker = GameObject.Instantiate(markerObject);

        // Place marker 1.2 m ahead of present viewpoint
        //marker.transform.position = Camera.main.transform.TransformPoint(0, 0, 1.2f);
        //marker.transform.rotation = Camera.main.transform.Rotate(0, 0, 0);

        // Limit to certain number of objects
        //if (list.Count == 10)
        //{
        //    Destroy(list[0]); // Delete oldest object
        //    list[0] = null;
        //    list.RemoveAt(0); // Remove element in list at index 0
        //}
        //list.Add(marker);

        // Take picture
        //capturePhoto.CallPhotoCapture();

        // Try Adding component
        EventHubsSender eventHubsSender = gameObject.GetComponent<EventHubsSender>();

        // set up handler to send out event
        //GameObject eventHubsSender = GameObject.Instantiate(EventHubsSender);

        eventHubsSender.EhConnectionString = "Endpoint=sb://oseventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YwRx2oOuw4JIx8C3AKq0qRWRa8j6Urj8ax21u93vKxk=";
        eventHubsSender.EhEntityPath = "HoloLensCmds";
        // Send event
        //Debug.Log("Call TestEventHubsSender");
        eventHubsSender.TestEventHubsSender();
    }

    void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);

        // Find the CapturePhoto object and assign to handler
        //capturePhoto = FindObjectOfType(typeof(CapturePhoto)) as CapturePhoto;
    }

    void Update()
    {
    }



}