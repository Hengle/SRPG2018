using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
	/// <summary>
	/// アルファをいじる対象の現在の色
	/// </summary>
	private Color _currentColor;

	/// <summary>
	/// アルファをいじる対象の元の色
	/// </summary>
	private Color _defaultColor;

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	public void Initalize()
	{
		_defaultColor = GetComponent<Image>().color;
		_currentColor = _defaultColor;
	}

	/// <summary>
	/// ゲーム開始時のアルファまでフェードインを開始するメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public void StartFadeInDefault(float time)
	{
		StartFadeIn(time, _defaultColor.a);
	}

	/// <summary>
	/// フェードインを開始するメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public void StartFadeIn(float time, float alphaLimit)
	{
		var alphaDistance = _currentColor.a - alphaLimit;

		// フェードアウトできるならば, TouchBlockerを有効にする.
		if(alphaDistance > 0)
		{
			StartCoroutine(FadeCoroutine(FadeIn, GetAlphaDistancePerFrame(alphaDistance, time), alphaLimit));
			gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// ゲーム開始時のアルファまでフェードアウトを開始するメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public void StartFadeOutDefault(float time)
	{
		StartFadeOut(time, _defaultColor.a);
	}

	/// <summary>
	/// フェードアウトを開始するメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public void StartFadeOut(float time, float alphaLimit)
	{
		var alphaDistance = alphaLimit - _currentColor.a;

		// フェードアウトできるならば, TouchBlockerを有効にする.
		if(alphaDistance > 0)
		{
			gameObject.SetActive(true);
			StartCoroutine(FadeCoroutine(FadeOut, GetAlphaDistancePerFrame(alphaDistance, time), alphaLimit));
		}
	}

	private IEnumerator FadeCoroutine(Func<float, float, IEnumerator> fadeFunc, float alphaDistancePerFrame, float time)
	{
		yield return StartCoroutine(fadeFunc(alphaDistancePerFrame, time));
	}

	/// <summary>
	/// フェードインを実行するコルーチンメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <param name="alphaLimit"></param>
	/// <returns></returns>
	private IEnumerator FadeIn(float alphaDistancePerFrame, float alphaLimit)
	{
		while(_currentColor.a > alphaLimit)
		{
			_currentColor -= new Color(0, 0, 0, alphaDistancePerFrame);
			Debug.Log("[Debug] : Fade In Updated! as " + _currentColor.a); // 4debug
			yield return null;
		}
	}

	/// <summary>
	/// フェードアウトを実行するコルーチンメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <param name="alphaLimit"></param>
	/// <returns></returns>
	private IEnumerator FadeOut(float alphaDistancePerFrame, float alphaLimit)
	{
		while(_currentColor.a < alphaLimit)
		{
			_currentColor += new Color(0, 0, 0, alphaDistancePerFrame);
			Debug.Log("[Debug] : Fade Out Updated! as " + _currentColor.a); // 4debug
			yield return null;
		}
	}

	/// <summary>
	/// 1フレームあたりのアルファ差分を計算するメソッド
	/// </summary>
	/// <returns></returns>
	private float GetAlphaDistancePerFrame(float alphaDistance, float time)
	{
		// AlphaDistancePreFrame [-/F]
		// = AlphaDistance [-] / (_fadeSpeedSecond [s] * FramePerSecond [F/s])
		// = AlphaDistance [-] * deltaTime [s/F] / _fadeSpeedSecond [s]
		return alphaDistance * Time.deltaTime / time;
	}
}
