using System;
using System.Collections.Generic;


class Inventory
{

    Dictionary<int, int> _inventory = null;

    public Inventory() {

        _inventory = new Dictionary<int, int>(5);

        _inventory.Add((int)Decoration.ORANGE_BALL, 5);
        _inventory.Add((int)Decoration.TEDDY_BEAR, 1);


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

    ORANGE_BALL, TEDDY_BEAR, CANDY_CANE, STAR

}