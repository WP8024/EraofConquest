using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLevel : MonoBehaviour
{
    public GameObject Group;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManager.Instance.enemy.money == 10000)
        {
            DataManager.Instance.enemy.money -= 8000;
            Group.SetActive(true);
        }
    }
}
