using UnityEngine;
using System.Collections;
using System;

public class MenuElement : MonoBehaviour {

	//public attributes
	public	Transform	CameraLookAt;
	public	Transform	CameraPosition;

	public event	Action	OnMenuAction;
	public event	Action	OnSelect;
	public event	Action	OnUnSelect;

	//public properties
	public	bool		Selected { get; private set; }


	//public method
	public	void		Select() {
		this.Selected = true;
		this.OnSelect();
	}

	public	void		UnSelect() {
		this.Selected = false;
		this.OnUnSelect();
	}

	public void			MenuAction() {
		this.MenuAction();
	}

	//private Unity Methods
	private	void		Awake() {
		this.OnMenuAction += empty;
		this.OnSelect += empty;
		this.OnUnSelect += empty;
	}

	private void		empty() {

	}
}
