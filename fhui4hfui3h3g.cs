﻿using Rocket.API;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;

namespace ItemRestrictorAdvanced
{

    public class CommandGetInventory : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "invsee";
        public string Help => "Shows you someone's inventory using UI that you can edit";
        public string Syntax => "/invsee or /ins";
        public List<string> Aliases => new List<string>() { "ins" };
        public List<string> Permissions => new List<string>() { "rocket.ins", "rocket.invsee", "rocket.invsee.edit", "rocket.ins.edit" };
        internal static CommandGetInventory Instance { get; private set; }

        public CommandGetInventory()
        {
            Instance = this;
        }

        public void Execute(IRocketPlayer fh579hf5hfh, string[] command)
        {
            try
            {
                UnturnedPlayer fhup5hpfuh349 = (UnturnedPlayer)fh579hf5hfh;
                EffectManager.sendUIEffect(8100, 22, fhup5hpfuh349.CSteamID, true);
                for (byte i = 0; i < Provider.clients.Count; i++)
                    EffectManager.sendUIEffectText(22, fhup5hpfuh349.CSteamID, true, $"text{i}", $"{Provider.clients[i].playerID.characterName}");
                EffectManager.sendUIEffectText(22, fhup5hpfuh349.CSteamID, true, $"page", "1");
                EffectManager.onEffectButtonClicked += new ManageUI((byte)System.Math.Ceiling(Provider.clients.Count / 24.0), fhup5hpfuh349.Player, fh579hf5hfh).gu1h3ca4pu34cdfjghnc4an;// feature
                EffectManager.sendUIEffectText(22, fhup5hpfuh349.CSteamID, true, "pagemax", $"{ManageUI.gu1h3a4pu34gdanasd}");
                ManageUI.gu1h3a4pu3asd4gdan.Add(fhup5hpfuh349.Player);
                fhup5hpfuh349.Player.serversideSetPluginModal(true);

                U.Events.OnPlayerConnected += new Refresh(fhup5hpfuh349.CSteamID).OnPlayersChange;
                U.Events.OnPlayerDisconnected += new Refresh(fhup5hpfuh349.CSteamID).OnPlayersChange;
            }
            catch (System.Exception e)
            {
                Rocket.Core.Logging.Logger.LogException(e, $"Exception in Invsee: caller: {fh579hf5hfh.DisplayName}");
                for (byte i = 0; i < Refresh.Refreshes.Length; i++)
                {
                    Refresh.Refreshes[i].TurnOff(i);
                }
            }

            //System.Console.WriteLine($"/gi executed");
        }
    }
}
//Effect ID is the id parameter, key is an optional instance identifier for modifying instances of an effect, 
//and child name is the unity name of a GameObject with a Text component.