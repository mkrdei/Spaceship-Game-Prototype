using UnityEngine;

public class AimArea : MonoBehaviour
{
    public Transform crosshair;
    Vector3 targetScreenPosition;
    public Camera cam;
    public Transform target;
    MeshRenderer meshRenderer;
    CapsuleCollider capsuleCollider;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        meshRenderer.enabled = false;
        capsuleCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {




        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            meshRenderer.enabled = true;
            capsuleCollider.enabled = true;
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            meshRenderer.enabled = false;
            capsuleCollider.enabled = false;
        }




        if (target != null)
        {
            targetScreenPosition = cam.WorldToScreenPoint(target.position);
            crosshair.position = targetScreenPosition;

            if ((cam.WorldToViewportPoint(target.position).x < 0 || cam.WorldToViewportPoint(target.position).x > 1) ||
            (cam.WorldToViewportPoint(target.position).y < 0 || cam.WorldToViewportPoint(target.position).y > 1))
            {
                target = null;
            }


        }
        else
        {
            crosshair.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
            if (other.transform.parent.name == "Collider")
                if (other.transform.parent.parent.tag == "Enemy")
                    target = other.transform.parent.parent;
    }
}
