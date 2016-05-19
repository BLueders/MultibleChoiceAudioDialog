using UnityEngine;
using System.Collections.Generic;

public class DialogEventDatabase : Singleton<DialogEventDatabase> {

	public string EventDatabaseFile = "DialogEvents.json";
	public string AudioDataPath = "Audio";

	private Dictionary<string, DialogEvent> _dialogEventMap = new Dictionary<string, DialogEvent>();

	void Awake(){
		LoadEventsFromFile ();
	}

	public static void LoadEventsFromFile(){
		DialogEventData[] eventData = JSONFileParser<DialogEventData[]>.Parse ("Resources/" + Instance.EventDatabaseFile);
		foreach(DialogEventData data in eventData){
			Instance._dialogEventMap.Add(data.ID, CreateEventFromData(data));
		}
	}

	private static DialogEvent CreateEventFromData(DialogEventData data){
		Debug.Log ("Creating Event: " + data.ID);
		DialogEvent newEvent = new DialogEvent ();
		newEvent.Audio = Resources.Load<AudioClip> (Instance.AudioDataPath + "/" + data.AudioFile);
		newEvent.ID = data.ID;
		newEvent.Buttons = data.Buttons;
		newEvent.TimedEvent = data.TimedEvent;
		return newEvent;
	}

	public static DialogEvent GetEventfromID(string id){
		return Instance._dialogEventMap [id];
	}
}
