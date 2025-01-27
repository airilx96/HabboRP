﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Plus.HabboHotel.Rooms;
using Plus.HabboHotel.Users;
using Plus.HabboHotel.Cache;

namespace Plus.Communication.Packets.Outgoing.Rooms.Settings
{
    class GetRoomBannedUsersComposer : ServerPacket
    {
        public GetRoomBannedUsersComposer(Room Instance)
            : base(ServerPacketHeader.GetRoomBannedUsersMessageComposer)
        {
            base.WriteInteger(Instance.Id);

            base.WriteInteger(Instance.BannedUsers().Count);//Count
            foreach (int Id in Instance.BannedUsers().ToList())
            {
                using (UserCache Data = PlusEnvironment.GetGame().GetCacheManager().GenerateUser(Id))
                {

                    if (Data == null)
                    {
                        base.WriteInteger(0);
                        base.WriteString("Erro desconhecido");
                    }
                    else
                    {
                        base.WriteInteger(Data.Id);
                        base.WriteString(Data.Username);
                    }
                }
            }
        }
    }
}
