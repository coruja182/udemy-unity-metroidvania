using UnityEngine;

public class MapActivator : MonoBehaviour
{
    // Usually we will name the Scene equal to the map
    [SerializeField] private string m_mapToActivate;

    // Start is called before the first frame update
    void Start()
    {
        MapController.Instance.ActivateMap(m_mapToActivate);
    }

}
