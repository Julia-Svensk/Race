public class Camera : MonoBehaviour
{
    public Transform target; //Target kommer vara spelaren.
    public Vector3 height; //Avståndet mellan Camera och spelaren.
 
    void LateUpdate()
    { //LateUpdate()
        transform.position = target.position;
        transform.position -= Vector3.forward + height;
        //Camera ska alltid vara bakom player.
    }
}
