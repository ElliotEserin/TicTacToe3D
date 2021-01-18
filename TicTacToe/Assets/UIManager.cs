using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("MENU")]
    public PhysicMaterial physic;
    public Slider friction, bounciness, mass;
    public Rigidbody naught, cross;
    public Animator animator;
    public float defaultFriction, defaultBounciness, defaultMass;

    [Header("END")]
    public TextMeshProUGUI winText;
    public GameObject endScreen;


    private void Start()
    {
        friction.value = defaultFriction;
        bounciness.value = defaultBounciness;
        mass.value = defaultMass;

        physic.dynamicFriction = physic.staticFriction = defaultFriction;
        physic.bounciness = defaultBounciness;
        naught.mass = cross.mass = defaultMass;
    }

    public void StartGame()
    {
        FindObjectOfType<Manager>().GameStarted = true;
        animator.SetTrigger("Begin");
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

    public void DisplayEndScreen(bool winner)
    {
        endScreen.SetActive(true);
        Destroy(GameObject.Find("Tutorial"));
        winText.SetText(((winner) ? "NAUGHTS" : "CROSSES") + " WIN!");
    }

    public void Restart()
    {
        var manager = FindObjectOfType<Manager>();
        endScreen.SetActive(false);
        manager.ClearBoard();
        manager.GameStarted = false;
        manager.gameEnded = false;
        animator.SetTrigger("Restart");
    }

    public void ExitGame() => Application.Quit();

    public void Refresh() => UnityEngine.SceneManagement.SceneManager.LoadScene(0);
}
