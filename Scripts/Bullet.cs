using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    public float Force;
    public int Damage;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * Force, ForceMode.Impulse);

        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject != null)
            {
                other.GetComponent<Enemy>().TakeDamage(Damage);
            }
              
        }
    }
}