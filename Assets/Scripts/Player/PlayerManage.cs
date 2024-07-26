using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManage : MonoBehaviour
{
    public static PlayerManage instance;
    public Player player;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

}
