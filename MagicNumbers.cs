namespace Clean_Code_3
{
    class Weapon
    {
        public const int BulletsPerShot = 1;

        private int _bullets;

        public bool CanShoot() => _bullets >= BulletsPerShot;

        public void Shoot() => _bullets -= BulletsPerShot;
    }
}
