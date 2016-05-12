using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class DialogSceneManager : Singleton<DialogSceneManager> {

	public GameObject ButtonPrefab;
	public GameObject ButtonParent;

	public AudioSource MainAudioSource;

	void Start(){
		PlayEvent ("Start");
	}

	public static void ResetDialogScene(){
		foreach(Transform child in Instance.ButtonParent.transform) {
			child.GetComponent<Button> ().onClick.RemoveAllListeners ();
			child.gameObject.SetActive (false);
			Destroy(child.gameObject);
		}
		Instance.MainAudioSource.Stop ();
	}

	public static void LoadDialogEvent(DialogEvent dialogEvent){
		Instance._LoadDialogEvent (dialogEvent);
	}

	public void _LoadDialogEvent(DialogEvent dialogEvent){

	}

	public void CreateButtons(List<DialogEventButtonData> data){
		foreach (DialogEventButtonData buttonData in data) {
			GameObject newButton = Instantiate<GameObject>(ButtonPrefab);
			newButton.transform.SetParent(ButtonParent.transform);
			newButton.GetComponentInChildren<Text>().text = buttonData.Text;
			Button button = newButton.GetComponentInChildren<Button>();
			button.onClick.RemoveAllListeners ();
			// the id has to be saved into a local variable, since buttonData is overwritten by the next loop rotation.
			string nextEventID = buttonData.LinkedEventID;
			button.onClick.AddListener( () => Instance.PlayEvent(nextEventID));
		}
	}

	public void PlayEvent(string ID) {
		ResetDialogScene ();
		DialogEvent nextEvent = DialogEventDatabase.GetEventfromID (ID);
		// Wait for clip to finish with 1 sec tolerance
		StartCoroutine(ShowButtonsAfterSeconds(nextEvent.Audio.length - 1, nextEvent.Buttons));
		MainAudioSource.clip = nextEvent.Audio;
		MainAudioSource.Play ();
	}

	bool _skipCurrentClip = false;
	IEnumerator ShowButtonsAfterSeconds(float seconds, List<DialogEventButtonData> data){
		float timer = seconds;
		while (timer > 0 && !_skipCurrentClip) {
			timer -= Time.deltaTime;
			yield return null;
		}
		_skipCurrentClip = false;
		CreateButtons (data);
	}

	public void SkipCurrentClip(){
		// TODO this is kind of a ugly solution to interrupt the waiting coroutine, find a better one?
		_skipCurrentClip = true;
	}
}
