using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    public GameObject lamp;
    public GameObject hands;

    private Vector3 moveDirection = Vector3.zero;
    private bool take = false;
    private bool inHands = false;
    private GameObject fireGrenade;

    void Update()
    {
        //Ray for get Objects
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
        float rayLength = 2f;

        // actual Ray
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        // debug Ray
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength, 1 << LayerMask.NameToLayer("FireGrenade")))
        {
            // our Ray intersected a collider
            if (hit.collider.tag == "FireGrenade")
            {
                take = true;
                if (inHands == false)
                    fireGrenade = hit.collider.gameObject;
            }
        }

        //Take fire grenade
        if (Input.GetKeyDown(KeyCode.E))
        {
            inHands = false;

            if (take && fireGrenade != null)
            {
                bool grab = fireGrenade.GetComponent<FireGrenade>().isGrab;
                if (!grab)
                {
                    fireGrenade.GetComponent<Rigidbody>().isKinematic = true;
                    fireGrenade.GetComponent<FireGrenade>().isGrab = true;
                    inHands = true;
                }
            }

        }
        //in order to avoid taking a fire extinguisher if we just looked at him
        take = false;

        if (inHands)
        {
            fireGrenade.transform.position = hands.transform.position;
        }

        if (!inHands)
        {
            if (fireGrenade != null)
            {
                bool grab = fireGrenade.GetComponent<FireGrenade>().isGrab;
                if (grab)
                {
                    fireGrenade.GetComponent<Rigidbody>().isKinematic = false;
                    fireGrenade.GetComponent<FireGrenade>().isGrab = false;
                    fireGrenade = null;
                }
            }
        }

        //Lamp Move 
        if (Input.GetKeyDown(KeyCode.F))
        {
            lamp.GetComponent<Rigidbody>().isKinematic = !lamp.GetComponent<Rigidbody>().isKinematic;
            if (lamp.GetComponent<Rigidbody>().isKinematic == true)
            {
                Vector3 originPosition = new Vector3(0, 0, 0) - lamp.transform.localPosition;
                Vector3 desireAngel = new Vector3((360.0f - lamp.transform.localRotation.eulerAngles.x) % 360.0f, (360.0f - lamp.transform.localRotation.eulerAngles.y) % 360.0f, (360.0f - lamp.transform.localRotation.eulerAngles.z) % 360.0f);
                lamp.transform.DOLocalRotate(desireAngel, 1).SetRelative().SetLoops(1, LoopType.Incremental);
                lamp.transform.DOLocalMove(originPosition, 1).SetRelative().SetLoops(1, LoopType.Incremental);
            }
        }
        //Task List
        if (Input.GetKeyDown(KeyCode.C))
        {
        }
        //Exit Program
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //Character Move
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
