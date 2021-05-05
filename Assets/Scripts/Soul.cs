using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VTL.ListView;

public class Soul : MonoBehaviour
{
    public Button readyButton;
    public static Soul instance = null;//the single instance of design manager available
    public List<GameObject> memoryList;
    public Text ascensionPoints, descensionPoints;
    public ListViewManager listViewManager;
    public GameObject memoryPanel;
    public GameObject decisionPanel;
    public GameObject scorePanel;
    public Camera mainCamera;
    public  Image imageCooldown;
    public float cooldown;
    bool isCooldown;
    public bool isReached = false;
    private enum TrueDecision
    {
        Guilty,
        Innocent
    };
    [SerializeField]
    TrueDecision trueDecision;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
  }
	private void Update()
	{
     if(isCooldown)
        {
            imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;
            if(imageCooldown.fillAmount >= 1)
            {
                imageCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.transform.tag == "Player")
        {
            isReached = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
     public void Ready()
    {
        memoryPanel.SetActive(false);
        decisionPanel.SetActive(true);
    }
    public void Continue()
    {
       if (Shinigami.isCutscene)
       Shinigami.isCutscene = false;
        PlayerVariables.lives = 3;
        if(SceneManager.GetActiveScene().buildIndex !=5)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            if(PlayerVariables.totalAscensionPoints>PlayerVariables.totalDescensionPoints)
            {
                SceneManager.LoadScene(6);
            }
            else
            {
                SceneManager.LoadScene(7);
            }
        }
    }
    public void GuiltyPressed()
    {
        decisionPanel.SetActive(false);
        scorePanel.SetActive(true);
        if(trueDecision == TrueDecision.Guilty)
        {
            PlayerVariables.totalAscensionPoints += 50;
        }
        else
        {
            PlayerVariables.totalDescensionPoints += 50;
        }
       ascensionPoints.text="Ascension Points:" + PlayerVariables.totalAscensionPoints;
       descensionPoints.text="Descension Points:" + PlayerVariables.totalDescensionPoints;
    }
    public void InnocentPressed()
    {
        decisionPanel.SetActive(false);
        scorePanel.SetActive(true);
        if (trueDecision == TrueDecision.Innocent)
        {
            PlayerVariables.totalAscensionPoints += 50;
        }
        else
        {
            PlayerVariables.totalDescensionPoints += 50;
        }
        ascensionPoints.text = "Ascension Points:" + PlayerVariables.totalAscensionPoints;
        descensionPoints.text = "Descension Points:" + PlayerVariables.totalDescensionPoints;
    }
    public void MainMenuPressed()
    {
        PlayerVariables.lives = 3;
        PlayerVariables.totalAscensionPoints = 0;
        PlayerVariables.totalDescensionPoints = 0;
        SceneManager.LoadScene(0);
    }
    public void CameraChangePressed()
    {
        if(isCooldown==true)
        {

        }
        else
        {

            StartCoroutine(CameraEffect(5.0f));
            isCooldown = true;
        }

    }
    // suspend execution for waitTime seconds
    IEnumerator CameraEffect(float waitTime)
    {
        mainCamera.GetComponent<Camera>().orthographicSize = 20.0f;
        yield return new WaitForSeconds(waitTime);
        mainCamera.GetComponent<Camera>().orthographicSize = 8.0f;
    }
}
