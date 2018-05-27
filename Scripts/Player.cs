using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour {

    public GameObject Bullet, StartBullet;

    private Vector3 Movement;

    public  Rigidbody MyBody; 

    public Text HealthText, AmmoText;

    public float Speed;

    private float GroundDistance;
    Collider Col;

    public int JumpForce;

    public int Health;

    public bool CanAttack = true, Reload = false;
    public const int Magazine = 90;
    public int Ammo = 540, CurrentMagazine = 90;

    public GameObject Cam;
    public GameObject Weapon;
    public float RotSpeedWeapon;

    Ray ray;
    RaycastHit hit;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        HealthText.text = "HP: " + Health.ToString();
        AmmoText.text = "Magazine: " + CurrentMagazine.ToString() + "/ Ammo:" + Ammo.ToString();


        MyBody = GetComponent<Rigidbody>();
        Col = GetComponent<Collider>();

        GroundDistance = Col.bounds.extents.y;
    }
	
	void Update() {

        if (Input.GetMouseButton(0))
            StartCoroutine(Fire());

        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(StartReload());

        ray = new Ray(Cam.transform.position, Cam.transform.forward);

        Physics.Raycast(ray, out hit);

        Vector3 rot;

        if (hit.collider == null)
            rot = Weapon.transform.forward;

        else
            rot = hit.point - Weapon.transform.position;

        Debug.DrawLine(Cam.transform.position, hit.point, Color.red);

        Weapon.transform.rotation = Quaternion.Slerp(Weapon.transform.rotation, Quaternion.LookRotation(rot), RotSpeedWeapon * Time.deltaTime);
    }

    void FixedUpdate() {
        float Right   = Input.GetAxisRaw("Horizontal");
        float Forward = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
            MyBody.AddForce(Vector3.up * JumpForce * 20, ForceMode.Impulse);

        Movement.Set(Forward, 0f, Right);
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

        AmmoText.text = "Magazine: " + CurrentMagazine.ToString() + "/ Ammo:" + Ammo.ToString();

        Reload = false;
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;

        HealthText.text = "HP: " + Health.ToString();

        if (Health <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, GroundDistance + 0.1f);
    }
}
