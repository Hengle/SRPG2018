﻿using UnityEngine;
using System.Collections;

/// <summary>
/// PopUpの実体を作成するクラスです。
/// --------------------------------
/// 同じオブジェクトにアタッチしていると想定しているもの
/// - Image(背景画像)
/// - DamagePopUp.cs
/// - CutInPopUp.cs
///
/// 子オブジェクトにアタッチしていると想定しているもの
/// - Text (名称:Text)
///
/// これさえ守れば、PopUpFactory(現在これを実現しているprefab)は
/// Hierarchy上のどこでも動きます。
/// </summary>
public class PopUpController : MonoBehaviour
{
	private UI _ui;

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	/// <param name="ui"></param>
	public void Initialize(UI ui)
	{
		_ui = ui;
	}

	/// <summary>
	/// ダメージのポップアップを作ります
	/// </summary>
	/// <param name="defender">ダメージを受けたユニット</param>
	/// <param name="damage">ダメージ量</param>
	public void CreateDamagePopUp(Transform defender, int damage)
	{
		var popUp = Instantiate(gameObject, defender);

		string text = damage.ToString();

		popUp.GetComponent<DamagePopUp>().Initialize(text);
	}

	/// <summary>
	/// カットインのポップアップを作ります
	/// </summary>
	/// <param name="team"></param>
	public void CreateCutInPopUp(Unit.Team team)
	{
		string text = "=== " + team.ToString() + " Order ===";

		_ui.TouchBlocker.GetComponent<CutInPopUp>().Initialize(text);
	}
}
