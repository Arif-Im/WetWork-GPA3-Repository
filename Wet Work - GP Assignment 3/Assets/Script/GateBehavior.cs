using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GateBehavior : MonoBehaviour
{
    GeneratorHealth[] generators;
    float numberOfGeneratorsDisabled = 0;
    bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        generators = FindObjectsOfType<GeneratorHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfGeneratorsDisabled >= generators.Length)
        {
            isOpen = true;
            OpenGate();
            Debug.Log(numberOfGeneratorsDisabled);
        }
    }

    public void OpenGate()
    {
        //isOpen = true;
        if(gameObject.GetComponent<TilemapCollider2D>() != null)
        {
            gameObject.GetComponent<TilemapCollider2D>().enabled = false;
            gameObject.GetComponent<TilemapRenderer>().enabled = false;
        }
        else if(gameObject.GetComponent<BoxCollider2D>() != null)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void SetTotalNumberOfGeneratorsDisabled()
    {
        numberOfGeneratorsDisabled++;
    }

    public bool IsGateOpen()
    {
        return isOpen;
    }    
}
