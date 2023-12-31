using Microsoft.Extensions.Configuration;

namespace GrechkaBOT.Database
{
    public class DatabasePost : ConnectionDB
    {
        private static readonly string _selectQuery = $@"SELECT id_guild as {nameof(ModelGuild.guildId)}, name_guild as {nameof(ModelGuild.Name)}, lang as {nameof(ModelGuild.Leng)}, id as {nameof(ModelGuild.Id)} FROM guilds WHERE id_guild = @{nameof(ModelGuild.guildId)}";
        private static readonly string _selectRoom = @$"SELECT 
                channelowner as {nameof(ModelRooms.channel_owmer)},
                name as {nameof(ModelRooms.name)},
                limit_vc as {nameof(ModelRooms.limit)}
                FROM rooms WHERE channelowner = @{nameof(ModelRooms.channel_owmer)}";

        private static readonly string _selectTempRoom = @$"SELECT id as {nameof(ModelTempRoom.id)}, channel_room as {nameof(ModelTempRoom.channel_room)}, user_id as {nameof(ModelTempRoom.id_user)} FROM temp_rooms WHERE channel_room = @{nameof(ModelTempRoom.channel_room)}";

        private static readonly string _insertRole = $@"INSERT INTO roles 
        (
            id_guilds, 
            emoji, 
            id_massage,  
            id_channel, 
            role_name, 
            role_id
        ) values  
        (
            @{nameof(ModelRoles.guilds_id_KEY)},
            @{nameof(ModelRoles.setEmoji)},
            @{nameof(ModelRoles.messageId)},
            @{nameof(ModelRoles.channelId)},
            @{nameof(ModelRoles.roleName)},
            @{nameof(ModelRoles.roleId)}
        )";

        private static readonly string _insertLobby = $@"INSERT INTO roomers_lobbys 
        (
            id_guilds,
            id_lobby
        ) values 
        (
            @{nameof(ModelRoomsLobby.guild_key)},
            @{nameof(ModelRoomsLobby.lobby_id)}
        )";

        private static readonly string _insertroom = $@"INSERT INTO rooms 
        (
            channelowner, 
            name 
        ) values 
        (
            @{nameof(ModelRooms.channel_owmer)},
            @{nameof(ModelRooms.name)}
        )";

        private static readonly string _selectRole = $@"SELECT 
        emoji as {nameof(ModelRoles.setEmoji)},  
        id_massage as {nameof(ModelRoles.messageId)},
        id_channel as {nameof(ModelRoles.channelId)},
        role_name as {nameof(ModelRoles.roleName)},
        role_id as {nameof(ModelRoles.roleId)},
        id_guilds as {nameof(ModelRoles.guilds_id_KEY)}
        FROM roles WHERE id_massage = @{nameof(ModelRoles.messageId)} AND emoji = @{nameof(ModelRoles.setEmoji)}";


        private static readonly string _selectQueryLobby = $@"SELECT id_lobby as {nameof(ModelRoomsLobby.lobby_id)} FROM roomers_lobbys WHERE id_guilds = @{nameof(ModelRoomsLobby.guild_key)}";

        public DatabasePost(IServiceProvider service) : base(service)
        {
        }

        public static int insertGuild(object guilds)
        {
            string _insertQuery = $@"INSERT INTO guilds (name_guild, id_guild, lang) values (@{nameof(ModelGuild.Name)}, @{nameof(ModelGuild.guildId)}, @{nameof(ModelGuild.Leng)})";
            return Execute(_insertQuery, guilds);
            
        }

        public static int insertRoles(object roles)
        {
            return Execute(_insertRole, roles);
        }

        public static int insertLobby(object lobby) 
        {
            return Execute(_insertLobby, lobby);
        }

        public static int insertSettingRoom(object room) {  
            return Execute(_insertroom, room);
        }

        public static int insertTempRoom(object tempRoom) {
            string _temproominsert = $@"INSERT INTO temp_rooms (channel_room, user_id) VALUES (@{nameof(ModelTempRoom.channel_room)}, @{nameof(ModelTempRoom.id_user)})";
            return Execute(_temproominsert, tempRoom);
        }

        public static int updateRoomName(object updateName) {
            string _updateroom = $@"UPDATE rooms SET name = @{nameof(ModelRooms.name)} WHERE channelowner = @{nameof(ModelRooms.channel_owmer)}";
            return Execute(_updateroom, updateName);
        }
        public static int updateRoomLimit(object updateLimit) {
            string _updateroom = $@"UPDATE rooms SET limit_vc = @{nameof(ModelRooms.limit)} WHERE channelowner = @{nameof(ModelRooms.channel_owmer)}";
            return Execute(_updateroom, updateLimit);
        }

       
        public static int deleteRoles(object roles)
        {
            ModelRoles res = GetRole<ModelRoles>(roles);
            var delete = new ModelRoles
            {
                guilds_id_KEY = res.guilds_id_KEY,
                setEmoji = res.setEmoji,
                messageId = res.messageId
            };
            string _deleteQuery = $@"DELETE FROM roles WHERE emoji = @{nameof(ModelRoles.setEmoji)} AND id_guilds = @{nameof(ModelRoles.guilds_id_KEY)} AND id_massage = @{nameof(ModelRoles.messageId)}";
            
            return Execute(_deleteQuery, delete);
        }

        public static int deleteLobby(long lobbyId) 
        {
            var delete = new ModelRoomsLobby 
            {
                lobby_id = lobbyId
            };

            string _deleteQuery = $@"DELETE FROM roomers_lobbys WHERE id_lobby = @{nameof(ModelRoomsLobby.lobby_id)}";

            return Execute(_deleteQuery, delete);
        }

        public static int deleteTempRoom(long channelId){
            var delete = new ModelTempRoom{
                channel_room = channelId
            };

            string _deleteQuery = $@"DELETE FROM temp_rooms WHERE channel_room = @{nameof(ModelTempRoom.channel_room)}";
            return Execute(_deleteQuery, delete);
        }


        public static ModelGuild GetGuild<ModelGuild>(object guild)
        {
            var res = QueryFirstOrDefault<ModelGuild>(_selectQuery, guild);
            return res;
        }

        public static ModelRoomsLobby GetIdChannelLobby<ModelRoomsLobby>(object lobby) 
        {
            var res = QueryFirstOrDefault<ModelRoomsLobby>(_selectQueryLobby, lobby);
            return res;
        }

        public static ModelRoles GetRole<ModelRoles>(object role)
        {
            var res = QueryFirstOrDefault<ModelRoles>(_selectRole, role);
            return res;
        }

        public static ModelRooms GetRoom<ModelRooms>(object room) {
            var res = QueryFirstOrDefault<ModelRooms>(_selectRoom, room);
            return res;
        }

        public static ModelTempRoom GetTempRoom<ModelTempRoom>(object roomtemp) {
            var res = QueryFirstOrDefault<ModelTempRoom>(_selectTempRoom, roomtemp);
            return res;
        }
    }
}
