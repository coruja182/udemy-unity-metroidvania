using UnityEngine;

public class MapController : MonoBehaviour, Singleton
{
    [SerializeField] private GameObject[] m_maps;
    public static MapController Instance { get; set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActivateMap(string mapToActivate)
    {
        foreach (GameObject mapIt in m_maps)
        {
            mapIt.SetActive(mapIt.name == mapToActivate);
        }
    }

    public void DestroyThyself()
    {
        Destroy(gameObject);
        Instance = null;
    }
}
