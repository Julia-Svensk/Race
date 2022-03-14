using UnityEngine;

public class Environment : MonoBehaviour
{
    //En transform variabeln för andra environment.
    public Transform otherEnvironment;
    public float halfLength = 104.5f;
    //Längden av en Environment är 209, hälften av det blir det 104.5f;
   //Vi vill veta var Player är därför behöver vi ha player’s transform
    private Transform player;
    private float offset = 110f;
   //Offset ger lite avstånd till player och environment.
 
   void Start()
   { //För att kunna ta Player’s transform component
     player = GameObject.FindGameObjectWithTag("Player").transform;
   }
 
  void Update()
  {
    //Om Player har passerat den banan som precis åkt på.
    //Player åker alltid framåt och därför jämför vi z-axeln.
    if (transform.position.z + halfLength   < player.transform.position.z - offset )
    {
        //Environmenten som bilen precis åkt på och passerat, flyttar framför  den environment som nu bilen är på.
        transform.position = new Vector3(otherEnvironment.position.x, otherEnvironment.position.y, otherEnvironment.position.z + halfLength * 2);
        //(helftLength * 2)För att den ska flyttas exakt framför den andra
    }
  }
}
