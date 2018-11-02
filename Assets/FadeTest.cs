using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTest : MonoBehaviour
{
	[SerializeField]
	private GameObject _fadePanel;

	private float _time;
	private float _fadeTime = 4f;

	private void Start()
	{
		StartCoroutine(_fadePanel.GetComponent<FadeController>().FadeOut(3f, 1f));
		Debug.Log("finished Fade Out");
	}

	//Aキーを押されたらフェード開始
	private void Update()
	{
		/*
		if(Input.GetKeyDown(KeyCode.A))
		{
			StartCoroutine(FadePanel());
			Debug.Log("hogehoge");
		}
		*/
	}

	//フェードアウト自体は↓の処理
	private IEnumerator FadePanel()
	{
		_time = 0;
		var gadpf = GetAlphaDistancePerFrame(1f, 4f);
		while(_time < _fadeTime)
		{
			_fadePanel.GetComponent<Image>().color += new Color(0, 0, 0, gadpf);
			_time += Time.deltaTime;
			Debug.Log("Update alpha! And accumlated delta = " + _time);	// 4debug
			yield return null;
		}
	}

	private float GetAlphaDistancePerFrame(float alphaDistance, float time)
	{
		// AlphaDistancePreFrame [-/F]
		// = AlphaDistance [-] / (_fadeSpeedSecond [s] * FramePerSecond [F/s])
		// = AlphaDistance [-] * deltaTime [s/F] / _fadeSpeedSecond [s]
		return alphaDistance * Time.deltaTime / time;
	}

	private IEnumerator CoroutineA()
	{
		// wait for 1sec
		Debug.Log("CoroutineA created.");
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine(CoroutineB());
		Debug.Log("CoroutineA running again.");
	}

	private IEnumerator CoroutineB()
	{
		Debug.Log("CoroutineB created.");
		yield return new WaitForSeconds(2.5f);
		Debug.Log("CoroutineB enables CoroutineA to run.");
	}
}
