using UnityEngine;

public class EnemyRespawn : MonoBehaviour {

    public bool Death;
    public float Timer;
    public float Cooldown;
    public GameObject Enemy;
    public string EnemyName;
    GameObject LastEnemy;

    void Start () {
        Death = false;
        this.gameObject.name = EnemyName + "SpawnPoint";
    }
	

	void Update () {
        if (Death == true)
        {
            //If my enemy is death, a timer will start.
            Timer += Time.deltaTime;

        }
        //If the timer is bigger than cooldown.
        if (Timer >= Cooldown)
        {
            //It will create a new Enemy of the same class, at this position.
            Enemy.transform.position = transform.position;

            Instantiate(Enemy);

            LastEnemy = GameObject.Find(Enemy.name + "(Clone)");
            LastEnemy.name = EnemyName;
            //My enemy won't be dead anymore.
            Death = false;
            //Timer will restart.
            Timer = 0;
          }
        }
}
