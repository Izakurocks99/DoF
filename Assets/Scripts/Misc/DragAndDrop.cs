
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    //Initialize Variables
    GameObject getTarget;
    bool isMouseDragging;
    Vector3 offsetValue;
    Vector3 positionOfScreen;

    [SerializeField]
    Vector3 cardOffset;

    ItemScript theCard;
    [SerializeField] LayerMask layerMask;

    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
        //Mouse Button Press Down
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            getTarget = ReturnClickedObject(out hitInfo);
            if (getTarget != null)
            {
                getTarget.GetComponent<Collider>().enabled = false;
                isMouseDragging = true;
                //Converting world position to screen position.
                positionOfScreen = Camera.main.WorldToScreenPoint(getTarget.transform.position);
                offsetValue = getTarget.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z)) + cardOffset;
            }
        }

        //Mouse Button Up
        if (Input.GetMouseButtonUp(0))
        {
            if (getTarget)
            {
                getTarget.GetComponent<Collider>().enabled = true;
                getTarget.GetComponent<Rigidbody>().isKinematic = false;
                theCard.reset = true;
            }
            isMouseDragging = false;

        }

        //Is mouse Moving
        if (isMouseDragging)
        {
            if (!getTarget)
                return;
            //tracking mouse position.
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z);

            //converting screen position to world position with offset changes.
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offsetValue;

            //It will update target gameobject's current postion.
            getTarget.transform.position = currentPosition;
        }


    }

    //Method to Return Clicked Object
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit,100f,layerMask))
        {
            if (hit.collider.gameObject)
            {
                theCard = hit.collider.gameObject.GetComponent<ItemScript>();
                return hit.collider.gameObject;
            }
        }
        return null;
    }

}