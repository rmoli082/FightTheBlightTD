using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPanel : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.UpdateGemsDisplay();
    }
}
