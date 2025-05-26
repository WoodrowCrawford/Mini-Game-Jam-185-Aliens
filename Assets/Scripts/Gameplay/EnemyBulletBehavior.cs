using System.Collections;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    public delegate void BulletEventHandler();

    public static event BulletEventHandler onBulletHitPlayer;


    [Header("Bullet values")]
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private float _timeUntilDespawn;

    [SerializeField] private AudioClip _enemyShootClip;



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            

            onBulletHitPlayer?.Invoke();
        }
    }


    void Start()
    {
        //play the shoot sound
        SoundFXManager.instance.PlaySoundFXClip(_enemyShootClip, this.transform, false, 0f);

        //start the timer
        StartCoroutine(StartTimer());
    }



    public IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(_timeUntilDespawn);
        Destroy(this.gameObject);
    }
    

}
