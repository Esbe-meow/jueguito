using UnityEngine;

public class CajaZ : MonoBehaviour
{
    [SerializeField] private GameObject cat;
    [SerializeField] private Player player;
    [SerializeField] private GameObject caja;
    [SerializeField] private float zOffset;
    [SerializeField] private float timer;
    [SerializeField] private bool canExit;
    [SerializeField] private bool timerRunning;

    public bool nearBoxZ = false;

    void Update()
    {
        if (nearBoxZ && !player.cajeando && Input.GetKeyDown(KeyCode.E))
        {
            player.cajeando = true;
            timerRunning = true;
            if (cat.transform.position.z > caja.transform.position.z && player.cajeando)
            {
                cat.transform.position = new Vector3(cat.transform.position.x, cat.transform.position.y, caja.transform.position.z+zOffset);
                player.rb.constraints = RigidbodyConstraints.FreezePositionX;
            }
            else
            {
                cat.transform.position = new Vector3(cat.transform.position.x, cat.transform.position.y, caja.transform.position.z-zOffset);
                player.rb.constraints = RigidbodyConstraints.FreezePositionX;
            }
        }

        if (player.cajeando && canExit)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Jump") || !nearBoxZ){
                canExit = false;
                player.cajeando = false;
                player.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
        }

        if (timerRunning)
                timer+=Time.deltaTime;
                if (timer >= 0.5f)
                {
                    canExit = true;
                    timerRunning = false;
                    timer = 0;
                }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nearBoxZ = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            nearBoxZ = false;
        }
    }
}
