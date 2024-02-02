using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    TextMeshProUGUI infoText;
    Rigidbody2D moverA;
    Rigidbody2D moverB;
    Rigidbody2D piano;

    private float _menuDelay = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            infoText = GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>();
            moverA = GameObject.Find("PlayerLeft(Clone)").GetComponent<Rigidbody2D>();
            moverB = GameObject.Find("PlayerRight(Clone)").GetComponent<Rigidbody2D>();
            piano = GameObject.FindWithTag("Piano").GetComponent<Rigidbody2D>();
            moverA.constraints = RigidbodyConstraints2D.FreezeAll;
            moverB.constraints = RigidbodyConstraints2D.FreezeAll;
            piano.constraints = RigidbodyConstraints2D.FreezeAll;
            infoText.enabled = true;
            infoText.text = "Well done! The piano is safe in the lorry! Now for the harp!";
            Invoke("BackToMenu", _menuDelay);
        }
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
