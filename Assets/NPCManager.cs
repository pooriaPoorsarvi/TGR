using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    public LevelFinisher levelFinisher;
    public int npcNumbers;
    public int originalNumber;
    public Text text;
    
    int getNumberOfChildren()
    {
        int res = 0;
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        foreach(Transform child in children)
        {
            if (child.parent == transform)
            {
                res += 1;
            }
        }
        return res;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        npcNumbers = getNumberOfChildren();
        originalNumber = npcNumbers;
    }

    // Update is called once per frame
    void Update()
    {
        npcNumbers = getNumberOfChildren();
        text.text = (originalNumber-npcNumbers) + "/" + originalNumber;
        if (npcNumbers == 0)
        {
            levelFinisher.FinishGame(true);
        }
    }
}
