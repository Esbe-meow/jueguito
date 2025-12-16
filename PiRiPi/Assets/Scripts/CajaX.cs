using UnityEngine;

public class CajaX : MonoBehaviour
{
    [SerializeField] private GameObject cat;
    [SerializeField] private Player player;
    [SerializeField] private GameObject caja;
    [SerializeField] private Rigidbody cajaRB;
    [SerializeField] private float xOffset;
    [SerializeField] private float timer;
    [SerializeField] private bool canExit;
    [SerializeField] private bool timerRunning;

    public bool nearBoxX = false;

    void Update()
    {
        stateManaging();
    }

    void stateManaging()
    {
                if (nearBoxX && !player.cajeando && Input.GetKeyDown(KeyCode.E))
        {
            player.cajeando = true;
            timerRunning = true;
            if (cat.transform.position.x > caja.transform.position.x && player.cajeando)
            {
                cat.transform.position = new Vector3(caja.transform.position.x + xOffset, cat.transform.position.y, cat.transform.position.z);
                player.rb.constraints = RigidbodyConstraints.FreezePositionZ;
                cajaRB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                //caja.transform.SetParent(cat.transform, true);
            }
            else
            {
                cat.transform.position = new Vector3(caja.transform.position.x - xOffset, cat.transform.position.y, cat.transform.position.z);
                player.rb.constraints = RigidbodyConstraints.FreezePositionZ;
                cajaRB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                //caja.transform.SetParent(cat.transform, true);
            }
        }

        if (player.cajeando && canExit)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Jump") || !nearBoxX){
                canExit = false;
                caja.transform.SetParent(cat.transform, false);
                player.cajeando = false;
                player.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                //cajaRB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                
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
            nearBoxX = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            nearBoxX = false;
        }
    }
}
