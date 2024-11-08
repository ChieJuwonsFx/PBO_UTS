using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS_PBO.App.Core;
using UTS_PBO.App.Models;

namespace UTS_PBO.App.Context
{
    internal class UserContext : DatabaseWrapper
    {
        private static string table = "user";

        public static DataTable All()
        {
            string query = $"SELECT * FROM {table}";
            DataTable dataProdi = queryExecutor(query);
            return dataProdi;
        }

        public static void AddProdi(M_User newProdi)
        {
            string query = $"INSERT INTO {table} (nama) values (@nama)";
            NpgsqlParameter[] parameters =
            {
                new NpgsqlParameter("@nama", NpgsqlDbType.Varchar){Value = newProdi.nama},
            };
            commandExecutor(query, parameters);
        }

        public static void UpdateProdi(M_User editProdi)
        {
            string query = $"UPDATE {table} SET nama = @nama WHERE id = @id";
            NpgsqlParameter[] parameters =
            {
                new NpgsqlParameter("@nama", NpgsqlDbType.Varchar){Value = editProdi.nama},
                new NpgsqlParameter("@id", NpgsqlDbType.Integer){Value = editProdi.id}
            };
            commandExecutor(query, parameters);
        }

        public static void DeleteProdi(int id)
        {
            string query = $"DELETE FROM {table} WHERE id = @id";
            NpgsqlParameter[] parameters =
            {
                new NpgsqlParameter("@id", NpgsqlDbType.Integer){Value = id},
            };
            commandExecutor(query, parameters);
        }
    }
}
