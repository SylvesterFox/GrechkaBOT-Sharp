﻿
namespace GrechkaBOT.Database
{
    public class ModelRoles
    {

        public string setEmoji {  get; set; }
        public long roleId { get; set; }
        public string roleName { get; set; }
        public long messageId { get; set; }
        public long channelId { get; set; }
        public int guilds_id_KEY { get; set; }

    }
}
