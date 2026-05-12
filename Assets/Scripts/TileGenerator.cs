using UnityEngine;

public sealed class TileGenerator : MonoBehaviour
{
    [SerializeField] private float _gap;
    [SerializeField] private Vector3 _centerOffset;
    [SerializeField] private Vector2 _tileSize;
    [SerializeField] private Vector2Int _tileCount;
    [SerializeField] private GameObject _prefab;

    private void Awake()
    {
        GenerateTiles();
    }
    public void GenerateTiles()
    {
        if (_prefab == null) return;
        if (_tileCount.x <= 0 || _tileCount.y <= 0) return;

        float diameterX = (_tileSize.x + _gap) * (_tileCount.x - 1);
        float diameterY = (_tileSize.y + _gap) * (_tileCount.y - 1);
        Vector3 position;

        for (int i = 0; i < _tileCount.x; i++)
        {
            for (int j = 0; j < _tileCount.y; j++)
            {
                position = transform.position + _centerOffset;

                if (_tileCount.x == 1)
                    position.x += 0f;
                else
                {
                    float tX = i / (_tileCount.x - 1f);
                    Debug.Log(tX);
                    position.x += Mathf.Lerp(-diameterX / 2f, diameterX / 2f, tX);
                }

                if (_tileCount.y == 1)
                    position.z += 0f;
                else
                {
                    float tY = j / (_tileCount.y - 1f);
                    position.z += Mathf.Lerp(-diameterY / 2f, diameterY / 2f, tY);
                }

                Instantiate(_prefab, position, Quaternion.identity, transform).SetActive(true);
            }
        }
    }
}
