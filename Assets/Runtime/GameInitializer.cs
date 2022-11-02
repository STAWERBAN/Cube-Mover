using System.Collections.Generic;

using UnityEngine;

using Runtime;
using Runtime.UI;
using Runtime.Cube;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private List<CubeView> _cubes;
    [SerializeField] private BackgroundView _backgroundView;
    [SerializeField] private UIView _view;
    [SerializeField] private Camera _camera;

    private MouseController _mouseController;
    private Game _game;
    private UIModel _model;

    private void Start()
    {
        InitializeGame();
        InitializeCubes();
    }

    private void InitializeUI()
    {
        _model = new UIModel();

        var controller = new UIController(_view, _model);
    }

    private void InitializeGame()
    {
        _game = new Game(_camera, _backgroundView, _model);
    }

    private void InitializeCubes()
    {
        foreach (var cube in _cubes)
        {
            var cubeModel = new CubeModel();
            var cubePath = new Path();
            var cubeController = new CubeController(cube, cubePath, cubeModel);
        }
    }
}
