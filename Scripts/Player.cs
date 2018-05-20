using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour {

    public GameObject Bullet, StartBullet;

    public  Rigidbody MyBody; 

    private Vector3   Movement;

    public Text HealthText, AmmoText;

    private bool Jump;

    public float Speed;

    public int Health, JumpForce;

    public bool CanAttack = true, Reload = false;
    public const int Magazine = 90;
    public int Ammo = 540, CurrentMagazine = 90;

    void Start() {
        MyBody = GetComponent<Rigidbody>();
        SetHealthText();
        AmmoText.text = "Magazine: " + CurrentMagazine.ToString() + "/ Ammo:" + Ammo.ToString();
    }
	
	void Update() {

        if (Input.GetMouseButton(0))
            StartCoroutine(Fire());

        if (Input.GetKeyUp(KeyCode.Space))
            Jump = true;

        if (Input.GetKeyUp(KeyCode.R))
            StartCoroutine(StartReload());
	}
 
    void FixedUpdate() {
        float Right   = Input.GetAxisRaw("Horizontal");
        float Forward = Input.GetAxisRaw("Vertical");


        if (Jump)
        {
            Jump = false;
            MyBody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
        }

        Movement.Set(Forward, 0f, Right);
        AmmoText.text = "Magazine: " + CurrentMagazine.ToString() + "/ Ammo:" + Ammo.ToString();
        MyBody.AddForce(transform.forward * Forward * Speed, ForceMode.Impulse);
        MyBody.AddForce(transform.right   * Right   * Speed, ForceMode.Impulse);
    }

    IEnumerator Fire()
    {   if (CanAttack && CurrentMagazine > 0 && !Reload)
        {
            CanAttack = false;
            CurrentMagazine--;

            AmmoText.text = "Magazine: " + CurrentMagazine.ToString() + "/ Ammo:" + Ammo.ToString();

            Instantiate(Bullet, StartBullet.transform.position, StartBullet.transform.rotation);

            if (CurrentMagazine <= 0)
            {
                StartCoroutine(StartReload());
                Reload = true;
            }
            CanAttack = true;
            yield return new WaitForSeconds(1f);
        }
    }
    
    IEnumerator StartReload()
    {
        yield return new WaitForSeconds(1f);

        if (Ammo > Magazine)
        {
            int Num = Magazine;
            Num = Num - CurrentMagazine;
            Ammo -= Num;
            CurrentMagazine = Magazine;
        }
         else
        {
            CurrentMagazine = Ammo;
            Ammo = 0;
        }

        Reload = false;
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        SetHealthText();
        if (Health <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void SetHealthText()
    {
        HealthText.text = "HP: " + Health.ToString();
    }
}
