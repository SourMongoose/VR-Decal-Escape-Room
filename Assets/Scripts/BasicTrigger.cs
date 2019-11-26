using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTrigger : MonoBehaviour
{
    //Make object hover until all required objects are present. 
    public float hoverForce = 12f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag == "TriggerKey");


    }
    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(Vector3.up * hoverForce, ForceMode.Acceleration);
    }
}
