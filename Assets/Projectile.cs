using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float trackingPrecision;
    public Transform explosion;
    public Transform target;
    public bool isBot, tracerMissile;
    [SerializeField] float projectileForce;
    [SerializeField] float damage;
    // Start is called before the first frame update
    void Start()
    {
        if(target == null)
            target = GameObject.Find("Aim Area").GetComponent<AimArea>().target;


        
        
            
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && tracerMissile)
        {
            Quaternion OriginalRot = transform.rotation;
            transform.LookAt(target.position);
            Quaternion NewRot = transform.rotation;
            transform.rotation = OriginalRot;
            transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, trackingPrecision * Time.deltaTime);
        }
        transform.position += transform.TransformVector(new Vector3(0, 0, 1 * projectileForce)*Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.parent != null)
            if (other.transform.parent.name == "Collider")
                if (other.transform.parent.parent.tag == (isBot ? "Player" : "Enemy"))
                {
                    other.transform.parent.parent.GetComponent<Attributes>().damage(damage);
                    Instantiate(explosion, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            
    }

}
