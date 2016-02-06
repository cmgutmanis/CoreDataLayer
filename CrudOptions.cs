using reflectionmapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CoreData
{
    public class CrudOptions<TItem>: ICrudOptions<TItem>, IDisposable
    {
        private string connectionString { get; set; }
        private SqlConnection connection { get; set; }
        private TItem item { get; set; }
        private List<SqlParameter> sqlParams { get; set; }
        
        public CrudOptions(string connectionString, TItem item)
        {
            this.connectionString = connectionString;
            this.connection = new SqlConnection(connectionString);
            this.sqlParams = new List<SqlParameter>();
            this.item = item;
        }
        
        public TItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> GetAll()
        {
            throw new NotImplementedException();
        }

        // Quick and dirty - should pass exclusions into propertymapper and handle it once.
        public ICrudOptions<TItem> MapAllExcept(List<string> exclusions)
        {
            var mapped = PropertyMapper.GetParamsFromObject(item).ToList();
            foreach (var i in mapped)
            {
                if (!exclusions.Contains(i.ParameterName))
                {
                    this.sqlParams.Add(i);
                }
            }

            return this;
        }

        private void MapAll()
        {
            this.sqlParams = PropertyMapper.GetParamsFromObject(item).ToList();
        }
        
        /// <summary>
        /// Uses default sproc naming convention dbo.Add[itemtype]
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public void Add()   
        {
            if (this.sqlParams.Count() == 0)
            {
                this.MapAll();
            }
            var sprocCommand = string.Concat("dbo.Add", typeof(TItem).ToString());
            Add(sprocCommand);
        }

        public void Add(string sprocCommand)
        {
            connection.Open();
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddRange(this.sqlParams.ToArray());
            cmd.CommandText = sprocCommand;
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void Update()
        {
            var sprocCommand = string.Concat("dbo.Update", typeof(TItem).ToString());
            Update(sprocCommand);
        }

        public void Update(string sprocCommand)
        {
            connection.Open();
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddRange(this.sqlParams.ToArray());
            cmd.CommandText = sprocCommand;
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
