using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class VictoryCondition : MonoBehaviour
{
    [SerializeField] GameObject playerObj;
    [SerializeField] Transform targetPosition;
    [SerializeField] Image fadeImage;

    Color imageColor;

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
        GameManager.Instance.gameWin = true;

        imageColor = Color.white;
        imageColor.a = 0;

        Vector3 lookDirection = (targetPosition.position - playerObj.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        
        while (playerObj.transform.position != targetPosition.position || fadeImage.color.a != 1)
        {
            playerObj.GetComponent<Rigidbody>().rotation = Quaternion.Slerp(playerObj.GetComponent<Rigidbody>().rotation, lookRotation, 10 * Time.deltaTime);

            if (playerObj.transform.position != targetPosition.position)
                playerObj.transform.position = Vector3.MoveTowards(playerObj.transform.position, targetPosition.position, 3 * Time.deltaTime);
            if (Vector3.Distance(playerObj.transform.position, targetPosition.position) <= 0.5f)
                playerObj.transform.position = targetPosition.position;

            imageColor.a = Mathf.MoveTowards(imageColor.a, 1, 0.25f * Time.deltaTime);
            fadeImage.color = imageColor;
            yield return null;
        }

        UIManager.Instance.EnableVictoryMenu();
    }
}
