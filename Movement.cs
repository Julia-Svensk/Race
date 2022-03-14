using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int forwardSpeed;//Med vilken hastighet ska bilen åka framåt
    public int sideSpeed; //Med vilken hastighet bilen svänger.
    private bool moving; //För att kolla om Player åker framåt eller inte.
    private bool isPaused;

    public float playerHealth; //Players hälsa

    //En Transform array.
    //Bullets instansieras ifrån de två positionerna.
    public Transform[] bulletStartPosition;
    //En GameObject array. Två Bullets ska Bilen skjuta 
    public GameObject[] bullets;
    private GameObject bulletsParent; //Ett Parent Objekt.

    // Update is called once per frame
    void Update()
    {
        //Vi behöver bara ha ett Parent Objekt.
        bulletsParent = GameObject.Find("BulletsParent");
        //Första gången hittas inget objekt som heter "BulletsParent".
        //Om det inte hittas så skapar vi ett objekt som SKA heta “BulletsParent”
        if (bulletsParent == false)
        {
            //Här skapas ett nytt GameObjekt med namnet "BulletsParent"
            bulletsParent = new GameObject("BulletsParent");
        }
      
        InputController();
 
        Shoot(); 

        Death();

    }	

    void InputController()
    {
        moving = false; //Player åker inte framåt när vi inte håller ner W.
      //Om vi antingen håller ner W ELLER håller ner UpArrow.
       if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
       {
            //För att bilen ska åka framåt
            transform.Translate(Vector3.up * forwardSpeed * Time.deltaTime);
            moving = true; //Det visar att vi åker framåt
            isPaused = true; //isPaused lika med true eftersom bilen åker
       }
       //OM vi inte åker framåt. Om vi inte håller ner W
       else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isPaused = false; //isPaused lika med false eftersom bilen inte åker
        }

       if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
       {  
           if (moving == true)//När Spelaren åker framåt så kan,
           {
                //Spelaren svänga åt vänster. (När den bara åker framåt).
                transform.Translate(-Vector3.left * sideSpeed * Time.deltaTime);
              
                //Spelaren roterar när den svänger åt vänster
                transform.rotation = Quaternion.Slerp(transform.rotation,  Quaternion.Euler(-90, 180, -angle), rotationSpeed * Time.deltaTime);
           }
       }
       if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
       {
           if (moving == true) //När Spelaren åker framåt så kan, 
           {
                //Spelaren svänga åt höger. (När den bara åker framåt)
                transform.Translate(Vector3.left * sideSpeed * Time.deltaTime);
 
                //Spelaren roterar när den svänger åt höger.
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-90, 180, angle), rotationSpeed * Time.deltaTime);
            }
        }

        if (isPaused == false)
        { //Panel som innehåller vårt Start & Quit buttons aktiveras
            UIManager.Instance.destroysPanel.SetActive(true);
        }
        else
        {//Panel som innehåller vårt Start & Quit buttons avaktiveras
            UIManager.Instance.destroysPanel.SetActive(false);
        }
    }

    void Shoot()
    {
        //Om man vänsterklickar
        if (Input.GetMouseButtonDown(0))
        {  
        //Instansieras två GameObjects (Två Bullets). 
        //bullets[0] första elementet av GameObject[] som instansieras från första elementet av Transform[] vilket är bulletStartPosition[0]
        GameObject left = Instantiate(bullets[0],     bulletStartPosition[0].position, bulletStartPosition[0].rotation) as GameObject;
        //bullets[1] andra elementet av GameObject[] som instansieras från andra elementet av Transform[] vilket är bulletStartPosition[1]
        GameObject right = Instantiate(bullets[1], bulletStartPosition[1].position, bulletStartPosition[1].rotation) as GameObject;
 
        //Blir det child direkt till bulletsParent så att vår hierarkin inte blir helt fullt med GameObjects.
        left.transform.parent = bulletsParent.transform;
        right.transform.parent = bulletsParent.transform;
        }
    } 

    void Death()
   {
       if (playerHealth <= 0 )
        {
           UIManager.Instance.startPanel.SetActive(true);
           UIManager.Instance.destroysPanel.SetActive(false);
           Time.timeScale = 0; //Stoppar spelet
        }
       else
        {
           Time.timeScale = 1; //Startar spelet
           UIManager.Instance.startPanel.SetActive(false);
        }
   }

   void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Movement>().playerHealth -= (damage * 0.1f); //(Vi har redan skrivit denna rad) 
 
            UIManager.Instance.Life(damage);
            //Visar players hälsa när den går ner
        }
    }

}
