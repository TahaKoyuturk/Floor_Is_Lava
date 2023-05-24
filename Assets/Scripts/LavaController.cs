using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class LavaController : MonoBehaviour
{
    [SerializeField] private TMP_Text lavaComingText;
    public int upDistance = -2; // Yukarı hareket mesafesi
    public int downDistance = -5; // Aşağı hareket mesafesi
    public int movementDuration = 5; // Hareket süresi
    public int movementDuration2 = 8; // Hareket süresi
    public int pauseDuration = 2; // Hareket süresi

    private Sequence movementSequence;
    private bool isCountdownOver = false;
    [SerializeField] private Light myLight;

    private void Start()
    {
        //myLight = GetComponent<Light>();
        myLight.intensity = 1f;
        StartCoroutine(Countdown(3));
    }
    private void StartGame()
    {
        if (isCountdownOver)
        {
            LavaMovement();
        }
    }
    private void LavaMovement()
    {
        movementSequence = DOTween.Sequence();
        movementSequence.Append(transform.DOMoveY(upDistance, movementDuration));

        StartCoroutine(CountdownForLava(movementDuration-1));
        
        movementSequence.AppendInterval(pauseDuration);

        movementSequence.Append(transform.DOMoveY(downDistance, movementDuration));
        
        movementSequence.AppendInterval(pauseDuration);

        movementSequence.Append(transform.DOMoveY(upDistance, movementDuration2));
        StartCoroutine(CountdownForLava(movementDuration-1));

        
        movementSequence.AppendInterval(pauseDuration);
        myLight.intensity = 1f;
        movementSequence.Append(transform.DOMoveY(downDistance, movementDuration2));
        myLight.intensity = 1f;
        movementSequence.SetLoops(-1);
        movementSequence.SetEase(Ease.Linear);
    }
    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {

            yield return new WaitForSeconds(1);
            count--;
        }
        isCountdownOver = true;
        StartGame();
    }
    IEnumerator CountdownForLava(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {
            if(count<5)
                lavaComingText.text = "Lava coming in " + count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        myLight.intensity = 2.5f;
        isCountdownOver = true;
        lavaComingText.text = "";
    }
    private void OnDestroy()
    {
        movementSequence.Kill();
        DOTween.Clear();
    }
}


