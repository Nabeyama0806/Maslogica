using Unity.VisualScripting;
using UnityEngine;

public class RoadCreate : MonoBehaviour
{
    [SerializeField] GameObject RoadPrefab;   
    [SerializeField] float m_roadWidth = 12; 
    
    private float zPosOffset;             
    private bool m_isCreatingRoad;        

    void Start()
    {
        //ê∂ê¨â¬î\Ç…Ç∑ÇÈ
        m_isCreatingRoad = false;

        //ìπÇÃí∑Ç≥ï™ÇæÇØê∂ê¨à íuÇÇ∏ÇÁÇ∑
        zPosOffset = m_roadWidth;
    }

    void Update()
    {
        //ê∂ê¨çœÇ›Ç»ÇÁâΩÇ‡ÇµÇ»Ç¢
        if (m_isCreatingRoad) return;

        //ê∂ê¨
        Instantiate(RoadPrefab, new Vector3(0, 0, zPosOffset), Quaternion.identity);
        zPosOffset += m_roadWidth;

        //ê∂ê¨çœÇ›Ç…Ç∑ÇÈ
        m_isCreatingRoad = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Road"))
        {
            Destroy(other.gameObject);
            m_isCreatingRoad = false;
        }
    }
}
