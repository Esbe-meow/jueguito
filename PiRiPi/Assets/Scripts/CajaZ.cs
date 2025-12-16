using UnityEngine;

public class CajaZ : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    [SerializeField] private Player player;
    [SerializeField] private GameObject caja;
    [SerializeField] private float zOffset=2;
    public bool nearBoxZ = false;

    void Update()
    {
        if (nearBoxZ && !player.cajeando && Input.GetKeyDown(KeyCode.E))
        {

            player.cajeando = true;
        }

        if (playerObj.transform.position.z > caja.transform.position.z && player.cajeando)
        {
            playerObj.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, zOffset);
        }
        else
        {
            playerObj.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, -zOffset);
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
