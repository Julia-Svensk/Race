public class Obstacle : MonoBehaviour
{
    //Vilken damage Player får när den kolliderar med obstacle.
    public int damage = 1;
    public int health = 1;     //Obstacles health.
   //Vi vill att det ska visas en explosion effekt när vi har förstört objektet.
    public GameObject explosion;
    //Vi vill att explosion effekten ska visas bara en gång.
    private bool redanSkapat = false;
  void CreateAndDestroy()
    {
        //Om vi inte redan skapat en explosion effekt
        if (redanSkapat == false)
        {
            //Vi Instansierar ett GameObject (en explosion effekt)
            GameObject expo = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            //Det är en particle effect.
            //Vi vill att när explosion effekten instansieras blir direkt child till själva objektet så att när objektet förstörs, explosion effekten också förstörs.
            expo.transform.parent = gameObject.transform; 
            //redanSkapat = true eftersom vi precis skapade en effekt och vi behöver bara ha EN.
            redanSkapat = true;
        }
 
        Destroy(gameObject, 1.3f);  //Förstörs efter 1.3 sekunder.
    }
 
    void Update()
    {
        if (health <= 0)//Om Obstacle hälsan är mindre eller lika med Noll 
        {
            //Anropar CreateAndDestroy() metoden.
            //Först skapar en explosion effekt och sedan förstörs både effekten och själva objektet.
            CreateAndDestroy(); 
        }
    }
 
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Player förlorar hälsan när den kolliderar med ett obstacle.
           other.gameObject.GetComponent<Movement>().playerHealth -= (damage * 0.1f);
            //Players hälsan är 1 därför multiplicerar vi damage med 0.1f
        }
        if(other.gameObject.tag == "Bullet")
        {
            //Obstacle förlorar hälsan när en Bullet träffar en Obstacle.
           health -= other.gameObject.GetComponent<Bullet>().bulletDamage;
        }  
    }
}