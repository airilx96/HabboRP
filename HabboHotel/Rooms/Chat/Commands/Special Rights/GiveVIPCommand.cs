﻿using Plus.Communication.Packets.Outgoing.Rooms.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plus.HabboHotel.Rooms.Chat.Commands.SpecialRights
{
    class GiveVIPCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "command_give_vip"; }
        }

        public string Parameters
        {
            get { return "%usuário%"; }
        }

        public string Description
        {
            get { return "Dá ao usuário VIP."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length != 2)
            {
                Session.SendWhisper("Você deve digitar o nome de usuário da pessoa para a qual você deseja dar VIP.", 1);
                return;
            }

            var TargetClient = PlusEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);

            if (TargetClient == null)
            {
                Session.SendWhisper("Este usuário não foi encontrado! Talvez ele esteja offline.", 1);
                return;
            }

            if (TargetClient.GetHabbo() == null || TargetClient.GetRoomUser() == null)
            {
                Session.SendWhisper("Este usuário não foi encontrado! Talvez ele esteja offline.", 1);
                return;
            }

            if (TargetClient.GetHabbo().VIPRank > 0)
            {
                Session.SendWhisper("Lamentamos, mas este utilizador já possui VIP!", 1);
                return;
            }

            int cAmount = 1000;
            int dAmount = 5;


            TargetClient.GetHabbo().Credits += cAmount;
            TargetClient.GetHabbo().UpdateCreditsBalance();

            TargetClient.GetHabbo().Diamonds += dAmount;
            TargetClient.GetHabbo().UpdateDiamondsBalance();

            TargetClient.GetHabbo().VIPRank = 1;
            TargetClient.GetHabbo().Colour = "#0000FF";
            TargetClient.SendNotification("Você acabou de receber VIP por " + Session.GetHabbo().Username);
            TargetClient.GetHabbo().GetPermissions().Init(TargetClient.GetHabbo());

            using (var dbClient = PlusEnvironment.GetDatabaseManager().GetQueryReactor())
                dbClient.RunQuery("UPDATE `users` SET `rank_vip` = '1', `colour` = '0000FF' WHERE `id` = '" + TargetClient.GetHabbo().Id + "'");

            Session.SendWhisper("*Você virou VIP, " + TargetClient.GetHabbo().Username + "*", 1);
        }
    }
}
