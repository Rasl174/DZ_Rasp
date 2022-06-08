namespace CS_DZ_Rasp_1
{
    class Weapon
    {
        private readonly int _damage;
        private int _bullets;

        private void DoDamage()
        {
            _bullets--;
        }

        public bool TryAttackPlayer(Player player)
        {
            if (_bullets > 0 && _damage > 0)
            {
                if (player.CheckPlayerAlive())
                {
                    DoDamage();
                    player.TakeDamage(_damage);
                    return true;
                }
                return false;
            }
            return false;
        }
    }

    class Player
    {
        private int _health;

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }

        public bool CheckPlayerAlive()
        {
            if (_health < 0)
            {
                return false;
            }
            return true;
        }
    }

    class Bot
    {
        private Weapon _weapon;

        public void OnSeePlayer(Player player)
        {
            _weapon.TryAttackPlayer(player);
        }
    }
}
