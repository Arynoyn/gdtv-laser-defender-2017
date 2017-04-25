#region Using Directives

using System;
using UnityEngine;

#endregion

public class PlayerController : MonoBehaviour {

    public GameObject Projectile;
    public AudioClip FireSound;
    public float Speed = 15.0f,
                 Padding = 1,
                 ProjectileSpeed = 10,
                 FireRate = 0.2f,
                 Health = 250f;
                 
    private float _xmin,
                  _xmax;

    

    // Use this for initialization
    private void Start() {
        var distance = transform.position.z - Camera.main.transform.position.z;
        var leftMost = Camera.main.ViewportToWorldPoint( new Vector3( 0, 0, distance ) );
        var rightMost = Camera.main.ViewportToWorldPoint( new Vector3( 1, 0, distance ) );
        _xmin = leftMost.x + Padding;
        _xmax = rightMost.x - Padding;
    }

    private void Fire() {
        var beam = Instantiate( Projectile, transform.position, Quaternion.identity ) as GameObject;
        if (beam != null) { beam.GetComponent<Rigidbody2D>().velocity = new Vector3( 0, ProjectileSpeed, 0 ); }
        AudioSource.PlayClipAtPoint(FireSound, transform.position);
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown( KeyCode.Space )) { InvokeRepeating( "Fire", 0.000001f, FireRate ); }

        if (Input.GetKeyUp( KeyCode.Space )) { CancelInvoke( "Fire" ); }

        if (Input.GetKey( KeyCode.LeftArrow )) { transform.position += Vector3.left * Speed * Time.deltaTime; }
        else if (Input.GetKey( KeyCode.RightArrow )) { transform.position += Vector3.right * Speed * Time.deltaTime; }

        // restrict the player to the gamespace
        var newX = Mathf.Clamp( transform.position.x, _xmin, _xmax );
        transform.position = new Vector3( newX, transform.position.y, transform.position.z );
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            Health -= missile.GetDamage();
            missile.Hit();
            if (Health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        var levelManager = GameObject.FindObjectOfType<LevelManager>();
        levelManager.LoadLevel("Win Screen");
        Destroy(gameObject);        
    }
}