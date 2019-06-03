using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class DataAccess
    {        
        public SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand cmd;

        public void CloseConection()
        {
            cnn.Close();
        }
        public SqlDataReader StoredProcedure_DataReader(string Nombre, List<SqlParameter> Parametros)
        {
            SqlDataReader dr = null;
            SqlDataReader drCoppy = null;
            cmd = new SqlCommand { Connection = cnn, CommandText = Nombre, CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddRange(Parametros.ToArray());
            try
            {
                cmd.Connection.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.HasRows)
                {
                    drCoppy = dr;
                }
                return drCoppy;
            }
            catch (SqlException ex) { throw ex; }            
        }
        public DataSet StoredProcedure_DataSet(string Nombre, List<SqlParameter> Parametros)
        {             
            cmd = new SqlCommand { Connection = cnn, CommandText = Nombre, CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddRange(Parametros.ToArray());            
            try
            {
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (SqlException ex) { throw ex; }            
        }
        public DataTable StoredPorcedure_DataTable(string Nombre, List<SqlParameter> Parametros)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand { Connection = cnn, CommandText = Nombre, CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddRange(Parametros.ToArray());
            try
            {
                cmd.Connection.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                return dt;
            }
            catch (SqlException ex) { throw ex; }           
        }
        public DataTable StoredPorcedure_DataTable(string Nombre)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand { Connection = cnn, CommandText = Nombre, CommandType = CommandType.StoredProcedure };
            try
            {
                cmd.Connection.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                return dt;
            }
            catch (SqlException ex) { throw ex; }
        }
        public SqlDataReader StoredProcedure_DataReader(string Nombre)
        {
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand { Connection = cnn, CommandText = Nombre, CommandType = CommandType.StoredProcedure };
            try
            {
                cmd.Connection.Open();
                dr = cmd.ExecuteReader();
                return dr;
            }
            catch (SqlException ex) { throw ex; }
        }      
        public void StoredProcedure_ExecuteQuery(string Nombre, List<SqlParameter> Parametros)
        {
            SqlCommand cmd = new SqlCommand { Connection = cnn, CommandText = Nombre, CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddRange(Parametros.ToArray());
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex) { throw ex; }
        }
        public object StoreProcedure_OutPutValue(string Nombre, List<SqlParameter> Parametros)
        {
            object result = new object();
            SqlCommand cmd = new SqlCommand { Connection = cnn, CommandText = Nombre, CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddRange(Parametros.ToArray());
            try
            {
                foreach (SqlParameter item in Parametros)
                {
                    if (item.Direction == ParameterDirection.Output)
                    {
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        result = cmd.Parameters[item.ParameterName].Value;
                    }
                }
                return result;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
