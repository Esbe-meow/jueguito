using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int yarn;
    public int collectibles;
    public float[] checkpoint;

    public PlayerData (Player player)
    {
        yarn = player.yarn;
        collectibles = player.collectionable;

        checkpoint = new float[3];
        checkpoint[0] = player.spawnpoint.x;
        checkpoint[1] = player.spawnpoint.y;
        checkpoint[2] = player.spawnpoint.z;
    }
}
