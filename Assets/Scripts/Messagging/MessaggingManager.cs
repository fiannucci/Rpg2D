using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class MessaggingManager : MonoBehaviour
{	
    public static MessaggingManager Instance
    {
        get; private set;
    }

    private List<Action> subscribers = new List<Action>();
    private List<Action<bool>> UiEventSubscribers = new List<Action<bool>>();
    private List<Action<InventoryItem>> inventorySubscribers = new List<Action<InventoryItem>>();

	void Awake ()
    {
        Debug.Log("Messagging Manager started");
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
        DontDestroyOnLoad(gameObject);
	}

    public void SubscribeInventoryEvent(Action<InventoryItem> subscriber)
    {
        if (inventorySubscribers != null)
            inventorySubscribers.Add(subscriber);
    }

    public void UnSubscribeInventoryEvent(Action<InventoryItem> subscriber)
    {
        if (inventorySubscribers != null)
            inventorySubscribers.Remove(subscriber);
    }

    public void ClearAllInventoryEventSubscribers()
    {
        if (inventorySubscribers != null)
            inventorySubscribers.Clear();
    }

    public void BroadcastInventoryEvent(InventoryItem itemInUse)
    {
        foreach(var subscriber in inventorySubscribers)
        {
            subscriber(itemInUse);
        }
    }

    public void SubscribeUIEvent(Action<bool> subscriber)
    {
        UiEventSubscribers.Add(subscriber);
    }

    public void UnSubscriveUIEvent(Action<bool> subscriber)
    {
        UiEventSubscribers.Remove(subscriber);
    }

    public void ClearAllUIEventSubscribers()
    {
        UiEventSubscribers.Clear();
    }

    public void BroadcastUIEvent(bool uiVisible)
    {
        foreach(var subscriber in UiEventSubscribers.ToArray())
        {
            subscriber(uiVisible);
        }
    }
    public void Subscribe(Action subscriber)
    {
        Debug.Log("Subscriber registered");
        subscribers.Add(subscriber);
    }

    public void UnSubscribe(Action subscriber)
    {
        Debug.Log("Subscriber registered");
        subscribers.Remove(subscriber);
    }

    public void ClearAllSubscribers()
    {
        subscribers.Clear();
    }

    public void Broadcast()
    {
        Debug.Log("Broadcast requested, N of Subscribers = " + subscribers.Count);

        foreach(var subscriber in subscribers)
        {
            subscriber();
        }
    }   
}
