using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PhysicMaterial physic;
    public Slider friction, bounciness, mass;
    public Rigidbody naught, cross;

    public void StartGame()
    {
        FindObjectOfType<Manager>().GameStarted = true;
        gameObject.SetActive(false);
    }

    public void ChangeFriction()
    {
        physic.staticFriction = friction.value;
        physic.dynamicFriction = friction.value;
    }

    public void ChangeBounciness()
    {
        physic.bounciness = bounciness.value;
    }

    public void ChangeMass()
    {
        naught.mass = mass.value;
        cross.mass = mass.value;
    }
}
