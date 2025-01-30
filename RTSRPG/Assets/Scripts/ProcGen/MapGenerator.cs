using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace ProcGen
{
    public class MapGenerator : MonoSingleton<MapGenerator>
    {
        public float regenerateInterval = 1;
        private float _regenerateTimer; 
        [Header("Dimensions")]
        public int width;
        public int height;
        [Header("Generators")]
        public TerrainGenerator terrain;
        public PropsGenerator props;
        [Header("Rules")]
        public bool generateOnStart = true;
        public bool generatePropsOnStart = true;
        public bool regenerateOnUpdate = true;
        
        [HideInInspector] public List<TerrainScript> terrains = new ();
        [HideInInspector] public List<Agent> agents = new ();
        private void Start()
        {
            if(generateOnStart) Generate();
        }

        public void Update()
        {
            Regenerate();
        }

        private void Regenerate()
        {
            _regenerateTimer += Time.deltaTime;
            if (_regenerateTimer < regenerateInterval) return;
            if(regenerateOnUpdate) props.Regenerate(_regenerateTimer, agents);
            _regenerateTimer = 0;
        }
        public void Generate()
        {
            var parent = new GameObject("parent");
            parent.transform.parent = transform;
            
            terrains = new();
            var map = terrain.Generate(parent, width, height);
            foreach (var terrainType in map.Values)
            {
                terrains.AddRange(terrainType);
            }

            if (generatePropsOnStart)
            {
                agents = props.Generate(map, parent);
            }
        }
        
        public TerrainType GetTerrain(Vector2 point) =>
            terrains
                .Find(go => Vector2.Distance(go.transform.position, point) < 0.5f)
                .GetComponent<TerrainScript>().category;
        
        public Rect Bounds => new Rect(0, 0, width, height);
        
        private void OnValidate()
        {
            width = Mathf.Max(1, width);
            height = Mathf.Max(1, height);
        }
    }
}