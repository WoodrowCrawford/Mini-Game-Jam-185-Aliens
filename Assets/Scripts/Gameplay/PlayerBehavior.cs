using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    //get the player input system
    PlayerActions inputActions;

    public delegate void PlayerEventHandler();
   

    public static event PlayerEventHandler onFireButtonPressed;
    public static event PlayerEventHandler onPauseButtonPressed;
    public static event PlayerEventHandler onPlayerDeath;


    [Header("Player values")]

    [SerializeField] private float _health;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;

    [SerializeField] private Animator _animator;

    void OnEnable()
    {
        inputActions = new PlayerActions();

        inputActions.Default.Enable();
        inputActions.Default.Shoot.performed += ctx => onFireButtonPressed?.Invoke();

        inputActions.Default.TogglePause.performed += ctx => onPauseButtonPressed?.Invoke();



        EnemyBulletBehavior.onBulletHitPlayer += TakeDamage;


    }

    void OnDisable()
    {



        inputActions.Default.Shoot.performed -= ctx => onFireButtonPressed?.Invoke();
        inputActions.Default.TogglePause.performed -= ctx => onPauseButtonPressed?.Invoke();


        EnemyBulletBehavior.onBulletHitPlayer -= TakeDamage;
       
        inputActions.Default.Disable();
    }

   

   

    void Awake()
    {
        _animator = GetComponent<Animator>();
    
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        float x = inputActions.Default.Movement.ReadValue<Vector2>().x;
        float y = inputActions.Default.Movement.ReadValue<Vector2>().y;


        _rb.linearVelocity = new Vector2(x * _speed, y * _speed);

        if (_rb.linearVelocityX > 0)
        {
            Debug.Log("MOve right");
            _animator.SetBool("TurningRight", true);
            _animator.SetBool("TurningLeft", false);
            _animator.SetBool("Idle", false);
        }
        else if (_rb.linearVelocityX < 0)
        {
            Debug.Log("move left");
            _animator.SetBool("TurningLeft", true);
            _animator.SetBool("TurningRight", false);
            _animator.SetBool("Idle", false);
        }

        else if (_rb.linearVelocityX == 0)
        {
            _animator.SetBool("TurningRight", false);
            _animator.SetBool("TurningLeft", false);
            _animator.SetBool("Idle", true);
        }

    }


    public void TakeDamage()
    {
        _health -= 1;

        if (_health <= 0)
        {
            SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.instance.enemyDamageClip, this.transform, false, 0f);
            _animator.SetTrigger("PlayerDeath");
        }
    }

    public void Die()
    {
        onPlayerDeath?.Invoke();
       Destroy(this.gameObject);
    }


}
