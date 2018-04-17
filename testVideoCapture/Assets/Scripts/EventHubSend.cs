using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;

public class EventHubSend : MonoBehaviour {


    private static EventHubClient eventHubClient;
    private const string EhConnectionString = "Endpoint=sb://oseventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YwRx2oOuw4JIx8C3AKq0qRWRa8j6Urj8ax21u93vKxk=";
    private const string EhEntityPath = "HoloLensCmds";


    private static async Task MainAsync()
    {
        // Creates an EventHubsConnectionStringBuilder object from the connection string, and sets the EntityPath.
        // Typically, the connection string should have the entity path in it, but this simple scenario
        // uses the connection string from the namespace.
        var connectionStringBuilder = new EventHubsConnectionStringBuilder(EhConnectionString)
        {
            EntityPath = EhEntityPath
        };

        eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

        await SendMessagesToEventHub(5);

        await eventHubClient.CloseAsync();

        //Console.WriteLine("Press ENTER to exit.");
        //Console.ReadLine();
    }

    // Creates an event hub client and sends 100 messages to the event hub.
    private static async Task SendMessagesToEventHub(int numMessagesToSend)
    {
        for (var i = 0; i < numMessagesToSend; i++)
        {
            try
            {
                var message = $"Message {i}";
                Debug.Log($"Sending message: {message}");
                await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
            }
            catch (Exception exception)
            {
                Debug.Log($"{DateTime.Now} > Exception: {exception.Message}");
            }

            await Task.Delay(10);
        }

        Debug.Log($"{numMessagesToSend} messages sent.");
    }
  

    // Use this for initialization
    void Start () {
        MainAsync().GetAwaiter().GetResult();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
