using UnityEngine;
using System.Collections.Generic;

class ObjectPool
{
    private List<PooledObject> _objects = null;
    public Transform Root { get; private set; } = null;

    public ObjectPool(int count, PooledObject sample, Transform parent) {

        _objects = new List<PooledObject>();
        Root = parent;

        for (int i = 0; i < count; i++)
        {
            AddObject(sample, Root);
        }
    }

    private void AddObject(PooledObject sample, Transform parent) {

        GameObject temp = GameObject.Instantiate(sample.gameObject, parent);
        temp.name = sample.name;
        _objects.Add(temp.GetComponent<PooledObject>());
       
        if (temp.TryGetComponent<Animator>(out Animator sampleAnimator)) {

            sampleAnimator.StartPlayback();
        }

        temp.SetActive(false);
    }

    public PooledObject GetObject() {

        for (int i = 0; i < _objects.Count; i++) {

            if (_objects[i].gameObject.activeInHierarchy == false) {

                _objects[i].gameObject.SetActive(true);
                return _objects[i];
            }
        }

        AddObject(_objects[0], Root);
        return _objects[_objects.Count - 1];
    }
   
}

