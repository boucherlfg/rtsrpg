using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data;
using States;
using UnityEngine;

namespace Core
{
    public class Visuals
    {
        private readonly Dictionary<int, List<(string message, Vector2 position)>> _floatingTextQueues = new();
        private readonly Dictionary<int, List<(Sprite image, Vector2 position)>> _floatingImageQueues = new();
        private VisualConfig _config;
        private bool _running;
        public void Initialize(VisualConfig config)
        {
            _config = config;
            _running = true;
            _ = FloatingTextProcess();
        }

        public void Uninitialize()
        {
            _running = false;
        }

        private async Task FloatingTextProcess()
        {
            while (_running)
            {
                var delay = (int)(1000 * _config.floatingTextDelay); // in milliseconds
                await Task.Delay(delay);

                foreach (var kvp in from kvp in _floatingTextQueues where kvp.Value.Count > 0 let first = kvp.Value[0] select kvp)
                {
                    if (kvp.Value.Count < 1) continue;
                    var text = kvp.Value[0];
                    kvp.Value.RemoveAt(0);
                    var instance = Object.Instantiate(_config.FloatingText, text.position + Random.insideUnitCircle * 0.5f, Quaternion.identity);
                    instance.Text = text.message;
                    
                }

                foreach (var kvp in _floatingImageQueues)
                {
                    if (kvp.Value.Count < 1) continue;
                    var image = kvp.Value[0];
                    kvp.Value.RemoveAt(0);
                    var instance = Object.Instantiate(_config.FloatingImage, image.position + Random.insideUnitCircle * 0.5f, Quaternion.identity);
                    instance.Image = image.image;
                }
            }
        }

        public void SpawnDestroyParticle(Vector2 position)
        {
            Object.Instantiate(_config.DestroyParticle, position, Quaternion.identity);
        }
        public void SpawnFloatingText(string message, AgentState source)
        {
            _floatingTextQueues.TryAdd(source.id, new List<(string, Vector2)>());
            _floatingTextQueues[source.id].Add((message, ((IPosition)source).Position));
        }

        public void SpawnFloatingImage(Sprite image, AgentState source)
        {
            _floatingImageQueues.TryAdd(source.id, new List<(Sprite, Vector2)>());
            _floatingImageQueues[source.id].Add((image, ((IPosition)source).Position));
        }
    }
}