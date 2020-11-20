using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public bool currentTurn;
    public float spawnHeight;
    public GameObject crossPrefab, naughtPrefab;

    [Space(20)]
    [SerializeField] public Node[] nodes;

    bool gameStarted = false;
    bool gameEnded = false;
    bool waiting = false;

    public bool GameStarted
    {
        get
        {
            return gameStarted;
        }
        set
        {
            gameStarted = value;

            va.AnimateSkybox(currentTurn);
            va.ChangeBloomTexture(currentTurn);
        }
    }

    VisualAnimations va;

    private void Start()
    {
        va = FindObjectOfType<VisualAnimations>();
    }

    private void OnMouseUp()
    {
        if (!gameEnded && gameStarted && waiting == false)
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (currentTurn)
                    Instantiate(crossPrefab, new Vector3(hit.point.x, spawnHeight, hit.point.z), Quaternion.identity);
                else
                    Instantiate(naughtPrefab, new Vector3(hit.point.x, spawnHeight, hit.point.z), Quaternion.identity);

                StartCoroutine(Check());
            }
        }
    }

    IEnumerator Check()
    {
        waiting = true;
        yield return new WaitForSeconds(2);
        CheckForWinner();

        if (!gameEnded)
        {
            currentTurn = !currentTurn;
            waiting = false;

            va.AnimateSkybox(currentTurn);
            va.ChangeBloomTexture(currentTurn);
        }
    }

    void CheckForWinner()
    {
        var crossWin = CheckLayers("Cross");
        var naughtWin = CheckLayers("Naught");

        if (crossWin)
            Debug.Log("CROSSES WIN");
        else if (naughtWin)
            Debug.Log("NAUGHTs WIN");

        if (crossWin || naughtWin)
            gameEnded = true;
    }

    bool CheckLayers(string tag)
    {
        for (int i = 0; i < 3; i++)
        {
            bool[] answers = new bool[17];

            //Y AXIS
            //rows
            answers[0] = CheckNodes(0 + i * 9, 1, tag);
            answers[1] = CheckNodes(3 + i * 9, 1, tag);
            answers[2] = CheckNodes(6 + i * 9, 1, tag);

            //columns
            answers[3] = CheckNodes(0 + i * 9, 3, tag);
            answers[4] = CheckNodes(1 + i * 9, 3, tag);
            answers[5] = CheckNodes(2 + i * 9, 3, tag);

            //diagonals
            answers[6] = CheckNodes(0 + i * 9, 4, tag);
            answers[7] = CheckNodes(2 + i * 9, 2, tag);

            //X AXIS
            //rows
            answers[8] = CheckNodes(0 + i, 9, tag);
            answers[9] = CheckNodes(3 + i, 9, tag);
            answers[10] = CheckNodes(6 + i, 9, tag);

            //diagonals
            answers[11] = CheckNodes(0 + i, 12, tag);
            answers[12] = CheckNodes(6 + i, 6, tag);
            
            //Z AXIS
            //diagonals
            answers[13] = CheckNodes(0 + i * 3, 10, tag);
            answers[14] = CheckNodes(2 + i * 3, 8, tag);

            if (CheckAnswers(answers)) return true;
        }
        return false;
    }

    bool CheckAnswers(bool[] answers)
    {
        foreach (bool answer in answers)
            if (answer == true)
                return true;
        return false;
    }

    bool CheckNodes(int startNode, int step, string tag)
    {
        if (CheckNode(nodes[startNode]) && CheckNode(nodes[startNode + step]) && CheckNode(nodes[startNode + step * 2]))
            return true;

        return false;

        bool CheckNode(Node node)
        {
            return node.CompareTag(tag);
        }
    }
}
