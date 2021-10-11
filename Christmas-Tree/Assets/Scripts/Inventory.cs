using System;
using System.Collections.Generic;
using UnityEngine;

static class Inventory
{

    private static Dictionary<Decoration, int> _inventory = null;

    private static List<int> _indices = null;

    static Inventory() {


        _indices = new List<int>((int[]) Enum.GetValues(typeof(Decoration)));
        _inventory = new Dictionary<Decoration, int>();
        
        _inventory.Add(Decoration.TEDDY_BEAR, 1);
        _inventory.Add(Decoration.BELL, 1);
        _inventory.Add(Decoration.CANDY_CANE, 2);
        _inventory.Add(Decoration.ORANGE_BALL, 2);
        _inventory.Add(Decoration.RED_BALL, 2);
        _inventory.Add(Decoration.SNOWMAN, 1);
        _inventory.Add(Decoration.SOCK, 1);
        _inventory.Add(Decoration.STAR, 1);
        _inventory.Add(Decoration.PRESENT_BLUE, 1);
        _inventory.Add(Decoration.PRESENT_WHITE, 1);

    }

    private static bool IsEmpty() {

        return _inventory.Count == 0 || _indices.Count == 0;
    }

    public static Projectile GetProjectile(Vector3 position, Quaternion rotation)
    {
        if (IsEmpty()) return null;

        int projPrefId = _indices[UnityEngine.Random.Range(0, _indices.Count)];

        int count = 0;
        Projectile result = null;

        _inventory.TryGetValue((Decoration)projPrefId, out count);

        while (count <= 0) {

            _inventory.Remove((Decoration)projPrefId);
            _indices.Remove(projPrefId);

            if (IsEmpty()) return null;

            projPrefId = _indices[UnityEngine.Random.Range(0, _indices.Count)];          
            _inventory.TryGetValue((Decoration)projPrefId, out count);
        }


        PoolsManager.GetObject(projPrefId, position, rotation).TryGetComponent<Projectile>(out result);
        _inventory[(Decoration)projPrefId] = --count;


        if (result != null)
        {
            return result;
        }
        else {

            throw new Exception("Bad projectile");

        }
       


    }



    //public static Projectile GetRandomProjectile(Vector3 position, Quaternion rotation) {

        
    //    int projPrefId = UnityEngine.Random.Range(0, _indices.Count);

    //    Projectile result;

    //    if (PoolsManager.GetObject(_indices[projPrefId], position, rotation).TryGetComponent<Projectile>(out result))
    //    {
    //        return result;
    //    }
    //    else {

    //        throw new Exception("Bad projectile");        
    //    }

        
    //}


  

}

enum Decoration : int { 

    ORANGE_BALL, TEDDY_BEAR, CANDY_CANE, STAR, RED_BALL, SNOWMAN, SOCK, BELL, PRESENT_BLUE, PRESENT_WHITE

}