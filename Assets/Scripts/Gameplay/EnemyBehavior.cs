using System.Collections;
using Unity.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public delegate void EnemyEventHandler();

    public static event EnemyEventHandler onEnemyDeath;
    public static event EnemyEventHandler onEnemyDeathFromDespawner;

    [SerializeField] private float _health;
    public float moveSpeed;
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private Animator _animator;

    [SerializeField] private Color _originalColor;

    [SerializeField] private int _pointsWhenKilled;


    [Header("Gun Values")]
    [SerializeField] private Transform _enemyGun;
    public float bulletSpeed;

    [SerializeField] private Transform _enemyBullet;

    [SerializeField] private float _minSecondsToWaitToShoot;
    [SerializeField] private float _maxSecondsToWaitToShoot;

    [SerializeField] private float _secondsToWaitToShoot;



    void OnEnable()
    {
        PlayerBehavior.onPlayerDeath += StopAllCoroutines;
    }

    void OnDisable()
    {
        PlayerBehavior.onPlayerDeath -= StopAllCoroutines;
    }

   

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Despawner"))
        {
            DieFromSpawner();
        }
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    void Start()
    {
        _originalColor = gameObject.GetComponent<SpriteRenderer>().color;


        StartCoroutine(Shoot());
        StartCoroutine(Move());
    }


    public IEnumerator TakeDamage()
    {
        //decrease health by one
        _health -= 1;

        //play enemy damage sound
        SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.instance.enemyDamageClip, this.transform, false, 0f);
    

        if (_health <= 0)
        {
            _health = 0;

            _animator.SetTrigger("EnemyDeath");
            StopCoroutine(Shoot());

            yield break;

        }

        else if (_health > 0)
        {
            //change color of enemy to red
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;

            //wait a second
            yield return new WaitForSeconds(0.5f);

            Debug.Log("Change color back to normal color");

            //revert back to the original color
            gameObject.GetComponent<SpriteRenderer>().color = _originalColor;
        }

        yield break;
    }




    public IEnumerator Shoot()
    {
        //first set the wait seeconds to be a random number
        _secondsToWaitToShoot = Random.Range(_minSecondsToWaitToShoot, _maxSecondsToWaitToShoot);


        yield return new WaitForSeconds(_secondsToWaitToShoot);


        Instantiate(_enemyBullet, _enemyGun.transform.position, Quaternion.identity);


        StartCoroutine(Shoot());

        yield break;
    }

    public IEnumerator Move()
    {
        //move the position of the enemy to the right
        _rb.AddForce(transform.right * moveSpeed, ForceMode2D.Force);
        yield return new WaitForSeconds(2f);
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0;




        //move down
        _rb.AddForce(-transform.up * moveSpeed, ForceMode2D.Force);
        yield return new WaitForSeconds(2f);
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0;


        // //move left
        _rb.AddForce(-transform.right * moveSpeed, ForceMode2D.Force);
        yield return new WaitForSeconds(2f);
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;

        //move down
        _rb.AddForce(-transform.up * moveSpeed, ForceMode2D.Force);

        yield return new WaitForSeconds(2f);
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;

        StartCoroutine(Move());

        yield break;


    }


    public void Die()
    {
        //add points
        GameManager.instance.currentScore += _pointsWhenKilled;

        StopAllCoroutines();

        onEnemyDeath?.Invoke();

        

        Destroy(this.gameObject);
    }

    public void DieFromSpawner()
    {
        onEnemyDeathFromDespawner?.Invoke();
        Destroy(this.gameObject);
    }

}
