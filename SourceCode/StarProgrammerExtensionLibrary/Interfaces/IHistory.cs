using System;

namespace StarProgrammerExtensionLibrary.Interfaces
{
    public interface IHistory : IEntity
    {
        Guid BaseObjectId { get; set; }
        DateTime HistoryDate { get; set; }
    }
}