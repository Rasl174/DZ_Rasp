using System;

namespace Clean_code_8
{
    class Player
    {
        private string _name;
        private int _age;
    }

    class PlayerMovement
    {
        private float _directionX;
        private float _directionY;
        private float _speed;

        public void Move()
        {
            //Do move
        }
    }

    class Weapon
    {
        private float _cooldown;
        private int _damage;

        public void Attack()
        {
            //Attack
        }

        public bool IsReloading()
        {
            throw new NotImplementedException();
        }
    }
}
