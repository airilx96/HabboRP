using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plus.HabboHotel.Rooms;
using Plus.HabboHotel.GameClients;
using Plus.Communication.Packets.Outgoing.Rooms.Chat;
using Plus.Communication.Packets.Outgoing.Rooms.Notifications;
using Plus.Communication.Packets.Outgoing.Inventory.Purse;

namespace Plus.HabboHotel.Rooms.Chat.Commands.Moderators
{
    class PremiarCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "command_premiar"; }
        }

        public string Parameters
        {
            get { return "%username% %badge%"; }
        }

        public string Description
        {
            get { return "Faz todas as fun��es para finaliza um evento."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor, digite o usu�rio que deseja premiar!");
                return;
            }

            GameClient Target = PlusEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (Target == null)
            {
                Session.SendWhisper("Opps, n�o foi poss�vel encontrar esse usu�rio!");
                return;
            }

            RoomUser TargetUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Target.GetHabbo().Id);
            if (TargetUser == null)
            {
                Session.SendWhisper("Usu�rio n�o encontrado!");
                return;
            }

            if (Target.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("Voc� n�o pode se premiar!");
                return;
            }

            if (Params.Length != 3)
            {
                Session.SendWhisper("Por favor, indique o c�digo do emblema que gostaria de enviar!");
                return;
            }

            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (ThisUser == null)
            {
                return;
            }
            else
            {
                PlusEnvironment.GetGame().GetClientManager().SendPacket(new RoomNotificationComposer("rank", "message", "O usu�rio " + TargetUser.GetUsername() + " ganhou o evento!"));
                Target.GetHabbo().Credits = Target.GetHabbo().Credits += 500;
                Target.SendPacket(new CreditBalanceComposer(Target.GetHabbo().Credits));
                if (Session.GetHabbo().Id != Session.GetHabbo().Id)
                    Target.SendWhisper("Parab�ns, voc� ganhou um evento!");
                Session.SendWhisper("Concedido com sucesso " + 500 + " Creditos ao usu�rio " + Target.GetHabbo().Username + "!");
                Target.SendPacket(new RoomNotificationComposer("goldapple", "message", "Voc� ganhou " + 500 + " cr�ditos, parab�ns " + Target.GetHabbo().Username + "!"));

                Target.GetHabbo().Duckets += 500;
                Target.SendPacket(new HabboActivityPointNotificationComposer(Target.GetHabbo().Duckets, 500));
                if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                    Session.SendWhisper("Concedido com sucesso " + 500 + " Duckets ao usu�rio " + Target.GetHabbo().Username + "!");
                Target.SendPacket(new RoomNotificationComposer("coracao2", "message", "Voc� ganhou " + 500 + " duckets! parab�ns " + Target.GetHabbo().Username + "!"));

                Target.GetHabbo().Diamonds += 5;
                Target.SendPacket(new HabboActivityPointNotificationComposer(Target.GetHabbo().Diamonds, 5, 5));
                if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                    Session.SendWhisper("Concedido com sucesso " + 5 + " diamantes ao usu�rio " + Target.GetHabbo().Username + "!");

                Target.GetHabbo().GOTWPoints = Target.GetHabbo().GOTWPoints + 5;
                Target.SendPacket(new HabboActivityPointNotificationComposer(Target.GetHabbo().GOTWPoints, 5, 103));
                if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                    Session.SendWhisper("Concedido com sucesso " + 5 + " GOTW point(s) ao " + Target.GetHabbo().Username + "!");
                Target.SendPacket(new RoomNotificationComposer("control", "message", "Voc� ganhou " + 500 + " GOTW point(s)! parab�ns " + Target.GetHabbo().Username + "!"));

                if (!Target.GetHabbo().GetBadgeComponent().HasBadge(Params[2]))
                {
                    Target.GetHabbo().GetBadgeComponent().GiveBadge(Params[2], true, Target);
                    if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                        Target.SendPacket(new RoomNotificationComposer("game", "message", "Voc� acaba de receber um emblema game!"));
                }
                else
                    Session.SendWhisper("Opps, esse usu�rio j� possui este emblema (" + Params[2] + ") !");

                foreach (RoomUser RoomUser in Room.GetRoomUserManager().GetUserList().ToList())
                {
                    if (RoomUser == null || RoomUser.IsBot || RoomUser.GetClient() == null || RoomUser.GetClient().GetHabbo() == null || RoomUser.GetClient().GetHabbo().GetPermissions().HasRight("mod_tool") || RoomUser.GetClient().GetHabbo().Id == Session.GetHabbo().Id)
                        continue;

                    RoomUser.GetClient().SendNotification("Esse evento acaba de ser finalizado, at� o pr�ximo!");

                    Room.GetRoomUserManager().RemoveUserFromRoom(RoomUser.GetClient(), true, false);
                }
                Session.SendWhisper("Voc� deu com sucesso emblema " + Params[2] + "!");
                Session.SendWhisper("Todos os usu�rios foram kikados com sucesso.");
                Session.SendWhisper("Voc� acabou de finalizar um evento.");
            }
        }

        private void SendMessage(RoomNotificationComposer roomNotificationComposer)
        {
            throw new NotImplementedException();
        }
    }
}