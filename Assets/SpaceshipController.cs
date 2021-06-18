using System.Collections;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    float xRotation, zRotation, yRotation;
    [SerializeField] bool isBot = false;
    [SerializeField] float movementSpeed;
    [SerializeField] Transform target;
    public Transform explosion;
    Rigidbody rigidbody;
    Vector3 randomizedTargetPosition;
    [SerializeField] float traceDistance = 50;
    Attributes attributes;
    
    void Start()
    {
        StartCoroutine("LookTarget");
        rigidbody = transform.GetComponent<Rigidbody>();
        attributes = transform.GetComponent<Attributes>();
        foreach (Transform child in transform)
        {
            if (child.tag == "Weapon")
            {
                child.GetComponent<SpaceGun>().isBot = isBot;
                child.GetComponent<SpaceGun>().target = target;
            }
                
        }
    }

    void Update()
    {
        // If it's a player.
        if (!isBot)
        {
            float mouseY = Input.GetAxis("Mouse Y");
            float mouseX = Input.GetAxis("Mouse X");

            if (Input.GetKey(KeyCode.W))
            {
                transform.localPosition += transform.TransformVector(new Vector3(0, 0, movementSpeed)) * Time.deltaTime;
            }

            // Rotation settings.
            xRotation += mouseY;
            zRotation += mouseX;
            yRotation += mouseX;
            transform.eulerAngles = new Vector3(xRotation, yRotation, -zRotation);
        }
        else
        {
            // If it's a bot.
            if (attributes.getAlive())
            {
                transform.LookAt(target.position);
                if (Vector3.Distance(target.position, transform.position) > traceDistance)
                    transform.position += transform.forward * movementSpeed * Time.deltaTime;
            }
            else
            {
                rigidbody.isKinematic = false;
                rigidbody.AddForce(-transform.forward * 5);
                rigidbody.AddTorque(transform.up * 5);
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject, 5);
            }

        }
        
    }

    

}
