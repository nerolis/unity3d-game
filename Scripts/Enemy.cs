using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

    public Transform Target;

    public float Speed, MaxDistance, SpeedRotation, MinDistance, AttackDistance, CooldownTime;

    public bool Cooldown = false;

    public int Health;

    public int Damage;

    private int Deaths;

    Rigidbody MyBody;
    Transform MyTransform;
    
	void Start () {
        MyBody = GetComponent<Rigidbody>();
        MyTransform = transform;

        Target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void FixedUpdate ()
    {
           if (Vector3.Distance(MyTransform.position, Target.position) < MaxDistance) {
            RotateToPlayer();

            if (Vector3.Distance(MyTransform.position, Target.position) > MinDistance)
                MoveToPlayer();

            if (Vector3.Distance(MyTransform.position, Target.position) < AttackDistance)
                AttackPlayer();
        }


	}

    private void MoveToPlayer()
    {
        MyTransform.position += MyTransform.forward * Speed * Time.deltaTime;
    }

    private void RotateToPlayer()
    {
        Vector3 rotation = Target.position - MyTransform.position;
        MyTransform.rotation = Quaternion.Slerp(MyTransform.rotation, Quaternion.LookRotation(rotation), SpeedRotation * Time.deltaTime);
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(CooldownTime);

        Target.GetComponent<Player>().TakeDamage(Damage);

        Cooldown = false;

    }

    private void AttackPlayer()
    {
        if(!Cooldown)
        {
            Cooldown = true;
            StartCoroutine(Attack());
        }
             

    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage; 

        if (Health <= 0)
        {
      
            GameObject.Find(gameObject.name + ("SpawnPoint")).GetComponent<EnemyRespawn>().Death = true;
            Destroy(gameObject);
        }
    }
}
