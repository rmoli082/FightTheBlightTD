using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class ReferenceManager : Singleton<ReferenceManager>
{
    private List<AsyncOperationHandle> loadedGameObjects;

    protected override void Awake()
    {
        base.Awake();
        loadedGameObjects = new List<AsyncOperationHandle>();
    }

    public AsyncOperationHandle<GameObject> Instantiate(AssetReferenceGameObject key, Vector3 position, Quaternion rotation, Transform parent)
    {
        InstantiationParameters instParams = new InstantiationParameters(position, rotation, parent);

        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(key, instParams, true);

        loadedGameObjects.Add(handle);

        return handle;
    }

    public void ClearLoadedGameObjects()
    {
        foreach (AsyncOperationHandle handle in loadedGameObjects)
        {
            if (handle.Result != null)
            {
                Addressables.ReleaseInstance(handle);
            }
        }

        loadedGameObjects.Clear();
    }
}
