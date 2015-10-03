using reflectionmapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CoreData
{
    public class CrudOptions<TItem>: ICrudOptions<TItem>
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

        public CrudOptions<TItem> MapAllExcept(List<string> exclusions)
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

        /// <summary>
        /// Uses default sproc naming convention dbo.Add[itemtype]
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Add()   
        {
            var sprocCommand = string.Concat("dbo.Add", typeof(TItem).ToString());
            return Add(sprocCommand);
        }

        public int Add(string sprocCommand)
        {
            connection.Open();
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddRange(this.sqlParams.ToArray());
            cmd.CommandText = sprocCommand;
        //    var id = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.ExecuteNonQuery();

            
            return 0;
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
