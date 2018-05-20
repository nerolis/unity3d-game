using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour {

    public GameObject Bullet, StartBullet;

    public  Rigidbody MyBody; 

    private Vector3   Movement;


    public float Speed;

    public int Health;

    void Start() {
        MyBody = GetComponent<Rigidbody>();
	}
	
	void Update() {
        if (Input.GetMouseButtonDown(0))
            Fire();
	}
 
    void FixedUpdate() {
        float Right   = Input.GetAxisRaw("Horizontal");
        float Forward = Input.GetAxisRaw("Vertical");

        Movement.Set(Forward, 0f, Right);

        MyBody.AddForce(transform.forward * Forward * Speed, ForceMode.Impulse);
        MyBody.AddForce(transform.right   * Right   * Speed, ForceMode.Impulse);
    }

    void Fire()
    {
        Instantiate(Bullet, StartBullet.transform.position, StartBullet.transform.rotation);
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;

        if (Health <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
