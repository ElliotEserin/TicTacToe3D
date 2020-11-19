using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    string defaultTag;

    private void Start()
    {
        defaultTag = tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        tag = other.tag;
    }
    private void OnTriggerExit(Collider other)
    {
        tag = defaultTag;
    }
}
