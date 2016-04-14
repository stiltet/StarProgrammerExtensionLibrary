using System;
using StarProgrammerExtensionLibrary.Interfaces;

namespace StarProgrammerExtensionLibrary.Models
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}