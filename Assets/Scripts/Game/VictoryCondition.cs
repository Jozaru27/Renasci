using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCondition : MonoBehaviour
{
    [SerializeField] GameObject playerObj;
    [SerializeField] Transform targetPosition;
    [SerializeField] Image fadeImage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FinishGame());
            GameManager.Instance.playerCannotMove = true;
        }    
    }

    IEnumerator FinishGame()
    {
        while (playerObj.transform.position != targetPosition.position)
        {
            //playerObj.transform.position = Vector3.MoveTowards(playerObj.transform.position, targetPosition.position,);
            yield return null;
        }

        GameManager.Instance.gameWin = true;
        UIManager.Instance.EnableVictoryMenu();
    }
}
