using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPanelController : MonoBehaviour
{
    // Start is called before the first frame update
    public void Canser()
    {
        Destroy(transform.parent.gameObject);
    }
}
