using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControleNPC : MonoBehaviour {

	private Rigidbody2D rig;
	private float mov = 1f;
    private AudioSource somMortePlayer;
    private AudioSource somMorteNPC;

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody2D> ();
        somMortePlayer = GetComponents<AudioSource>()[0];
        somMorteNPC = GetComponents<AudioSource>()[1];
	}

	// Update is called once per frame
	void Update () {
		if (mov > 0) {
			GetComponent<SpriteRenderer> ().flipX = false;
		} else {
			GetComponent<SpriteRenderer> ().flipX = true;
		}
		rig.velocity = new Vector2 (mov, rig.velocity.y);
	}

	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Player") {
			if (col.gameObject.transform.position.y > 
				gameObject.transform.position.y + 1) {
				Destroy (gameObject); //NPC morre
                somMorteNPC.Play();
			} else {
				Destroy (col.gameObject); //Npc Mata
                somMortePlayer.Play();

			}
		} else if (col.gameObject.tag == "Fire") {
			Destroy (gameObject); //Npc morre
            somMorteNPC.Play();
		} else {
			mov = mov * -1;
		}
	}
}
