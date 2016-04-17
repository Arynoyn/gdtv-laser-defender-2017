#region Using Directives

using UnityEngine;

#endregion

public class FormationController : MonoBehaviour {

    public GameObject EnemyPrefab;
    public float Width = 8f,
                 Height = 5f,
                 Speed = 2.5f,
                 SpawnDelay = 0.5f;

    private float _xmin,
                  _xmax;

    private bool _movingRight = true;

    // Use this for initialization
    void Start() {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint( new Vector3( 0, 0, distanceToCamera ) );
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint( new Vector3( 1, 0, distanceToCamera ) );
        _xmin = leftBoundary.x;
        _xmax = rightBoundary.x;

        //SpawnEnemies();
        SpawnUntilFull();
    }

    public void OnDrawGizmos() { Gizmos.DrawWireCube( transform.position, new Vector3( Width, Height, 0 ) ); }

    // Update is called once per frame
    void Update() {
        if (_movingRight) { transform.position += Vector3.right * Speed * Time.deltaTime; }
        else { transform.position += Vector3.left * Speed * Time.deltaTime; }

        float rightEdgeOfFormation = transform.position.x + ( Width / 2 );
        float leftEdgeOfFormation = transform.position.x - ( Width / 2 );
        if (leftEdgeOfFormation < _xmin) { _movingRight = true; }
        else if (rightEdgeOfFormation > _xmax) { _movingRight = false; }

        // restrict the player to the gamespace
        float newX = Mathf.Clamp( transform.position.x, _xmin, _xmax );
        transform.position = new Vector3( newX, transform.position.y, transform.position.z );

        if (AllEnemiesAreDead()) {
            //SpawnEnemies();
            SpawnUntilFull();
        }
    }

    private bool AllEnemiesAreDead() {
        foreach (Transform childPositionGameObject in transform) { if (childPositionGameObject.childCount > 0) { return false; } }
        return true;
    }

    private Transform NextFreePosition() {
        foreach (Transform childPositionGameObject in transform) { if (childPositionGameObject.childCount <= 0) { return childPositionGameObject; } }
        return null;
    }

    private void SpawnUntilFull() {
        Transform freePosition = NextFreePosition();
        // Create an instance of the Enemy Prefab
        if (freePosition) {
            GameObject enemy = Instantiate(EnemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            // Set the enemy to spawn within the EnemyFormation
            if (enemy != null) { enemy.transform.parent = freePosition; }
        }
        if (NextFreePosition()) {
            Invoke( "SpawnUntilFull", SpawnDelay );
        }
    }

    private void SpawnEnemies() {
        foreach (Transform child in transform) {
            // Create an instance of the Enemy Prefab
            GameObject enemy = Instantiate( EnemyPrefab, child.transform.position, Quaternion.identity ) as GameObject;
            // Set the enemy to spawn within the EnemyFormation
            if (enemy != null) { enemy.transform.parent = child; }
        }
    }

}