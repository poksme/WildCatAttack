using UnityEngine;
using System.Collections;

public class DeathZoneBehaviour : MonoBehaviour {

	public	GameObject	ExplosionPrefab;

	private	bool		playerOneDying = false;
	private	bool		playerTwoDying = false;

	IEnumerator		PlayerDeathCoroutine(WildCatController deadPlayer, Vector3 pos) {
		yield return new WaitForSeconds(1.0f);
		GameObject explosionGO = GameObject.Instantiate(this.ExplosionPrefab, pos, Quaternion.identity) as GameObject;
		yield return new WaitForSeconds(1.0f);
		GameObject.Destroy(explosionGO);
		deadPlayer.Die();
		if (deadPlayer.CompareTag("Player1")) playerOneDying = false;
		if (deadPlayer.CompareTag("Player2")) playerTwoDying = false;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player1") || other.CompareTag("Player2")) {
			if (playerOneDying && other.CompareTag("Player1")) return;
			if (playerTwoDying && other.CompareTag("Player2")) return;
			WildCatController deadPlayer = other.transform.parent.gameObject.GetComponent<WildCatController>();
			if (deadPlayer) {
				Debug.Log("OnTriggerEnter");
				if (other.CompareTag("Player1")) playerOneDying = true;
				if (other.CompareTag("Player2")) playerTwoDying = true;
				StartCoroutine(this.PlayerDeathCoroutine(deadPlayer, other.gameObject.transform.position));
			}
		}
	}
}
