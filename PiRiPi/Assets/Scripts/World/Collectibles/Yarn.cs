using Unity.VisualScripting;
using UnityEngine;
 //ola

public class Yarn : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.yarn++;
            Destroy(this.gameObject);
        }
    }
}
