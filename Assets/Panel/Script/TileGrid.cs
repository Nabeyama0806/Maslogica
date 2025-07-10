using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TileGrid : MonoBehaviour
{
    private const int GridSize = 7;     //�Ֆʂ̑傫��(���a)

    //�Ֆ�
    [SerializeField] private TileDate[] m_tile;
    private static TileDate[,] m_tileGrid = new TileDate[GridSize, GridSize];

    private void Start()
    {
        SetTileGrid();
    }

    private void SetTileGrid()
    {
        int index = 0;
        for (int i = 0; i < GridSize; ++i)
        {
            for (int j = 0; j < GridSize; ++j)
            {
                m_tileGrid[i, j] = m_tile[index];
                index++;
            }
        }
    }

    //�Ֆʍ��W�ɕϊ�
    public static Vector3 ToGridPos(Vector3 position)
    {
        int posX = Mathf.RoundToInt(position.x);
        int posZ = Mathf.RoundToInt(position.z);

        //�ՖʊO�Ȃ獶��ɏC��
        if (posX >= GridSize || posZ >= GridSize) return new Vector3();

        return new Vector3(posX, position.y, posZ);
    }

    //��Ԃ̔��]
    public static bool Flip(Vector2Int panelPos)
    {
        //�ՖʊO�Ȃ牽�����Ȃ�
        if (panelPos.x >= GridSize || panelPos.y >= GridSize) return false;

        //��Ԃ̔��]
        m_tileGrid[panelPos.x, panelPos.y].IsActive = !m_tileGrid[panelPos.x, panelPos.y].IsActive;

        return m_tileGrid[panelPos.x, panelPos.y].IsActive;
    }

    //�����_���ň�}�X�I��
    static public void RandomSelect()
    {
        int randX = Random.Range(0, GridSize);
        int randY = Random.Range(0, GridSize);

        m_tileGrid[randX, randY].EnemyAttack();
    }

    //�Ֆʂ̃��Z�b�g
    public static void PassiveAll()
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                //�S��OFF�̏��
                if (m_tileGrid[y, x].IsActive) m_tileGrid[y, x].Passive();
            }
        }
    }

    static public void PlayEffect()
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                //�A�N�e�B�u�̔Ֆʂ̓G�t�F�N�g��\��
                if (m_tileGrid[y, x].IsActive) m_tileGrid[y, x].PlayEffect(TileDate.EffectType.PlayerAttack);
            }
        }
    }
}
