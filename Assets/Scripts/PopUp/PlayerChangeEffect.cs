using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// </summary>
public class PlayerChangeEffect : MonoBehaviour
{
	private UI _ui;
	private FadeController _fc;
	private PopUpController _puc;

	[SerializeField]
	private float _fadeTime;
	[SerializeField, Range(0, 1f)]
	private float _fadeOutAlphaLimit;

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	/// <param name="ui"></param>
	public void Initialize(UI ui, PopUpController puc)
	{
		_ui = ui;
		_fc = new FadeController(gameObject.GetComponent<Image>());
		_puc = puc;
	}

	public IEnumerator Act(Unit.Team team)
	{
		yield return _fc.FadeOut(_fadeTime, _fadeOutAlphaLimit);
		_puc.CreateCutInPopUp(team);
		yield return _fc.FadeIn(_fadeTime);
	}
}
