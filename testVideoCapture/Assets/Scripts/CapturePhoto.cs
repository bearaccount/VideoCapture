using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.WSA.WebCam;

public class CapturePhoto : MonoBehaviour
{

    PhotoCapture photoCaptureObject = null;

    void Start()
    {
        CallPhotoCapture();
        //PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void CallPhotoCapture()
    {
        PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
    }

    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCaptureObject = captureObject;

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        CameraParameters c = new CameraParameters();
        c.hologramOpacity = 0.0f;
        c.cameraResolutionWidth = cameraResolution.width;
        c.cameraResolutionHeight = cameraResolution.height;
        c.pixelFormat = CapturePixelFormat.BGRA32;

        captureObject.StartPhotoModeAsync(c, OnPhotoModeStarted);
    }

    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            string filename = string.Format(@"CapturedImage{0}_n.jpg", Time.time);
            string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);

            photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);
        }
        else
        {
            Debug.LogError("Unable to start photo mode!");
        }
    }

    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            Debug.Log("Saved Photo to disk!");
            photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
        }
        else
        {
            Debug.Log("Failed to save Photo to disk");
        }
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }
}


#if false
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class CubeManager : MonoBehaviour, IInputClickHandler, IInputHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        // AirTap code goes here
    }
    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }
}
#endif