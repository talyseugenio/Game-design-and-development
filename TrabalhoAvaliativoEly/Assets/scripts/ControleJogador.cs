using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControleJogador : MonoBehaviour {
    private Rigidbody2D rig;
    public float speed;
    public float jumpForce;
	private bool pulando = false;
	private Animator animator;
	public Transform camera;

	private AudioSource somPulo;
	private AudioSource somMoeda;
	
	public GameObject fireball;

	private int pontos = 0;
	public TextMeshProUGUI  txtMoeda;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();
		//definir a posição inicial da câmera
		camera.position =  new Vector3(1.21f, 0.0f, -10.0f);
		//instancia o som
		somPulo = GetComponents<AudioSource>()[1];
		somMoeda = GetComponents<AudioSource>()[2];
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

        if (Input.GetKeyDown(KeyCode.Space) && pulando == false) {
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			pulando = true;
			animator.SetBool ("Pulando", true);
			somPulo.Play();
		}

		//fireball
		if (Input.GetKeyDown(KeyCode.Tab)) {
            float fx;
            float movFire;
            bool flipFire;
            if (GetComponent<SpriteRenderer>().flipX) {
				//define a posição e direção da bola para esquerda
                movFire = -3F;
                fx = rig.transform.position.x - 1.5F; 
                flipFire = false;
            } else {
				//define a posição e direção da bola para direita
                movFire = 3F;
                fx = rig.transform.position.x + 1.5F; 
                flipFire = true;
            }    
			//define a altura da bola de acordo com a altura do gato
            float fy = rig.transform.position.y-0.3F;
            float fz = rig.transform.position.z;
			//instancia um novo objeto Fireball
            GameObject novo = Instantiate(fireball, new Vector3(fx, fy, fz), Quaternion.identity);
            novo.GetComponent<Fireball>().mov = movFire;
            novo.GetComponent<SpriteRenderer>().flipX = flipFire;            
        }



		float camx = rig.transform.position.x + 3;
		if (camx < -4.7) {
			camx = -4.7f;
		}
		if (camx > 37.29) {
			camx = 37.29f;
		}
		camera.position = new Vector3 (camx, 0.0f, -10.0f);
    }

	void OnCollisionEnter2D(Collision2D coll) {
		pulando = false;
		animator.SetBool ("Pulando", false);
	}

	void OnTriggerEnter2D(Collider2D coll) {

		if (coll.gameObject.tag == "Finish") {
			//muda de fase
			 SceneManager.LoadScene("Fase2", LoadSceneMode.Single);
		}

		somMoeda.Play ();
		Destroy (coll.gameObject);
		pontos++;
		//exibir os pontos na HUD
		txtMoeda.text = ""+pontos;
	}
}
