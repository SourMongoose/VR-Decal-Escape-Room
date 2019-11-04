using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour {
    private const float
        X_THRESHOLD = 0.4f,
        Z_THRESHOLD = 0.4f,
        Y_THRESHOLD = 1f;

    private Vector3 origPosition;

    private GameObject follow = null;
    private Vector3 followDiff;
    
    public void Start() {
        //keep track of original position
        origPosition = transform.localPosition;
    }

    public void Update() {
        //follow hand around
        if (follow != null) {
            transform.position = follow.transform.position + followDiff;
        }
    }

    public void moveBack() {
        transform.localPosition = origPosition;
    }

    public void pickUp(GameObject hand) {
        follow = hand;
        followDiff = transform.position - hand.transform.position;
    }

    public void drop() {
        follow = null;

        int x = Mathf.RoundToInt(transform.localPosition.x);
        int z = Mathf.RoundToInt(transform.localPosition.z);
        int y = 0;

        //within board bounds
        if (x >= 0 && x < 8 && z >= 0 && z < 8) {
            //close enough to center of square
            if (Mathf.Abs(transform.localPosition.x - x) < X_THRESHOLD
                && Mathf.Abs(transform.localPosition.z - z) < Z_THRESHOLD
                && Mathf.Abs(transform.localPosition.y - y) < Y_THRESHOLD) {
                transform.localPosition = new Vector3(x, y, z);
                return;
            }
        }

        moveBack();
    }
}
