using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using UnityEngine;

namespace BetterLock
{
    internal class SetEvents : IEventHandler, IEventHandlerCallCommand, IEventHandlerWaitingForPlayers, IEventHandlerRoundStart
    {
        public SetEvents(MainSettings mainSettings)
        {
            Global.plugin = mainSettings;
        }

        public void OnCallCommand(PlayerCallCommandEvent ev)
        {
            if (!Global.can_use_commands)
            {
                ev.ReturnMessage = "Дождитесь начала раунда!";
                return;
            }
            string command = ev.Command.Split(new char[]
            {
                            ' '
            })[0].ToLower();

            if (command == "lock")
            {

                if (ev.Player.GetCurrentItem().ItemType != ItemType.CHAOS_INSURGENCY_DEVICE && ev.Player.GetCurrentItem().ItemType != ItemType.O5_LEVEL_KEYCARD)
                {
                    ev.ReturnMessage = Global._notitem;
                    return;
                }
                if ((ev.Player.GetGameObject() as GameObject).GetComponent<CooldownComponent>() != null)
                {
                    ev.ReturnMessage = Global._iscooldown + (ev.Player.GetGameObject() as GameObject).GetComponent<CooldownComponent>().cooldown.ToString();
                    return;
                }

                if (!Physics.Raycast(((ev.Player.GetGameObject() as GameObject).GetComponent<Scp049PlayerScript>().plyCam.transform.forward * 1.001f) + (ev.Player.GetGameObject() as GameObject).transform.position, (ev.Player.GetGameObject() as GameObject).GetComponent<Scp049PlayerScript>().plyCam.transform.forward, out RaycastHit hit, Global.distance_to_lock))
                {
                    ev.ReturnMessage = Global._isnotdoor;
                    return;
                }
                if (hit.transform.GetComponentInParent<Door>() == null)
                {
                    ev.ReturnMessage = Global._isnotdoor;
                    return;
                }

                if (Global.is_fullrp)
                {
                    if (hit.transform.GetComponentInParent<Door>().DoorName == "")
                    {
                        ev.ReturnMessage = Global._notcurrentdoor;
                        return;
                    }
                }

                if (hit.transform.GetComponentInParent<Door>().gameObject.GetComponent<DoorState>() == null && !hit.transform.GetComponentInParent<Door>().locked)
                {
                    (ev.Player.GetGameObject() as GameObject).AddComponent<CooldownComponent>();
                    hit.transform.GetComponentInParent<Door>().gameObject.AddComponent<DoorState>();
                    ev.ReturnMessage = Global._successlock;
                    return;
                }
                else
                {
                    ev.ReturnMessage = Global._alreadylock;
                    return;
                }
            }
        }

        public void OnRoundStart(RoundStartEvent ev)
        {
            Global.can_use_commands = true;
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            Global.can_use_commands = false;
            try
            {
                Global.is_fullrp = System.Convert.ToBoolean(Global.plugin.GetConfigString("bl_is_fullrp"));
                Global.plugin.Info("<bl_is_fullrp> set from config file: " + Global.is_fullrp.ToString());
            }
            catch (System.FormatException)
            {
                Global.is_fullrp = false;
                Global.plugin.Info("Failed convert <bl_is_fullrp> from config file. <bl_is_fullrp> set to default value: " + Global.is_fullrp.ToString());
            }
            try
            {
                Global.cooldown = System.Convert.ToSingle(Global.plugin.GetConfigString("bl_cooldown"));
                Global.plugin.Info("<bl_cooldown> set from config file: " + Global.cooldown.ToString());
            }
            catch (System.Exception)
            {
                Global.cooldown = 300.0f;
                Global.plugin.Info("Failed convert <bl_cooldown> from config file. <bl_cooldown> set to default value: " + Global.cooldown.ToString());
            }

            try
            {
                Global.timeToUnlock = System.Convert.ToSingle(Global.plugin.GetConfigString("bl_timeToUnlock"));
                Global.plugin.Info("<bl_timeToUnlock> set from config file: " + Global.timeToUnlock.ToString());
            }
            catch (System.Exception)
            {
                Global.timeToUnlock = 15.0f;
                Global.plugin.Info("Failed convert <bl_timeToUnlock> from config file. <bl_timeToUnlock> set to default value: " + Global.timeToUnlock.ToString());
            }
        }
    }
}