using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    public Vector3 holdPosition = new Vector3(0, -0.025f, 0.03f);
    public Vector3 holdRotation = new Vector3(0, 180, 0);
    public OVRInput.Controller controller;

    private float indexState = 0;
    private float handState = 0;

    private bool grabbing = false;
    private ChessPiece chessPiece = null;
    private FlashLight flashLight = null;

    public void Update() {
        indexState = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
        handState = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);

        if (grabbing && indexState <= 0.9f) {
            Release();
        }
    }

    public void OnTriggerStay(Collider other) {
        Debug.Log(other.gameObject);
        if (other.CompareTag("ChessPiece")) {
            Debug.Log(grabbing + " " + indexState);
            if (!grabbing && indexState > 0.9f) {
                Grab(other.gameObject);
            }
        } else if (other.CompareTag("FlashLight")) {
             Debug.Log(grabbing + " " + indexState);
             if (!grabbing && indexState > 0.9f) {
                 GrabFlashLight(other.gameObject);
             }
        }
    }

    private void Grab(GameObject piece) {
        grabbing = true;
        chessPiece = piece.GetComponent<ChessPiece>();
        chessPiece.pickUp(gameObject);
    }

    private void GrabFlashLight(GameObject flashlight) {
        grabbing = true;
        // flashLight = flashlight.GetComponent<FlashLight>();
        flashLight = flashlight;
        flashLight.transform.parent = transform;
        gun.transform.localPosition = holdPosition;
        gun.transform.localEulerAngles = holdRotation;
        gun.GetComponent<Rigidbody>().useGravity = false;
        gun.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Release() {
        grabbing = false;
        if (chessPiece )
        {
            chessPiece.drop();
            chessPiece = null;
        } 
        else 
        {
            flashLight.transform.parent = null;
            Rigidbody rigidbody = flashLight.GetComponent<Rigidbody>();
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.velocity = OVRInput.GetLocalControllerVelocity(controller);
            grabbing = false;
            flashLight = null;
        }
    }
}
