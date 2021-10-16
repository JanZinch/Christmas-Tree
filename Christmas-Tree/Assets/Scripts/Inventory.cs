using System;
using System.Collections.Generic;
using UnityEngine;

static class Inventory
{

    private static Dictionary<Decoration, int> _inventory = null;

    private static List<int> _indices = null;

    private const int StartPosition = 1;

    static Inventory() {

        Fill();
        GameManager.OnDestroyScene += delegate () { Fill(); };
    }

    private static void Fill() {

        _indices = new List<int>((int[])Enum.GetValues(typeof(Decoration)));
        _inventory = new Dictionary<Decoration, int>();

        _inventory.Add(Decoration.BLUE_PRESENT, 1);
        _inventory.Add(Decoration.ORANGE_BALL, 4);
        _inventory.Add(Decoration.STAR, 1);

        /* _inventory.Add(Decoration.TEDDY_BEAR, 1);
         _inventory.Add(Decoration.BELL, 3);
         _inventory.Add(Decoration.CANDY_CANE, 4);        
         _inventory.Add(Decoration.RED_BALL, 4);
         _inventory.Add(Decoration.SNOWMAN, 3);
         _inventory.Add(Decoration.SOCK, 4);                
         _inventory.Add(Decoration.WHITE_PRESENT, 1);*/

    }


    private static bool IsEmpty() {

        return _inventory.Count == 0 || _indices.Count == 0;
    }

    private static bool HasOnlyStar() {

        return _inventory.Count == StartPosition || _indices.Count == StartPosition;

    }

    private static bool IsMassive(Decoration decoration) {

        return decoration == Decoration.BLUE_PRESENT || decoration == Decoration.WHITE_PRESENT || decoration == Decoration.TEDDY_BEAR;
    }


    public static Projectile GetProjectile(Vector3 position, Quaternion rotation)
    {
        if (IsEmpty()) return null;

        int count = 0;
        Projectile result = null;
        int projPrefId;

        projPrefId = _indices[UnityEngine.Random.Range(StartPosition, _indices.Count)];
        _inventory.TryGetValue((Decoration)projPrefId, out count);

        while (count <= 0)
        {
            _inventory.Remove((Decoration)projPrefId);
            _indices.Remove(projPrefId);

            if (HasOnlyStar())
            {

                projPrefId = (int)Decoration.STAR;
                _inventory.TryGetValue((Decoration)projPrefId, out count);

                _indices.Clear();
                _inventory.Clear();

                break;

            }
            else
            {

                projPrefId = _indices[UnityEngine.Random.Range(StartPosition, _indices.Count)];
                _inventory.TryGetValue((Decoration)projPrefId, out count);

            }


        }








        PoolsManager.GetObject(projPrefId, position, rotation).TryGetComponent<Projectile>(out result);
        _inventory[(Decoration)projPrefId] = --count;


        if (result != null)
        {
            if (IsMassive((Decoration)projPrefId)) {

                result.IsMassive = true;
            }


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

    STAR,
    TEDDY_BEAR,
    BLUE_PRESENT, 
    WHITE_PRESENT,
    BELL,
    CANDY_CANE,
    ORANGE_BALL,
    RED_BALL, 
    SNOWMAN, 
    SOCK   

}