using UnityEngine;
using System.Collections;
using System;

public class MenuElement : MonoBehaviour {

	//public attributes
	public	Transform	CameraLookAt;
	public	Transform	CameraPosition;

	public event	Action	OnMenuAction;
	public event	Action	OnReturn;
	public event	Action	OnSelect;
	public event	Action	OnUnSelect;

	//public properties
	public	bool		Selected { get; private set; }
	public	bool		HasFocus { get; private set; }

	
	//public method
	public	void		Select() {
		this.Selected = true;
		this.OnSelect();
	}

	public	void		UnSelect() {
		this.Selected = false;
		this.OnUnSelect();
	}

	public	void		Focus() {
		this.HasFocus = true;
	}

	public	void		UnFocus() {
		this.HasFocus = false;
	}

	public void			MenuAction() {
		this.HasFocus = true;
		this.OnMenuAction();
	}

	public void			Return() {
		this.OnReturn();
	}

	//private Unity Methods
	private	void		Awake() {
		this.OnMenuAction += empty;
		this.OnReturn += empty;
		this.OnSelect += empty;
		this.OnUnSelect += empty;
	}

	private void		empty() {

	}
}
