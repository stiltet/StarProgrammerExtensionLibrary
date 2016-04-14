using System;
using StarProgrammerExtensionLibrary.Interfaces;

namespace StarProgrammerExtensionLibrary.Models
{
    public class UserHistory : User, IHistory
    {
        public Guid UserId { get; set; }

        Guid IHistory.BaseObjectId
        {
            get { return UserId; }
            set { UserId = value; }
        }

        public DateTime HistoryDate { get; set; }
    }
}