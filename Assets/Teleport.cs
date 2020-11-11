using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 departurePortalPosition;
    public Transform destinationPortal;
    public GameObject rocket;

    public float getDepartureX()
    {
        return this.departurePortalPosition.x;
    }

    public float getDepartureY()
    {
        return this.departurePortalPosition.y;
    }

    public float getDepartureZ()
    {
        return this.departurePortalPosition.z;
    }

    public void setDepartureX(float x)
    {
        this.departurePortalPosition.x = x;
    }

    public void setDepartureY(float y)
    {
        this.departurePortalPosition.y = y;
    }

    public void setDepartureZ(float z)
    {
        this.departurePortalPosition.z = z;
    }

    public Teleport(float x, float y, float z)
    {
        this.departurePortalPosition.x = x;
        this.departurePortalPosition.y = y;
        this.departurePortalPosition.z = z;
    }

    private void OnTriggerEnter(Collider other)
    {
        rocket.transform.position = destinationPortal.transform.position - new Vector3(4f,0f,0f);
    }

}