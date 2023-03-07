using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainNavMeshBaker
{
    private static Dictionary<Terrain, TreeInstance[]> _terrainTrees = new Dictionary<Terrain, TreeInstance[]>();

    [MenuItem("Tools/Bake NavMesh")]
    public static void BakeNavMesh()
    {
        try
        {
            RemoveTrees();
            NavMeshBuilder.BuildNavMesh();
        }
        finally
        {
            RestoreTrees();
        }
    }

    private static void RestoreTrees()
    {
        foreach (var terrain in _terrainTrees)
        {
            terrain.Key.terrainData.treeInstances = terrain.Value;
        }
    }

    private static void RemoveTrees()
    {
        var terrains = new List<Terrain>();

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);

            if (!scene.isLoaded)
                continue;

            terrains.AddRange(scene.GetRootGameObjects().SelectMany(x => x.GetComponentsInChildren<Terrain>()));
        }

        _terrainTrees.Clear();
        foreach (var terrain in terrains)
        {
            _terrainTrees[terrain] = terrain.terrainData.treeInstances;
            terrain.terrainData.treeInstances = new TreeInstance[0];
        }
    }
}