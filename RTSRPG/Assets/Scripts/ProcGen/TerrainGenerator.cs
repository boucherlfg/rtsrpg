using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace ProcGen
{
    public class TerrainGenerator : MonoBehaviour
    {
        public float scale;
        public int octaves;
        [Range(0, 1)]
        public float persistance;
        public float lacunarity;
        public int seed;
        public Vector2 offset;

        public GameObject[] terrainType;
        public Props[] props;
        public GameObject wallRock;
        
        public Dictionary<TerrainType, List<TerrainScript>> Generate(GameObject parent, int width, int height)
        {
            var noiseMap = Noise.GenerateNoiseMap(width, height, seed, scale, octaves, persistance, lacunarity, offset);

            var map = new Dictionary<TerrainType, List<TerrainScript>>();
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var noise = noiseMap[x, y];
                    for (var i = 0; i < terrainType.Length; i++)
                    {
                        var prob = (i + 1f) / terrainType.Length;
                        if (noise > prob) continue;

                        var terrain = terrainType[i];
                        var type = terrain.GetComponent<TerrainScript>().category;
                        if (!map.ContainsKey(type)) map[type] = new List<TerrainScript>();

                        var instance = Instantiate(terrain, new Vector2(x, y), Quaternion.identity, parent.transform).GetComponent<TerrainScript>();
                        map[type].Add(instance);
                        break;
                    }
                }
            }

            for (var x = -1; x <= height; x++)
            {
                Instantiate(wallRock, new Vector2(x, -1), Quaternion.identity, parent.transform);
                Instantiate(wallRock, new Vector2(x, height), Quaternion.identity, parent.transform);
            }
            for (var y = 0; y < height; y++)
            {
                Instantiate(wallRock, new Vector2(-1, y), Quaternion.identity, parent.transform);
                Instantiate(wallRock, new Vector2(width, y), Quaternion.identity, parent.transform);
            }
            
            return map;
        }

        void OnValidate()
        {
            lacunarity = Mathf.Max(1, lacunarity);
            octaves = Mathf.Max(1, octaves);
        }
    }
}