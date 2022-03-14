using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("UI is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public Image healthBar;
    public Text pointText;
    public Text timer;
    public GameObject destroysPanel;
    public GameObject startPanel;

    protected int points;

    public void Life(float damage)
    {
        healthBar.fillAmount -= damage * 0.1f;
    }

    public void Score(int point)
    {
        pointText.text = "Score : " + point;
    }

    public void Timer(float time)
    {
        timer.text = "Time Left: " + time;
    }

}


