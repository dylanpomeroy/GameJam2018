using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class MouseDrag : MonoBehaviour {



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    bool wasDragging = false;
    private void OnMouseDrag()
    {
        bool useParent = false;
        if (transform.parent != null) useParent = true;

        if (transform.position.x > 8 && !wasDragging)
        {
            if (useParent)
            {
                if (transform.parent.transform.position.y == Ref.SelectorPeicePlacements[0].y) Ref.SelectorSectionsFull[0] = false;
                else if (transform.parent.transform.position.y == Ref.SelectorPeicePlacements[1].y) Ref.SelectorSectionsFull[1] = false;
                else Ref.SelectorSectionsFull[2] = false;
            }
            else
            {
                if (transform.position.y == Ref.SelectorPeicePlacements[0].y) Ref.SelectorSectionsFull[0] = false;
                else if (transform.position.y == Ref.SelectorPeicePlacements[1].y) Ref.SelectorSectionsFull[1] = false;
                else Ref.SelectorSectionsFull[2] = false;
            }
        }

        wasDragging = true;

        var mousePos = Input.mousePosition;
        mousePos.x -= Screen.width / 2;
        mousePos.y -= Screen.height / 2;
        mousePos.z = -0.6f;

        var curScreenSpace = new Vector3(8 * mousePos.x / Screen.width, 8 * mousePos.y / Screen.height, -0.6f);

        if (useParent) transform.parent.transform.position = curScreenSpace;
        else transform.position = curScreenSpace;
    }

    private void OnMouseUp()
    {
        bool useParent = false;
        if (transform.parent != null) useParent = true;

        if (wasDragging)
        {
            if (useParent) transform.parent.transform.position = GetClosestGridPosition(transform.position);
            else transform.position = GetClosestGridPosition(transform.position);
        }

        wasDragging = false;
    }

    private Vector3 GetClosestGridPosition(Vector3 position)
    {
        var closestPosition = new Vector3(0, 0, -0.6f);

        if (position.y >= 2.5f) closestPosition.y = 3.75f;
        else if (position.y >= 0f) closestPosition.y = 1.25f;
        else if (position.y >= -2.5f) closestPosition.y = -1.25f;
        else closestPosition.y = -3.75f;

        if (position.x >= 2.5f) closestPosition.x = 3.75f;
        else if (position.x >= 0f) closestPosition.x = 1.25f;
        else if (position.x >= -2.5f) closestPosition.x = -1.25f;
        else closestPosition.x = -3.75f;

        return closestPosition;
    }
}
