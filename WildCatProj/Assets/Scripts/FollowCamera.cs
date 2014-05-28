using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	[SerializeField] private Transform BoundsPlayer1Left;
	[SerializeField] private Transform BoundsPlayer1Right;
	[SerializeField] private Transform BoundsPlayer2Left;
	[SerializeField] private Transform BoundsPlayer2Right;
	[SerializeField] private float MaxUnzoom = 100f;
	[SerializeField] private float MinZoom = 30f;
	[SerializeField] private float UnzoomStep = 0.5f;
	[SerializeField] private float CenteringSmoothingStep = 1f;
	[SerializeField] private float ZoomingSmoothingStep = 10f;

	[SerializeField] private float minPan = 0f;
	[SerializeField] private float maxPan = 100f;
	[SerializeField] private float panStep = 0.5f;
	[SerializeField] private float PanningSmoothingStep = 10f;

	public enum ZoomingType {
		LensZoom,
		CameraPan
	}

	[SerializeField] private ZoomingType ZoomType = ZoomingType.CameraPan;

	private Vector3 center;
	private Plane[] planes;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		centerCamera();
		if (ZoomType == ZoomingType.CameraPan)
			panCamera();
		else
			zoomCamera();
	}

	private void centerCamera() {
		center = ((BoundsPlayer2Right.position - BoundsPlayer1Left.position)/2.0f) + BoundsPlayer1Left.position;
		center.y += 2f;
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, center.x, Time.deltaTime * CenteringSmoothingStep), 
		                                 Mathf.Lerp(transform.position.y, center.y, Time.deltaTime * CenteringSmoothingStep),
		                                 transform.position.z);
		transform.LookAt(new Vector3(transform.position.x, transform.position.y, center.z));
	}

	private void zoomCamera() {
		float fovBck = this.camera.fieldOfView;
		this.camera.fieldOfView = MinZoom;
		planes =  GeometryUtility.CalculateFrustumPlanes(this.camera);
		while (!seeBothPlayers() && this.camera.fieldOfView < MaxUnzoom) {
			planes =  GeometryUtility.CalculateFrustumPlanes(this.camera);
			this.camera.fieldOfView += UnzoomStep;
		}
		this.camera.fieldOfView = Mathf.Lerp(fovBck, this.camera.fieldOfView, Time.deltaTime * ZoomingSmoothingStep);
	}

	
	private void panCamera() {
		Vector3 panBck = transform.position;
		transform.position = new Vector3(transform.position.x, transform.position.y, minPan);
		planes =  GeometryUtility.CalculateFrustumPlanes(this.camera);
		while (!seeBothPlayers() && transform.position.z > -maxPan) {
			planes =  GeometryUtility.CalculateFrustumPlanes(this.camera);
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - panStep);
		}
		transform.position = Vector3.Lerp(panBck, transform.position, Time.deltaTime * PanningSmoothingStep);
	}

	private bool seeBothPlayers() {
		return GeometryUtility.TestPlanesAABB(planes, BoundsPlayer1Left.collider.bounds) 
			&& GeometryUtility.TestPlanesAABB(planes, BoundsPlayer1Right.collider.bounds)
			&& GeometryUtility.TestPlanesAABB(planes, BoundsPlayer2Left.collider.bounds)
			&& GeometryUtility.TestPlanesAABB(planes, BoundsPlayer2Right.collider.bounds);
	}
}
