using UnityEngine;
using System.Collections;

public class ProximityTrigger : MonoBehaviour
{
    bool HasObjectInProximity = false;
    GameObject Object = null;

    public GameObject GetObject()
    {
        return Object;
    }

    public bool HasObject()
    {
        return HasObjectInProximity;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;
        if (otherGO)
        {
            if (otherGO.CompareTag("Player"))
            {
                HasObjectInProximity = true;
                Object = otherGO;
            }

            if (otherGO.CompareTag("BlackBox"))
            {
                HasObjectInProximity = true;
                Object = otherGO;
            }
        }
    }
}
