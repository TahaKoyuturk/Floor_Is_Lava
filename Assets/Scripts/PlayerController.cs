using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float bounce = 5f;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private TMP_Text CountDownText;

    private Rigidbody Rigidbodyrb;
    private Animator animator;
    private bool isJumping;
    private bool isJumperPower;
    private bool isDead;
    private bool isCountdownOver;

    void Start()
    {
        animator = GetComponent<Animator>();
        isJumperPower = false;
        isJumping = false;
        isCountdownOver = false;
        Rigidbodyrb = GetComponent<Rigidbody>();
        StartCoroutine(Countdown(3));
    }
    void Update()
    {
        if (isCountdownOver)
        {
            CountDownText.text = "";
            if (!isDead)
            {
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);

                if (isJumperPower)
                {
                    isJumperPower = false;

                    jumpPower += PlayerPrefs.GetInt("JumpPower");
                }
                if (Input.GetMouseButtonDown(0) && !isJumping)
                {
                    Rigidbodyrb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                }
            }
            else
            {
                StartCoroutine(GameOver());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1);
            
            EventManager.CoinCollectEvent();

            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("JumpPower"))
        {
            isJumperPower = true;

            PlayerPrefs.SetInt("JumpPower", PlayerPrefs.GetInt("JumpPower") + 1);
           
            EventManager.JumpPowerCollectEvent();

            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (other.gameObject.CompareTag("Lava"))
        {
            EventManager.PlayerDieEvent();

            isDead = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isJumping = false;
        }
        if (collision.gameObject.CompareTag("Tramboline"))
        {
            Debug.Log("Tramboline");
            Rigidbodyrb.AddForce(Vector3.up * bounce, ForceMode.Impulse);
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isJumping = true;

            if(transform.position.y< this.gameObject.transform.position.y)
            {
                Rigidbodyrb.isKinematic = true;
            }
        }
    }
    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {
            CountDownText.text=count.ToString();
            // display something...
            yield return new WaitForSeconds(1);
            count--;
        }
        CountDownText.text = count.ToString();
        // count down is finished...
        isCountdownOver = true;
    }
    IEnumerator GameOver()
    {
        animator.SetBool("Die",true);
        
        yield return new WaitForSeconds(1.3f);
        
        SceneManager.LoadScene(1);
    }
    public float GetPlayerSpeed()
    {
        return Speed;
    }
}
