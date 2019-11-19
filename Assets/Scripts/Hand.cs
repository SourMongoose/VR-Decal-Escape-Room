using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    public OVRInput.Controller controller;

    private float indexState = 0;
    private float handState = 0;

    private bool grabbing = false;
    private ChessPiece chessPiece = null;

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
        }
    }

    private void Grab(GameObject piece) {
        grabbing = true;
        chessPiece = piece.GetComponent<ChessPiece>();
        chessPiece.pickUp(gameObject);
    }

    private void Release() {
        grabbing = false;
        chessPiece.drop();
        chessPiece = null;
    }
}
