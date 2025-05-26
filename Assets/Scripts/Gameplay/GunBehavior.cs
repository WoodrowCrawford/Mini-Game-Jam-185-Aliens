using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    [Header("Gun vaules")]
    [SerializeField] private GameObject _gun;

    [SerializeField] private GameObject _bullet;


    [SerializeField] private float _shootCooldown;

    [SerializeField] private bool _canFireGun;

    public Vector2 forceToApply;


    void OnEnable()
    {
        PlayerBehavior.onFireButtonPressed +=  CallFireGun;
    }

    void OnDisable()
    {
        PlayerBehavior.onFireButtonPressed -= CallFireGun;
    }









    void Awake()
    {
        _gun = GameObject.Find("Gun").gameObject;
    }


    public void CallFireGun()
    {
        StartCoroutine(FireGun());
    }

    public IEnumerator FireGun()
    {
        Debug.Log("fire the gun");

        if (_canFireGun)
        {
            _canFireGun = false;
            //create a bullet at the gun loctaion
            Instantiate(_bullet, _gun.transform.position, Quaternion.identity);



            yield return new WaitForSeconds(_shootCooldown);

            _canFireGun = true;


            yield break;
        }

    }

    
}
