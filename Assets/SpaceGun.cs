using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGun : MonoBehaviour
{
    public Transform missile;
    public Transform target;
    Transform myMissile;
    bool isFiring = false;
    [SerializeField] float trackingPrecision;
    public bool isBot;
    [SerializeField] float fireRate;
    [SerializeField] bool tracerMissile;
    [SerializeField] float destroyMissleTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.Mouse0) && !isFiring) || (isBot && !isFiring))
        {
            StartCoroutine("Fire");
        }
    }

    IEnumerator Fire()
    {

        myMissile = Instantiate(missile, transform.position, transform.rotation);
        if (isBot)
        {
            myMissile.GetComponent<Projectile>().target = target;
            myMissile.GetComponent<Projectile>().isBot = isBot;
        }
        myMissile.GetComponent<Projectile>().tracerMissile = tracerMissile;
        myMissile.GetComponent<Projectile>().trackingPrecision = trackingPrecision;

        Destroy(myMissile.gameObject, destroyMissleTime);
        isFiring = true;
        yield return new WaitForSeconds(fireRate);
        isFiring = false;

    }
}
