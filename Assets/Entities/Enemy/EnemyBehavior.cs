using UnityEngine;
using System.Collections;
using System;

public class EnemyBehavior : MonoBehaviour {

    public GameObject Projectile;
    public AudioClip FireSound;
    public AudioClip DeathSound;
    public float ProjectileSpeed = 10f,
                 Health = 150f,
                 ShotsPerSecond = 0.5f;
    public int ScoreValue = 125;
    
    private ScoreKeeper _scoreKeeper;

    private void Start(){
        _scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void Update() {
        var probability = Time.deltaTime * ShotsPerSecond;
        if (UnityEngine.Random.value < probability) {
            Fire();
        }
    }

    private void Fire() {
        var beam = Instantiate(Projectile, transform.position, Quaternion.identity) as GameObject;
        if (beam != null) { beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -ProjectileSpeed, 0); }
        AudioSource.PlayClipAtPoint(FireSound, transform.position);
    }

    private void OnTriggerEnter2D( Collider2D collider ) {
        var missile = collider.gameObject.GetComponent<Projectile>();
        if (missile) {
            Health -= missile.GetDamage();
            missile.Hit();
            if (Health <= 0) {
                Die();
            }
        }
    }

    private void Die() {
        AudioSource.PlayClipAtPoint(DeathSound, transform.position);
        _scoreKeeper.AddScore(ScoreValue);
        Destroy( gameObject );    
    }
}
