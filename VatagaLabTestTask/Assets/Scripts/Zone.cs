using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Zone : MonoBehaviour
{
    public Material activeMaterial;
    public Material deActiveMaterial;

    private bool doOnes;

    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = deActiveMaterial;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireGrenade")
        {
            bool isGarb = other.GetComponent<FireGrenade>().isGrab;
            if (!isGarb)
            {
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            other.gameObject.GetComponent<FireGrenade>().inZone = true;
            doOnes = true;
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = activeMaterial;
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "FireGrenade")
        {
            // gameObject.GetComponent<MeshRenderer>().sharedMaterial = activeMaterial;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            bool isGarb = other.GetComponent<FireGrenade>().isGrab;
            if (!isGarb && doOnes)
            {
                Vector3 originPosition = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z) - other.transform.localPosition;
                Vector3 desireAngel = new Vector3((-90.0f - other.transform.localRotation.eulerAngles.x) % 360.0f, (360.0f - other.transform.localRotation.eulerAngles.y) % 360.0f, (-125.0f - other.transform.localRotation.eulerAngles.z) % 360.0f);
                other.transform.DOLocalRotate(desireAngel, 1).SetRelative().SetLoops(1, LoopType.Incremental);
                other.transform.DOLocalMove(originPosition, 1).SetRelative().SetLoops(1, LoopType.Incremental);
                doOnes = false;
            }
            if (isGarb)
            {
                doOnes = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "FireGrenade")
        {
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = deActiveMaterial;
            bool isGarb = other.GetComponent<FireGrenade>().isGrab;
            if (!isGarb)
            {
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            other.gameObject.GetComponent<FireGrenade>().inZone = false;
            doOnes = true;
        }
    }
}
