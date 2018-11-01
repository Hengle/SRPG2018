using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
	[SerializeField]
	private float fadeInSpeedSecond; // fade in speed [sec]
	[SerializeField]
	private float fadeOutSpeedSecond; // fade out speed [sec]
	public bool isFadeIn;
	public bool isFadeOut;

	private float _fadeInTime;
	private float _fadeOutTime;
	private static int _oneFrame = 60;  // 1 [min] = 60 [F]
	private Image _fadeImage;
	private float _red, _green, _blue, _alfa;   //パネルの色、不透明度を管理


	// Use this for initialization
	void Start()
	{
		_fadeInTime = 0;
		_fadeOutTime = 0;
		_fadeImage = GetComponent<Image>();
		_red = _fadeImage.color.r;
		_green = _fadeImage.color.g;
		_blue = _fadeImage.color.b;
		_alfa = _fadeImage.color.a;
		if(isFadeIn && fadeInSpeedSecond > 0)
		{
			_fadeImage.enabled = true;
			StartFadeIn();
		}
		else
		{
			_fadeImage.enabled = false;
			_alfa = 0.0f;
			SetAlpha();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(isFadeIn && fadeInSpeedSecond > 0 && !isFadeOut)
		{
			_fadeInTime = 0;
			StartFadeIn();
		}

		if(isFadeOut && fadeOutSpeedSecond > 0 && !isFadeIn)
		{
			_fadeOutTime = 0;
			StartFadeOut();
		}
	}

	void StartFadeIn()
	{
		_fadeInTime += Time.deltaTime;
		// decrease the alfa value on 1 [F].
		_alfa -= _fadeInTime / fadeInSpeedSecond;
		SetAlpha();
		_fadeInTime = 0;

		if(_alfa <= 0)
		{
			isFadeIn = false;
			_fadeImage.enabled = false;
		}
	}

	void StartFadeOut()
	{
		_fadeOutTime += Time.deltaTime;
		_fadeImage.enabled = true;
		// increase the alfa value on 1 [F].
		_alfa += _fadeOutTime / fadeOutSpeedSecond;
		SetAlpha();
		_fadeOutTime = 0;

		if(_alfa >= 1)
		{
			isFadeOut = false;
		}
	}

	void SetAlpha()
	{
		_fadeImage.color = new Color(_red, _green, _blue, _alfa);
	}
}
