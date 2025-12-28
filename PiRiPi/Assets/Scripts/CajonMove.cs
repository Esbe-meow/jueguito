using UnityEngine;

public class cajonMove : MonoBehaviour
{
    [SerializeField] private float desplazamiento = 1.2f;
    [SerializeField] private float vel = 1f;
    [SerializeField] private Vector3 initPos;

    void Start()
    {
        initPos = transform.position;
        vel = Random.Range(0.5f, 5);
        desplazamiento = Random.Range(0.5f, 7);
    }

    void Update()
    {
        float movX = Mathf.PingPong(Time.time * vel, desplazamiento);
        transform.position = new Vector3(initPos.x + movX, initPos.y, initPos.z);
    }
}
