using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private PlayerController pCont;
    [SerializeField] private Material originalMat, glowMat;
    // Start is called before the first frame update
    void Start()
    {
        pCont = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pCont.selectedObj == this.gameObject.name)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = glowMat;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = originalMat;
        }
    }
}
