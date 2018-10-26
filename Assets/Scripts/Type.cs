using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;

public class Type : MonoBehaviour
{
	[SerializeField]
	private List<Type> _strong;
	[SerializeField]
	private List<Type> _slightlyStrong;
	[SerializeField]
	private List<Type> _slightlyWeak;
	[SerializeField]
	private List<Type> _weak;

	public void Initilize()
	{
		// Resources/Prefabs/Typesディレクトリ以下のTypeクラスのアタッチされたGameObjectを全て読み込む.
		var typeList = Resources.LoadAll<Type>("Prefabs/Types");

		// === 4debug ===
		Debug.Log("Length = " + typeList.Length);   // 4debug
		foreach (var item in typeList)
		{
			Debug.Log("Asset name = " + item.name); // 4debug
		}
		// === debug end ===


		var Available_Type_List = new List<string>();
		Available_Type_List.AddRange(_strong.Select(x => x.Name));
		Available_Type_List.AddRange(_slightlyStrong.Select(x => x.Name));
		Available_Type_List.AddRange(_slightlyWeak.Select(x => x.Name));
		Available_Type_List.AddRange(_weak.Select(x => x.Name));
		//現在ある属性リストの用意

		bool QuitFlag = false;

		foreach (var x in typeList)
		{
			if (Available_Type_List.Exists(key => key == x.name))
			{
				Available_Type_List.Remove(x.name);
			}
			else
			{
				Debug.LogWarning(x.name + "is not available in " + this.Name);
				QuitFlag = true;
			}
		}
		if (QuitFlag == true)
		{
			Application.Quit();
		}

		if (Available_Type_List.Count != 0)
		{
			Debug.LogWarning(this.Name + "has type which is not compatible");
			Debug.LogWarning(Available_Type_List.ToArray());
			Application.Quit();
		}
	}

	public string Name
	{
		get { return transform.name; }
	}

	public bool IsStrongAgainst(Type type)
	{
		return _strong.Any(x => x.Name == type.Name);
	}

	public bool IsSlightlyStrongAgainst(Type type)
	{
		return _slightlyStrong.Any(x => x.Name == type.Name);
	}

	public bool IsSlightlyWeakAgainst(Type type)
	{
		return _slightlyWeak.Any(x => x.Name == type.Name);
	}

	public bool IsWeakAgainst(Type type)
	{
		return _weak.Any(x => x.Name == x.Name);
	}
}