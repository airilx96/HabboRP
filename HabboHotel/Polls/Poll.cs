using System.Collections.Generic;
using Plus.HabboHotel.Polls.Enums;

namespace Plus.HabboHotel.Polls
{
    /// <summary>
    ///     Class Poll.
    /// </summary>
    internal class Poll
    {
        /// <summary>
        ///     The answers negative
        /// </summary>
        internal int AnswersNegative;

        /// <summary>
        ///     The answers positive
        /// </summary>
        internal int AnswersPositive;

        /// <summary>
        ///     The identifier
        /// </summary>
        internal int Id;

        /// <summary>
        ///     The poll invitation
        /// </summary>
        internal string PollInvitation;

        /// <summary>
        ///     The poll name
        /// </summary>
        internal string PollName;

        /// <summary>
        ///     The prize
        /// </summary>
        internal string Prize;

        /// <summary>
        ///     The questions
        /// </summary>
        internal List<PollQuestion> Questions;

        /// <summary>
        ///     The room identifier
        /// </summary>
        internal int RoomId;

        /// <summary>
        ///     The thanks
        /// </summary>
        internal string Thanks;

        /// <summary>
        ///     The type
        /// </summary>
        internal PollType Type;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Poll" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="pollName">Name of the poll.</param>
        /// <param name="pollInvitation">The poll invitation.</param>
        /// <param name="thanks">The thanks.</param>
        /// <param name="prize">The prize.</param>
        /// <param name="type">The type.</param>
        /// <param name="questions">The questions.</param>
        internal Poll(int id, int roomId, string pollName, string pollInvitation, string thanks, string prize, int type, List<PollQuestion> questions)
        {
            Id = id;
            RoomId = roomId;
            PollName = pollName;
            PollInvitation = pollInvitation;
            Thanks = thanks;
            Type = (PollType) type;
            Prize = prize;
            Questions = questions;
            AnswersPositive = 0;
            AnswersNegative = 0;
        }
    }
}