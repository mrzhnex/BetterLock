using Smod2;
using Smod2.Attributes;
using Smod2.Config;

namespace BetterLock
{
    [PluginDetails(
        author = "Innocence",
        description = "description",
        id = "better.lock",
        name = "BetterLock",
        configPrefix = "bl",
        SmodMajor = 3,
        SmodMinor = 0,
        SmodRevision = 0,
        version = "1.3.B.B.2"
    )]

    public class MainSettings : Plugin
    {
        public override void Register()
        {
            AddEventHandlers(new SetEvents(this));
            AddConfig(new ConfigSetting("bl_cooldown", 300.0f, true, "This is a description"));
            AddConfig(new ConfigSetting("bl_timeToUnlock", 15.0f, true, "This is a description"));
            AddConfig(new ConfigSetting("bl_is_fullrp", "false", true, "This is a description"));
        }

        public override void OnEnable()
        {
            Info(Details.name + " on");
        }

        public override void OnDisable()
        {
            Info(Details.name + " off");
        }
    }
}
