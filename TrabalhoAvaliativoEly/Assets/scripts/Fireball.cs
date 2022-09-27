using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
	public float mov = 4F;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float x = transform.position.x + (Time.deltaTime * mov);
		float y = transform.position.y;
		float z = transform.position.z;
		transform.position = new Vector3 (x, y, z);
	}
	void OnCollisionEnter2D(Collision2D col) {
		animator.SetBool("morre", true);
		 Invoke("Desaparecer", 0.30F);
	}
	void Desaparecer() {
		Destroy (gameObject);
	}
}
