using UnityEngine;

public class GridUI : MonoBehaviour
{
    [SerializeField] private Vector2 _xLims;
    [SerializeField] private Vector2 _yLims;
    [SerializeField] private float _spacing;
    [SerializeField] private float _minorLineWidth;
    [SerializeField] private float _majorLineWidth;
    [SerializeField] private Gradient _colorGradient;
    private int _nx;
    private int _ny;

    private void Start()
    {
        _nx = (int)(_xLims[1] / _spacing);
        _ny = (int)(_yLims[1] / _spacing);
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        GenerateLine(new Vector3(_xLims[0], 0f, 0.5f), new Vector3(_xLims[1], 0f, 0.5f), _majorLineWidth);
        GenerateLine(new Vector3(0f, _yLims[0], 0.5f), new Vector3(0f, _yLims[1], 0.5f), _majorLineWidth);
        for (int i = 1;  i < _nx; i++)
        {
            float xPos = i * _spacing;
            GenerateLine(new Vector3(xPos, _yLims[0], 0.5f), new Vector3(xPos, _yLims[1], 0.5f), _minorLineWidth);
            GenerateLine(new Vector3(-xPos, _yLims[0], 0.5f), new Vector3(-xPos, _yLims[1], 0.5f), _minorLineWidth);
        }
        for (int j = 1; j < _ny; j++)
        {
            float yCenter = j * _spacing;
            GenerateLine(new Vector3(_xLims[0], yCenter, 0.5f), new Vector3(_xLims[1], yCenter, 0.5f), _minorLineWidth);
            GenerateLine(new Vector3(_xLims[0], -yCenter, 0.5f), new Vector3(_xLims[1], -yCenter, 0.5f), _minorLineWidth);
        }
    }

    public void GenerateLine(Vector3 start, Vector3 end, float width)
    {
        GameObject go = new() { name = "GridLine" };
        go.AddComponent<LineRenderer>();
        LineRenderer lr = go.GetComponent<LineRenderer>();
        go.transform.SetParent(transform);
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply")); ;
        lr.colorGradient = _colorGradient;
        lr.SetPositions(new Vector3[] { start, end });
        lr.widthMultiplier = width;
    }
}
