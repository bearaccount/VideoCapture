using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Security;
using UnityEngine.UI;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Amqp;
#if !UNITY_WSA || UNITY_EDITOR
//#if WINDOWS_UWP
using System.Security.Cryptography.X509Certificates;
#endif

public class EventHubsSender : BaseEventHubs
{

    //private static EventHubClient eventHubClient;
    public EventHubClient eventHubClient;

    [HideInInspector]
    public Text DebugText;

    //public EventHubsConnectionStringBuilder eventHubsConnectionStringBuilder;

#if !UNITY_WSA || UNITY_EDITOR
//#if WINDOWS_UWP
    private class CustomCertificatePolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint sp,
            X509Certificate certificate, WebRequest request, int error)
        {
            return true;
        }
    }
#endif

    // Use this for initialization
    public async void TestEventHubsSender()
    {
        Debug.Log("In TestEventHubsSender");

        try
        {
#if !UNITY_WSA || UNITY_EDITOR
//#if WINDOWS_UWP
            //Unity will complain that the following statement is deprecated
            //however, it's working :)
            ServicePointManager.CertificatePolicy = new CustomCertificatePolicy();
            
            //this 'workaround' seems to work for the .NET Storage SDK, but not here. Leaving this for clarity
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
#endif
            //EventHubsConnectionStringBuilder eventHubsConnectionStringBuilder = GameObject.Instantiate<EventHubsConnectionStringBuilder>(EventHubsConnectionStringBuilder);

            // no error
#if true
            //var connectionStringBuilder = new EventHubsConnectionStringBuilder(EhConnectionString)
            EventHubsConnectionStringBuilder connectionStringBuilder = new EventHubsConnectionStringBuilder(EhConnectionString)
            {
                EntityPath = EhEntityPath
            };
#endif

            Debug.Log($"Endpoint> {connectionStringBuilder.Endpoint.ToString()}");
            Debug.Log($"EntityPath> {connectionStringBuilder.EntityPath.ToString()}");


            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            //EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            Debug.Log($"EventHubName: {eventHubClient.EventHubName.ToString()}");


#if true // error
            await SendMessagesToEventHub(1);
#else
            await TempWait(10);
#endif
        }
        catch (Exception ex)
        {
            Debug.Log($"ErrorNew: {ex} in EventHub!");
            //WriteLine(ex.Message);
        }
        finally
        {
            // <<TEMP CHANGE>> put back when I can create an eventHubClient
            await eventHubClient.CloseAsync();
        }

    }



    private async Task SendMessagesToEventHub(int numMessagesToSend)
    {
        Debug.Log("In SendMessagesToEventHub");

        for (var i = 0; i < numMessagesToSend; i++)
        {
            try
            {
                //var message = $"CLICK {i} at {DateTime.Now}";
                string message = $"CLICK {i} at {DateTime.Now}";
                //WriteLine($"Sending message: {message}");
                Debug.Log($"Sending message: {message}");

                // Test
                EventData eventData = new EventData(Encoding.UTF8.GetBytes(message));
                Debug.Log($"eventData> {eventData.ToString()}");

                // <<ERROR>> - got error just trying to access eventHubClient
                Debug.Log($"EventHubName: {eventHubClient.EventHubName.ToString()}");

                // Choose one:
                //await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
                await eventHubClient.SendAsync(eventData);
            }
            catch (Exception exception)
            {
                //WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
                Debug.Log($"{DateTime.Now} > Exception: {exception.Message}");
                //something happened so exit the loop
                break;
            }

            await Task.Delay(10);
        }

        //WriteLine($"{numMessagesToSend} messages sent.");
    }

    private async Task TempWait(int time_ms)
    {
        //Debug.Log("In TempWait");


        await Task.Delay(time_ms);

    }
}
