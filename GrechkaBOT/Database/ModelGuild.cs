﻿
namespace GrechkaBOT.Database
{
    public class ModelGuild
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long guildId { get; set; }
        public string Leng { get; set; }

        public virtual ModelRoles Roles { get; set; }
    }
}
