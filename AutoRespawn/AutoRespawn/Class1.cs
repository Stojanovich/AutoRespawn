using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using UnityEngine;
using System;
using System.Collections;

namespace AutoRespawn
{
    public class AutoRespawn : RocketPlugin<AutoRespawnConfiguration>
    {
        public static AutoRespawn Instance { get; private set; }
        private const string RESPAWN_MESSAGE = "You have been automatically respawned!";

        protected override void Load()
        {
            Instance = this;
            Rocket.Core.Logging.Logger.Log($"{Name} {Assembly.GetName().Version} has been loaded!");
            PlayerLife.onPlayerDied += OnPlayerDied;
        }

        protected override void Unload()
        {
            PlayerLife.onPlayerDied -= OnPlayerDied;
            Rocket.Core.Logging.Logger.Log($"{Name} has been unloaded!");
        }

        private void OnPlayerDied(PlayerLife sender, EDeathCause cause, ELimb limb, CSteamID instigator)
        {
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(sender.player);
            if (player == null) return;
            StartCoroutine(RespawnPlayer(player));
        }

        private IEnumerator RespawnPlayer(UnturnedPlayer player)
        {
            yield return new WaitForSeconds(Configuration.Instance.Delay);
            if (player != null && player.Player != null && !player.Dead)
            {
                yield break;
            }
            player.Player.life.ServerRespawn(false);
            Say(player, RESPAWN_MESSAGE);
        }

        public void Say(UnturnedPlayer player, string message)
        {
            if (string.IsNullOrEmpty(Configuration.Instance.MessageIcon))
            {
                UnturnedChat.Say(player, message, UnturnedChat.GetColorFromName(Configuration.Instance.MessageColor, Color.green));
            }
            else
            {
                ChatManager.serverSendMessage(message,
                    UnturnedChat.GetColorFromName(Configuration.Instance.MessageColor, Color.green),
                    null,
                    player.Player.channel.owner,
                    EChatMode.SAY,
                    Configuration.Instance.MessageIcon,
                    true);
            }
        }
    }

    public class AutoRespawnConfiguration : IRocketPluginConfiguration
    {
        public float Delay;
        public string MessageColor;
        public string MessageIcon;

        public void LoadDefaults()
        {
            Delay = 5f;
            MessageColor = "green";
            MessageIcon = "https://i.imgur.com/JZjQEHV.png";
        }
    }
}