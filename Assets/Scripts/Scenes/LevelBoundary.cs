using System;
using UnityEngine;
using Zenject;

namespace ClickerGame.Scenes
{
    public class LevelBoundary
    {
        readonly Camera _camera;
        readonly Settings _settings;

        public LevelBoundary([Inject(Id = "Main")] Camera camera,
                            Settings settings)
        {
            _camera = camera;
            _settings = settings;
        }

        public float ExtentHeight => _camera.orthographicSize;
        public float Height => ExtentHeight * 2.0f;

        public float ExtentWidth => _camera.aspect * _camera.orthographicSize;
        public float Width => ExtentWidth * 2.0f;

        public float Bottom => -ExtentHeight + _settings.Padding;
        public float Top => ExtentHeight - _settings.Padding;
        public float Left => -ExtentWidth + _settings.Padding;
        public float Right => ExtentWidth - _settings.Padding;


        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float Padding { get; private set; }
        }
    }
}