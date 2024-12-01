using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScreens : MonoBehaviour
{
    public void DestroyChild(GameObject child)
    {
        Destroy(child);
    }
}
