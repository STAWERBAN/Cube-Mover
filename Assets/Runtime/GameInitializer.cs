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
    private UIModel _uiModel;
    private Game _game;

    private void Start()
    {
        InitializeUI();
        InitializeBackground();
        InitializeGame();
        InitializeCubes();
    }

    private void InitializeUI()
    {
        _uiModel = new UIModel();

        var controller = new UIController(_view, _uiModel);
        controller.Initialize();
    }

    private void InitializeBackground()
    {
        var controller = new BackgroundController(_uiModel, _backgroundView);
        controller.Initialize();
    }

    private void InitializeGame()
    {
        _game = new Game(_camera, _uiModel);
        _game.Initilize();
    }

    private void InitializeCubes()
    {
        foreach (var cube in _cubes)
        {
            var cubeModel = new CubeModel();
            var cubePath = new Path(cube.LineRenderer);
            var cubeController = new CubeController(cube, cubePath, cubeModel);

            _game.AddNewCube(cube, cubePath);

            cubeController.Initialize();
        }
    }
}