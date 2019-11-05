using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using server.Models.Characters;
using server.Models.Interactions;
using server.Models.Jobs;
using server.Models.Players;

// https://stackoverflow.com/questions/44207280/deserialize-type-handled-json-using-newtonsoft-library-between-different-applica
// https://stackoverflow.com/questions/47535339/json-net-how-to-remove-assembly-info-from-types-in-the-resulting-json-string
namespace server.Util.Serialization
{
    public class DtoSerializationBinder : ISerializationBinder
    {
        public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Objects,
            SerializationBinder = new DtoSerializationBinder()
        };

        private readonly IList<Type> _knownTypes = new List<Type>
        {
            // Base types
            /*
            typeof(object),
            typeof(string),
            typeof(int),
            */

            // .Net derivatives
            typeof(Dictionary<string, object>),
            typeof(Dictionary<ushort, Dictionary<string, object>>),
            typeof(List<object>),

            // DTO's
            typeof(InteractionDto),
            typeof(JobMenuDataDto),
            typeof(JobUpgradeCategoryDto),
            typeof(JobUpgradeDto),
            typeof(PlayerDto),
            typeof(PlayerTeamsDto),
            typeof(List<PlayerTeamsDto>),
            typeof(FaceFeaturesDto)

        };

        public Type BindToType(string assemblyName, string typeName)
        {
            return _knownTypes.FirstOrDefault(t =>
            {
                var fullName = t.FullName
                    .ReplaceX(@", Version=\d+.\d+.\d+.\d+", string.Empty)
                    .ReplaceX(@", Culture=\w+", string.Empty)
                    .ReplaceX(@", PublicKeyToken=\w+", string.Empty);

                return fullName == typeName;
            });

            // TODO: Do not remove, might later work! (otherwise i'll kill you :P)
            /*
            switch (typeName)
            {
                case "shared.Enums.AdminLevel":
                    return typeof(AdminLevel);
                case "shared.Enums.Gender":
                    return typeof(Gender);
                case "shared.Enums.Objects":
                    return typeof(Objects);
                case "shared.Interaction.InteractionDto":
                    return typeof(InteractionDto);
                case "shared.Job.JobMenuDataDto":
                    return typeof(JobMenuDataDto);
                case "shared.Job.JobUpgradeCategoryDto":
                    return typeof(JobUpgradeCategoryDto);
                case "shared.Job.JobUpgradeDto":
                    return typeof(JobUpgradeDto);
                case "shared.Player.PlayerDto":
                    return typeof(PlayerDto);
            }

            return Type.GetType(typeName);
            */
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.FullName
                .ReplaceX(@", Version=\d+.\d+.\d+.\d+", string.Empty)
                .ReplaceX(@", Culture=\w+", string.Empty)
                .ReplaceX(@", PublicKeyToken=\w+", string.Empty);

            // TODO: Do not remove, might later work! (otherwise i'll kill you :P)
            // typeName = serializedType.AssemblyQualifiedName;
        }
    }
}
