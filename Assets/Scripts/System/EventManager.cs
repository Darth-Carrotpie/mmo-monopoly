using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[ExecuteInEditMode]
public class EventManager : MonoBehaviour
{
    public bool enableDebugging;

    private Dictionary<string, UnityGameEvent> eventDictionary;
    private Dictionary<string, UnityGameEvent> attachmentsDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityGameEvent>();
        }
        if (attachmentsDictionary == null)
        {
            attachmentsDictionary = new Dictionary<string, UnityGameEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction<GameMessage> listener)
    {
        UnityGameEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityGameEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }
    public static void Attach(string eventName, UnityAction<GameMessage> eventToAttach)
    {
        //use this to attach events to other events, this way making ordered event chains
        //??? or use stateMachines???
        UnityGameEvent thisEvent = null;
        if (instance.attachmentsDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(eventToAttach);
        }
        else
        {
            thisEvent = new UnityGameEvent();
            thisEvent.AddListener(eventToAttach);
            instance.attachmentsDictionary.Add(eventName, thisEvent);
        }
    }
    public static void StopListening(string eventName, UnityAction<GameMessage> listener)
    {
        if (eventManager == null) return;
        UnityGameEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void Detach(string eventName, UnityAction<GameMessage> listener)
    {
        if (eventManager == null) return;
        UnityGameEvent thisEvent = null;
        if (instance.attachmentsDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void TriggerEvent(string eventName, GameMessage message)
    {
        if (instance.enableDebugging == true)
            Debug.LogWarning(eventName + ": " + message);
        UnityGameEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(message);
        }
        if (instance.attachmentsDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(message);
        }

    }
}