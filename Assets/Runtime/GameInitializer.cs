using System.Collections.Generic;
using UnityEngine;
using Runtime;
using Runtime.Background;
using Runtime.UI;
using Runtime.Cube;
using UnityEngine.EventSystems;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private List<CubeView> _cubes;
    [SerializeField] private BackgroundView _backgroundView;
    [SerializeField] private FinishView _finishView;
    [SerializeField] private UIView _view;
    [SerializeField] private Camera _camera;
    [SerializeField] private EventSystem _eventSystem;

    private List<IUpdatable> _updatables = new List<IUpdatable>();

    private Game _game;
    private UIModel _uiModel;
    private MouseController _mouseController;

    private void Start()
    {
        InitializeMouseController();
        InitializeUI();
        InitializeBackground();
        InitializeGame();
        InitializeCubes();
    }

    private void Update()
    {
        foreach (var updatable in _updatables)
        {
            updatable.Update();
        }
    }

    private void InitializeMouseController()
    {
        _mouseController = new MouseController(_camera, _eventSystem);
        _updatables.Add(_mouseController);
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
        _game = new Game(_uiModel, _finishView, _mouseController);
        _game.Initilize();
    }

    private void InitializeCubes()
    {
        foreach (var cube in _cubes)
        {
            var cubeModel = new CubeModel();
            var cubePath = new Path(cube.LineRenderer, cube.PathColor, cube.Position());
            var cubeController = new CubeController(cube, cubePath, cubeModel);

            _updatables.Add(cubeController);

            _game.AddNewCube(cube, cubeModel);

            cubeController.Initialize();
        }
    }
}