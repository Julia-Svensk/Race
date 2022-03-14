public class Bullet : MonoBehaviour
{
    public int bulletSpeed; //Hastigheten
    public int bulletDamage; //Vilken damage den ger till Obstacles.
    private Rigidbody rb;
     
    void Start()
    {
        //Vi kan använda GetComponent eftersom att Rigidbody och skriptet ligger på samma objekt.
        rb = GetComponent<Rigidbody>();
    }
    void LateUpdate() /// LateUpdate()
    {
        //Bullets ska åka iväg så fort dem instansieras.
        rb.velocity = Vector3.forward * bulletSpeed * Time.deltaTime;
        Destroy(gameObject, 3f);
        //De förstörs efter 3 sekunder om dem inte kolliderar med något objekt.
    }
 
    void OnCollisionEnter(Collision other)
    {
        //När Bullet kolliderar med Obstacles
        if(other.gameObject.tag == "Obstacles")
        {
           Destroy(gameObject);
         //Bullet förstörs när den kolliderar med ett Obstacle. 
        }
    }
}
