using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NormalPopUp : MonoBehaviour
{
	[SerializeField]
	private float stopTime = 5;
	[SerializeField]
	private float upcomingTime = 3;
	[SerializeField]
	private Vector3 displayPos;
	[SerializeField]
	private Vector3 _initialPos;

	private Text _text;
	private IEnumerator enumerator;

	private void Start()
	{
		_text = gameObject.GetComponentInChildren<Text>();
	}

	public void PopUpNormalInformation(string text)
	{
		_text.text = text;

		if(enumerator != null) StopCoroutine(enumerator);

		enumerator = Move();

		StartCoroutine(enumerator);
	}

	IEnumerator Move()
	{
		float time = 0;
		while(time<upcomingTime)
		{
			transform.localPosition = Vector3.Lerp(_initialPos, displayPos, time / upcomingTime);
			
			yield return null;
			time += Time.deltaTime;
		}

		while(time < stopTime + upcomingTime)
		{
			yield return null;
			time += Time.deltaTime;
		}

		float total = stopTime + upcomingTime * 2;
		while(time < total)
		{
			float rate = (total - time) / upcomingTime;
			transform.localPosition = Vector3.Lerp(_initialPos, displayPos, rate);

			yield return null;
			time += Time.deltaTime;
		}
	}
}
