using System;
using System.Collections.Generic;
using UnityEngine;

class Inventory
{

    Dictionary<int, int> _inventory = null;

    private static int[] _indices = null;

    static Inventory() {

        //_inventory = new Dictionary<int, int>(5);

        //_inventory.Add((int)Decoration.ORANGE_BALL, 5);
        //_inventory.Add((int)Decoration.TEDDY_BEAR, 1);

        _indices = (int[]) Enum.GetValues(typeof(Decoration));
    }

    public static Projectile GetRandomProjectile(Vector3 position, Quaternion rotation) {

        
        int projPrefId = UnityEngine.Random.Range(0, _indices.Length);

        Projectile result;

        if (PoolsManager.GetObject(_indices[projPrefId], position, rotation).TryGetComponent<Projectile>(out result))
        {
            return result;
        }
        else {

            throw new Exception("Bad projectile");        
        }

        
    }


    //public bool GetProjectile(int id, out Projectile projectile) {

    //    int count;

    //    if (_inventory.TryGetValue(id, out count)) {

    //        _inventory.Remove(id);

    //        if (count > 0)
    //        {
    //            count--;
    //            _inventory.Add(id, count);

    //            //projectile = PoolsManager.GetObject(1, _startPoint.position, Quaternion.identity).GetComponent<Projectile>();

    //            return true;
    //        }
    //        else {

    //            projectile = null;
    //            return false;            
    //        }


    //    }
    
    //}

}

enum Decoration : int { 

    ORANGE_BALL, TEDDY_BEAR, CANDY_CANE, STAR, RED_BALL, SNOWMAN

}