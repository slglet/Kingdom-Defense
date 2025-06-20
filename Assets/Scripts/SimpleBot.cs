using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBot : MonoBehaviour
{
    public Game game;
    private bool isThinking = false;

    public void Start()
    {
        if (game == null)
        {
            game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        }
    }

    void Update()
    {
        if (!isThinking && !game.IsGameOver() && game.GetCurrentPlayer() == "black")
        {
            StartCoroutine(PlayRandomMove());
        }
        }


    private IEnumerator PlayRandomMove()
    {
        isThinking = true;

        yield return new WaitForSeconds(1.0f); // delay before bot acts

        GameObject[] blackPieces = game.GetPlayerBlack();
        List<GameObject> validPieces = new List<GameObject>();

        foreach (GameObject piece in blackPieces)
        {
            if (piece != null)
            {
                validPieces.Add(piece);
            }
        }

        if (validPieces.Count == 0) yield break;

        GameObject selectedPiece = null;
        List<GameObject> generatedMovePlates = new List<GameObject>();

        for (int i = 0; i < validPieces.Count; i++)
        {
            GameObject candidate = validPieces[Random.Range(0, validPieces.Count)];
            candidate.GetComponent<Chessman>().InitiateMovePlates();

            yield return new WaitForSeconds(3.5f);

            GameObject[] allPlates = GameObject.FindGameObjectsWithTag("MovePlate");

            if (allPlates.Length > 0)
            {
                selectedPiece = candidate;
                generatedMovePlates.AddRange(allPlates);
                break;
            }
        }

        if (generatedMovePlates.Count == 0)
        {
            isThinking = false;
            yield break;
        }

        yield return new WaitForSeconds(0.5f); // add suspense :)

        GameObject selectedMove = generatedMovePlates[Random.Range(0, generatedMovePlates.Count)];
        selectedMove.GetComponent<MovePlate>().OnMouseUp();

        yield return new WaitForEndOfFrame();
        isThinking = false;
    }
}