using UnityEngine;

public class cajonMove : MonoBehaviour
{
    public float desplazamiento = 1.2f;
    public float vel = 1f;
    public Vector3 initPos;

    void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
        float movX = Mathf.PingPong(Time.time * vel, desplazamiento);
        transform.position = new Vector3(initPos.x + movX, initPos.y, initPos.z);
    }
}
