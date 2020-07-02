using Smod2;

namespace BetterLock
{
    class Global
    {
        internal static bool can_use_commands;
        public static Plugin plugin;

        public static string _isnotdoor = "Ошибка: Вы не смотрите на дверь, либо находитесь слишком близко к ней.";
        public static string _successlock = "Дверь успешно заблокирована.";
        public static string _notitem = "Ошибка: Возьмите нужный предмет в руки.";
        public static string _notcurrentdoor = "Ошибка: эта дверь не может быть заблокирована.";


        public static string _alreadylock = "Ошибка: эта дверь уже заблокирована";

        public static float distance_to_lock = 2.5f;


        public static float cooldown = 300.0f;

        public static string _iscooldown = "Не получилось. Попробуйте через: ";

        public static float timeToUnlock = 15.0f;

        public static bool is_fullrp = false;
    }
}
