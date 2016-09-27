using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClick(Button button)
    {
        UnityMessageHelper.Instance.loadBaseMap();
    }
}
