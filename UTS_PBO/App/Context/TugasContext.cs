using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winform_mvc.App.Core;
using winform_mvc.App.Models;

namespace winform_mvc.App.Context
{
    internal class TugasContext : DatabaseWrapper
    {
        private static string table = "tugas";

        public static DataTable All()
        {
            string query = @"
        SELECT 
            t.id,
            t.judul,
            t.deskripsi,
            t.deadline,
            t.id_user,
            u.nama_user  
        FROM 
            tugas m
        JOIN 
            user u ON t.id_user = u.id";

            DataTable dataMahasiswa = queryExecutor(query);
            return dataMahasiswa;
        }

        public static DataTable getMahasiswaById(int id)
        {
            string query = @"
            SELECT 
                t.id,
                t.judul,
                t.deskripsi,
                t.deadline,
                t.id_user,
                u.nama_user  
            FROM 
                tugas m
            JOIN 
                user u ON t.id_user = u.id;
                WHERE 
                    m.id = @id";

            NpgsqlParameter[] parameters =
            {
                new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = id }
            };

            DataTable dataMahasiswa = queryExecutor(query, parameters);
            return dataMahasiswa;
        }


        public static void AddMahasiswa(M_Tugas mahasiswaBaru)
        {
            string query = $"INSERT INTO {table} (judul, deskripsi, deadline, id_user) VALUES(@judul, @deskripsi, @deadline, @id_user)";

            NpgsqlParameter[] parameters =
            {
                new NpgsqlParameter("@judul", mahasiswaBaru.judul),
                new NpgsqlParameter("@deskripsi", mahasiswaBaru.deskripsi),
                new NpgsqlParameter("@deadline", mahasiswaBaru.deadline),
                new NpgsqlParameter("@id_user", mahasiswaBaru.id_user)
            };

            commandExecutor(query, parameters);
        }

        public static void UpdateMahasiswa(M_Tugas mahasiswa)
        {
            string query = $"UPDATE {table} SET judul = @judul, deskripsi = @deskripsi, deadline = @deadline, id_user = @id_user WHERE id = @id";

            NpgsqlParameter[] parameters =
            {
                new NpgsqlParameter("@judul", mahasiswa.judul),
                new NpgsqlParameter("@deskripsi", mahasiswa.deskripsi),
                new NpgsqlParameter("@deadline", mahasiswa.deadline),
                new NpgsqlParameter("@id_user", mahasiswa.id_user)
            };

            commandExecutor(query, parameters);
        }

        public static void DeleteMahasiswa(int id)
        {
            string query = $"DELETE FROM {table} WHERE id = @id";

            NpgsqlParameter[] parameters =
            {
                new NpgsqlParameter("@id", id)
            };

            commandExecutor(query, parameters);
        }
    }
}
