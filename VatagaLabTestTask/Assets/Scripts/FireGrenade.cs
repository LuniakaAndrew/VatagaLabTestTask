using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGrenade : MonoBehaviour
{

    public bool isOld = false;
    public bool isRandomOld = false;
    public bool isGood = false;
    public bool isRandomGood = false;

    public Material defaultMaterial;
    public Material oldMaterial;

    public GameObject stateCube;
    public Material goodCubeMaterial;
    public Material badCubeMaterial;

    public bool isGrab = false;
    public bool inZone = false;


    // Use this for initialization
    void Start()
    {
        if (isRandomOld)
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            int value = Random.Range(0, 100);
            if (value % 2 == 0)
            {
                isOld = true;
            }
            else
            {
                isOld = false;
            }
        }

        if (isRandomGood)
        {
            Random.InitState(System.DateTime.Now.Millisecond%3);
            int value = Random.Range(0, 100);
            if (value % 2 == 0)
            {
                isGood = true;
            }
            else
            {
                isGood = false;
            }
        }

        if (isOld)
        {
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = oldMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = defaultMaterial;
        }

        if (isGood)
        {
            stateCube.GetComponent<MeshRenderer>().sharedMaterial = goodCubeMaterial;
        }
        else
        {
            stateCube.GetComponent<MeshRenderer>().sharedMaterial = badCubeMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
