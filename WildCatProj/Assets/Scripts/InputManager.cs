using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public enum Direction {
		None,
		Left,
		Right
	}

	// Key watching variables
	[SerializeField] private KeyCode _leftButtonKey = KeyCode.A;
	private bool _leftButtonPressed = false;

	[SerializeField] private KeyCode _rightButtonKey = KeyCode.D;
	private bool _rightButtonPressed = false;

	// Double tap watching variables
	[SerializeField] private float _doubleTapDelay = 0.2f;
	private float _doubleTapPassed = 0f;
	private Direction _watchedTapDirection = Direction.None;
	private Direction _doubleTapedDirection = Direction.None;


	// Getters
	public bool LeftButtonPressed {
		get { return _leftButtonPressed; } 
	}

	public bool RightButtonPressed {
		get { return _rightButtonPressed; } 
	}

	public Direction DoubleTapDirection {
		get { return _doubleTapedDirection; }
	}
	
	// Use this for initialization
	void Start () {
		InitializeValues();
	}
	
	// Update is called once per frame
	void Update () {
		InitializeValues();
		UpdateDoubleTapTimer();
		UpdatePressedKeys();
		UpdateDoubleTap();
	}

	// Values re-initialized at each update
	private void InitializeValues() {
		_leftButtonPressed = false;
		_rightButtonPressed = false;
		_doubleTapedDirection = Direction.None;
	}

	private void UpdateDoubleTapTimer() {
		if (_watchedTapDirection != Direction.None) {
			// If there is a watched direction
			_doubleTapPassed += Time.deltaTime; // Increment timer
			if (_doubleTapPassed > _doubleTapDelay) {
				// If the time is expired
				ResetTimer();
			}
		} else {
			ResetTimer();
		}
	}

	private void UpdatePressedKeys() {
		_leftButtonPressed = Input.GetKeyDown(_leftButtonKey);
		_rightButtonPressed = Input.GetKeyDown(_rightButtonKey);
	}

	private void UpdateDoubleTap() {
		if ((_leftButtonPressed || _rightButtonPressed)
		    && !(_leftButtonPressed && _rightButtonPressed)) { 
			// If only one direction is pressed
			if (_watchedTapDirection == Direction.None) { 
				// If no direction is watched
				_watchedTapDirection = _leftButtonPressed ? Direction.Left : Direction.Right; // Watch this direction
			} else { 
				// If a direction is already watched
				if ((_watchedTapDirection == Direction.Left && _leftButtonPressed)
				    || (_watchedTapDirection == Direction.Right && _rightButtonPressed)) { 
					// If it's the same direction as the watched one
					if (_doubleTapPassed <= _doubleTapDelay) {
						// If the second tap is in time limit
						_doubleTapedDirection = _leftButtonPressed ? Direction.Left : Direction.Right; // Set the doubleTapedDirection
					}
				} else {
					// If it's a different direction as the watched one
					ResetTimer();
					_watchedTapDirection = _leftButtonPressed ? Direction.Left : Direction.Right; // Watch this direction
				}
			}
		} else if (_leftButtonPressed && _rightButtonPressed) { 
			// Both direction are pressed at the same time
			ResetTimer();
		}
	}

	// Resets the timer and unwatch the tap direction	
	private void ResetTimer() {
		_watchedTapDirection = Direction.None;
		_doubleTapPassed = 0f;
	}
}
