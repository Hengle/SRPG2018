using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
	// フェードイン中か否かを表すフラグ
	private bool _isFadeIn;

	// フェードアウト中か否かを表すフラグ
	private bool _isFadeOut;

	/// <summary>
	/// フェードが完了するまでの時間 [秒]
	/// </summary>
	private float _fadeSpeedSecond;

	/// <summary>
	/// アルファ値の下限 (初期値)
	/// </summary>
	private float _fadeAlphaLowerLimit;

	/// <summary>
	/// フェードアウト時のアルファ値の上限 (0 ~ 1の間)
	/// </summary>
	private float _fadeAlphaUpperLimit;

	// フェード開始からの
	private float _accumulatedFadeTime;

	/// <summary>
	/// アルファをいじる対象の現在の色
	/// </summary>
	private Color _currentColor;

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	public void Initalize()
	{
		_currentColor = GetComponent<Image>().color;
		_fadeAlphaLowerLimit = GetComponent<Image>().color.a;
	}

	/// <summary>
	/// フェードインを開始するメソッド
	/// </summary>
	/// <param name="time"></param>
	public void StartFadeIn(float time)
	{
		// 4debug
		if(_isFadeOut) Debug.LogError("[Error] : Fade in started while fading out!");
		if(_currentColor.a <= _fadeAlphaLowerLimit) Debug.LogError("[Error] : Not need to fade in!");

		_fadeSpeedSecond = time;
		_isFadeIn = true;
	}

	/// <summary>
	/// フェードアウトを開始するメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <param name="alphaUpperLimit"></param>
	public void StartFadeOut(float time, float alphaUpperLimit)
	{
		// 4debug
		if(_isFadeIn) Debug.LogError("[Error] : Fade out started while fading in!");
		if(alphaUpperLimit < 0 || alphaUpperLimit > 1f) Debug.LogError("[Error] : Fade out alpha upper limit value is over!");
		if(_currentColor.a >= _fadeAlphaUpperLimit) Debug.LogError("[Error] : Not need to fade out!");

		// フェードが完了するまでの時間を更新
		_fadeSpeedSecond = time;

		// アルファ上限を設定
		_fadeAlphaUpperLimit = alphaUpperLimit;

		// フェードアウト開始
		_isFadeOut = true;
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	private void Update()
	{
		// フェードイン中であればアルファを減少させる.
		if(_isFadeIn)
		{
			UpdateFadeInAlpha();
		}

		// フェードアウト中であればアルファを増加させる.
		if(_isFadeOut)
		{
			UpdateFadeOutAlpha();
		}
	}

	/// <summary>
	/// フェードイン中にアルファを更新するメソッド
	/// </summary>
	private void UpdateFadeInAlpha()
	{
		// 1フレームあたりのアルファ減少量を計算し, 更新.
		UpdateAlpha(GetAlphaDifferencePerFrame(), (currentAlpha, difference) => currentAlpha - difference);

		// アルファ値の下限を下回ったらフェードインを終了.
		if(_currentColor.a <= _fadeAlphaLowerLimit)
		{
			_isFadeIn = false;
		}
	}

	/// <summary>
	/// フェードアウト中にアルファを更新するメソッド
	/// </summary>
	private void UpdateFadeOutAlpha()
	{
		// 1フレームあたりのアルファ減少量を計算し, 更新.
		UpdateAlpha(GetAlphaDifferencePerFrame(), (currentAlpha, difference) => currentAlpha + difference);

		// アルファ値の上限を上回ったらフェードアウトを終了.
		if(_currentColor.a >= _fadeAlphaUpperLimit)
		{
			_isFadeOut = false;
		}
	}

	/// <summary>
	/// 1フレームあたりのアルファ差分を計算するメソッド
	/// </summary>
	/// <returns></returns>
	private float GetAlphaDifferencePerFrame()
	{
		// AlphaDistancePreFrame [-/F]
		// = AlphaDistance [-] / (_fadeSpeedSecond [s] * FramePerSecond [F/s])
		// = AlphaDistance [-] * deltaTime [s/F] / _fadeSpeedSecond [s]
		return (_fadeAlphaUpperLimit - _fadeAlphaLowerLimit) * Time.deltaTime / _fadeSpeedSecond;
	}

	/// <summary>
	/// updaterで定義した方法で, colorのalphaを更新するメソッド
	/// </summary>
	/// <param name="alpha"></param>
	/// <param name="updater"></param>
	private void UpdateAlpha(float alpha, Func<float, float, float> updater)
	{
		_currentColor = new Color(_currentColor.r, _currentColor.g, _currentColor.b, updater(_currentColor.a, alpha));
	}
}
