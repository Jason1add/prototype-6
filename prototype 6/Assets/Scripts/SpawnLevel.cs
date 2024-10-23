using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    public GameObject block;
    int length = 16;
    float[] scale;
    float[] posOffset;

    float startPos = -5.0f;
    public float currPos;

    // Start is called before the first frame update
    void Start()
    {
        currPos = startPos;
        scale = new float[length];
        posOffset = new float[length];
        scale[0] = 10.0f;
        scale[1] = 10.0f;
        scale[2] = 10.0f;
        scale[3] = 10.0f;
        scale[4] = 9.0f;
        scale[5] = 8.0f;
        scale[6] = 7.0f;
        posOffset[0] = 6.0f;
        posOffset[1] = 5.0f;
        posOffset[2] = 4.0f;
        posOffset[3] = 3.0f;
        posOffset[4] = 3.0f;
        posOffset[5] = 3.0f;
        posOffset[6] = 3.0f;
        for (int i = 7; i < length; i++)
        {
            scale[i] = Random.Range(5.0f, 10.0f);
            posOffset[i] = Random.Range(2.0f, 6.0f);
        }
        for (int i = 0; i < length; i++)
        {
            currPos += posOffset[i];
            GameObject instance = Instantiate(block, new Vector3(currPos, -1.0f, 0.0f), Quaternion.Euler(0, 180, 0));
            if (i == 0)
            {
                instance.GetComponent<BlockColor>().m_PrevPosX = startPos;
            }
            else
            {
                instance.GetComponent<BlockColor>().m_PrevPosX = currPos - posOffset[i];
            }
            instance.GetComponent<BlockColor>().m_ScaleX = scale[i];

        }
    }

    // Update is called once per frame
    void Update()
    {


    }
}
