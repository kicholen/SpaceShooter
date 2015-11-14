using System.Collections.Generic;
using System;
using UnityEngine;

public class EventService
{
	public delegate void EventDelegate<T>(T e) where T : GameEvent;
	
	Dictionary<Type, Delegate> delegates = new Dictionary<Type, Delegate>();
	
	public void AddListener<T>(EventDelegate<T> listener) where T : GameEvent {
		Type type = typeof(T);
		Delegate d;
		if (delegates.TryGetValue(type, out d)) {
			delegates[type] = Delegate.Combine(d, listener);
		}
		else {
			delegates[type] = listener;
		}
	}
	
	public void RemoveListener<T>(EventDelegate<T> listener) where T : GameEvent {
		Type type = typeof(T);
		Delegate d;
		if (delegates.TryGetValue(type, out d)) {
			Delegate currentDel = Delegate.Remove(d, listener);
			
			if (currentDel == null) {
				delegates.Remove(type);
			}
			else {
				delegates[type] = currentDel;
			}
		}
	}
	
	public void Dispatch<T>(T e) where T : GameEvent {
		if (e == null) {
			Debug.Log("Invalid event argument: " + e.GetType());
			return;
		}
		
		Type type = e.GetType();
		Delegate d;
		if (delegates.TryGetValue(type, out d)) {
			EventDelegate<T> callback = d as EventDelegate<T>;
			if (callback != null) {
				callback(e);
			}
			else {
				Debug.Log("Not removed callback: " + type);
			}
		}
	}
}
public class GameEvent { }