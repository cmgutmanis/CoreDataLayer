using reflectionmapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CoreData
{
    public class FitPowerCrudOptions<TItem>: ICrudOptions<TItem>
    {
        private string connectionString { get; set; }
        private SqlConnection connection { get; set; }
        
        public FitPowerCrudOptions(string connectionString)
        {
            this.connectionString = connectionString;
            this.connection = new SqlConnection(connectionString);
        }
        
        public TItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Add(TItem item)
        {
            connection.Open();
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var mapped = PropertyMapper.GetParamsFromObject(item);
            cmd.Parameters.Add(mapped);
            cmd.CommandText = string.Concat("dbo.Add", typeof(TItem).ToString());

            var id = Convert.ToInt32(cmd.ExecuteScalar());
            return id;
        }

        public void Update(TItem item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
