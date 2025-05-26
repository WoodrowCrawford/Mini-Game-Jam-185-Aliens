using System.Collections;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public delegate void BulletEventHandler();

    public static event BulletEventHandler onBulletHitEnemy;
    

    [Header("Bullet values")]
    [SerializeField] private float _damage;
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private float _timeUntilDespawn;

    public float speed;

    [SerializeField] private AudioClip _playerShootClip;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);

            collision.gameObject.GetComponent<EnemyBehavior>().StartCoroutine(collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage());
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);

            collision.gameObject.GetComponent<EnemyBehavior>().StartCoroutine(collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage());
        }
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        //play the shoot sound
        SoundFXManager.instance.PlaySoundFXClip(_playerShootClip, this.transform, false, 0f);

        Debug.Log("Add force here");
        _rb.linearVelocity = transform.up * speed;

        //start the timer
        StartCoroutine(StartTimer());
    }

    

    public IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(_timeUntilDespawn);
        Destroy(this.gameObject);
    }
}
