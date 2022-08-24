using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleJogador : MonoBehaviour
{
    private Rigidbody2D rig;
    public float speed;
    public float jumpForce;
	private bool pulando = false;
	private Animator animator;
	public Transform camera;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();
		//definir a posiçõa inicial da câmera
		camera.position = new Vector3(1.21f,0.0f,-10.0f);
    }
    
    void Update()
    {
        float mov = Input.GetAxisRaw("Horizontal");
		if (mov == 1) {
			GetComponent<SpriteRenderer> ().flipX = false;
		} else if (mov == -1) {
			GetComponent<SpriteRenderer> ().flipX = true;
		}

        rig.velocity = new Vector2(mov * speed, rig.velocity.y);
		animator.SetFloat ("Velocidade", Mathf.Abs (mov));

        if (Input.GetKeyDown(KeyCode.Space)  && pulando == false) {
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			pulando = true;
			animator.SetBool ("Pulando", true);
        }
		float camx = rig.transform.position.x + 3;
		if(camx < 1.21f){
			camx = 1.21f; 
		} 
		if (camx > 35.29f) {
			camx = 35.29f;
		}
		camera.position = new Vector3 (camx, 0.0f, -10.0f);
    }

	void OnCollisionEnter2D(Collision2D coll) {
		pulando = false;
		animator.SetBool ("Pulando", false);
	}


}
