﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Plus.HabboHotel.Items;
using Plus.HabboHotel.Rooms;
using Plus.HabboHotel.Groups;
using Plus.HabboHotel.Users;

using Plus.Utilities;

namespace Plus.Communication.Packets.Outgoing.Rooms.Engine
{
    class ObjectAddComposer : ServerPacket
    {
        public ObjectAddComposer(Item Item, Room Room)
            : base(ServerPacketHeader.ObjectAddMessageComposer)
        {
            base.WriteInteger(Item.Id);
            base.WriteInteger(Item.GetBaseItem().SpriteId);
            base.WriteInteger(Item.GetX);
            base.WriteInteger(Item.GetY);
            base.WriteInteger(Item.Rotation);
           base.WriteString(String.Format("{0:0.00}", TextHandling.GetString(Item.GetZ)));
           base.WriteString(String.Empty);

            if (Item.LimitedNo > 0)
            {
                base.WriteInteger(1);
                base.WriteInteger(256);
               base.WriteString(Item.ExtraData);
                base.WriteInteger(Item.LimitedNo);
                base.WriteInteger(Item.LimitedTot);
            }
            else
            {
                ItemBehaviourUtility.GenerateExtradata(Item, this);
            }

            base.WriteInteger(-1); // to-do: check
            base.WriteInteger((Item.GetBaseItem().Modes > 1) ? 2 : 0);
            base.WriteInteger(Item.UserID);
           base.WriteString(Item.Username);
        }
    }
}