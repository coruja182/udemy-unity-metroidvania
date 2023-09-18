using UnityEditor;
using UnityEngine;

public class MapController : MonoBehaviour, Singleton
{
    [SerializeField] private GameObject[] m_maps;
    [field: SerializeField] public GameObject FullMapCamera { get; private set; }
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

    private void Start()
    {
        foreach (var mapIt in m_maps)
        {
            mapIt.SetActive(SaveManager.HasMap(mapIt.gameObject.name));
        }
    }

    public void ActivateMap(string mapToActivate)
    {
        GameObject map = ArrayUtility.Find(m_maps, mapIt => mapIt.name == mapToActivate);
        if (map)
        {
            map.SetActive(true);
            SaveManager.SaveMap(mapToActivate);
        }
    }

    public void DestroyThyself()
    {
        Destroy(gameObject);
        Instance = null;
    }
}
