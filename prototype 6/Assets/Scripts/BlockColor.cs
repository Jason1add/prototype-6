using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockColor : MonoBehaviour
{
    SpriteRenderer m_Renderer;
    public float m_PrevPosX;
    public float m_ScaleX;
    Color m_NewColor;
    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(m_ScaleX, transform.localScale.y,
            transform.localScale.z);
        float scale = transform.localScale.x - 5.0f;
        float dist = transform.position.x - m_PrevPosX - 2.0f;
        float saturation = 0.25f + (0.15f * scale);
        float value = 1 - (0.15f * dist);
        //Debug.Log(scale + " " + dist + " " + saturation + " " + value);
        m_NewColor = Color.HSVToRGB(0.67f, saturation, value);
        GetComponent<SpriteRenderer>().color = new Color(m_NewColor.r, m_NewColor.g, m_NewColor.b);
        // Debug.Log("col" + m_NewColor.r + " " +


    }

    // Update is called once per frame
    void Update()
    {

    }
}
