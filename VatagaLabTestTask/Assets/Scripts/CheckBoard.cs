using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBoard : MonoBehaviour
{
    public GameObject testBoard;
    public GameObject checkPass;
    public GameObject checkFail;

    public GameObject lampForCheck;
    public GameObject[] fireGrenadesForCheck;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            testBoard.SetActive(!testBoard.activeInHierarchy);
            CleanChecks();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckLamp();
            CheckFireGrenades();
        }

    }

    void TogglePassChange(int checkNumber)
    {
        Toggle[] passToggles = checkPass.GetComponentsInChildren<Toggle>();
        Toggle[] failToggles = checkFail.GetComponentsInChildren<Toggle>();
        if (passToggles[checkNumber].isOn && !failToggles[checkNumber].isOn)
        {
            passToggles[checkNumber].GetComponentInChildren<Image>().color = Color.green;
            failToggles[checkNumber].GetComponentInChildren<Image>().color = Color.green;
        }
        else
        {
            passToggles[checkNumber].GetComponentInChildren<Image>().color = Color.red;
            failToggles[checkNumber].GetComponentInChildren<Image>().color = Color.red;
        }
    }

    void ToggleFailChange(int checkNumber)
    {
        Toggle[] passToggles = checkPass.GetComponentsInChildren<Toggle>();
        Toggle[] failToggles = checkFail.GetComponentsInChildren<Toggle>();
        if (!passToggles[checkNumber].isOn && failToggles[checkNumber].isOn)
        {
            passToggles[checkNumber].GetComponentInChildren<Image>().color = Color.green;
            failToggles[checkNumber].GetComponentInChildren<Image>().color = Color.green;
        }
        else
        {
            passToggles[checkNumber].GetComponentInChildren<Image>().color = Color.red;
            failToggles[checkNumber].GetComponentInChildren<Image>().color = Color.red;
        }
    }

    void CheckLamp()
    {
        if (lampForCheck.GetComponent<Rigidbody>().isKinematic == true)
        {
            TogglePassChange(3);
        }
        else
        {
            ToggleFailChange(3);
        }
    }

    void CheckFireGrenades()
    {
        bool inZones = true;
        bool isNotOld = false;
        bool isGood = true;
        for (int i = 0; i < fireGrenadesForCheck.Length; i++)
        {
            bool inZone = fireGrenadesForCheck[i].GetComponent<FireGrenade>().inZone;
            bool isOld = fireGrenadesForCheck[i].GetComponent<FireGrenade>().isOld;
            bool istGood = fireGrenadesForCheck[i].GetComponent<FireGrenade>().isGood;

            if (inZone == false)
            {
                inZones = false;
            }

            if (isOld == true)
            {
                isNotOld = true;
            }

            if (istGood == false)
            {
                istGood = false;
            }
        }

        if (inZones)
        {
            TogglePassChange(0);
        }
        else
        {
            ToggleFailChange(0);
        }

        if (!isNotOld)
        {
            TogglePassChange(1);
        }
        else
        {
            ToggleFailChange(1);
        }

        if (isGood)
        {
            TogglePassChange(2);
        }
        else
        {
            ToggleFailChange(2);
        }
    }

    void CleanChecks()
    {
        Toggle[] passToggles = checkPass.GetComponentsInChildren<Toggle>();
        Toggle[] failToggles = checkFail.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < passToggles.Length; i++)
        {
            passToggles[i].isOn = false;
            failToggles[i].isOn = false;
            passToggles[i].GetComponentInChildren<Image>().color = Color.red;
            failToggles[i].GetComponentInChildren<Image>().color = Color.red;
        }
    }
}
