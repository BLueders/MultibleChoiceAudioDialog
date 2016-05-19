using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DialogTimer : Singleton<DialogTimer>
{
	private static Action _onTimerExpireCallback;
	private static float _duration;

	[SerializeField] private GameObject _timerObject;
	[SerializeField] private Text _timerText;

	private bool isActive = false;

	public static void StartTimer(Action onTimerExpire, float duration) {
		_onTimerExpireCallback = onTimerExpire;
		_duration = duration;
		Instance._StartTimer ();
	}

	private void _StartTimer() {
		isActive = true;
		_timerObject.SetActive (true);
	}

	public static void StopTimer() {
		Instance._StopTimer ();
	}

	private void _StopTimer() {
		isActive = false;
		_timerObject.SetActive (false);
		_onTimerExpireCallback = null;
	}

	void Update(){
		if (isActive) {
			_duration -= Time.deltaTime;
			if (_duration < 0) {
				_duration = 0;
				OnTimerExpire ();
			}
			int minutes = ((int)_duration) / 60;
			int seconds = ((int)_duration) % 60;
			_timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
		}
	}

	void OnTimerExpire(){
		_onTimerExpireCallback ();
		StopTimer ();
	}
}

