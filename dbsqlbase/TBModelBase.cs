
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbsqlbase
{
    public abstract class TBModelBase
    {
        private string columnsWithValues = "";
        [Flags]
        public enum Privilege
        {
            NONE = 0,
            Admin = 1 << 0,
            SuperUser = 1 << 1,
            User = 1 << 2,

        }
        public string ColumnsWithValues
        {
            get
            {
                columnsWithValues = "";
                int parCount = 1;
                this.GetType().GetProperties().ToList().ForEach(p =>
                {

                    MDTBProperty MDTBProperty = (MDTBProperty)Attribute.GetCustomAttribute(p, typeof(MDTBProperty));
                    if (MDTBProperty != null && MDTBProperty.IsdbReadWrite)
                    {
                        columnsWithValues += p.Name + "=";
                        columnsWithValues += $"@param{ parCount}" + ",";
                        parCount++;
                    }
                });
                try
                {

                    if (columnsWithValues.ToCharArray().ToList().Last() == ',')
                        columnsWithValues = columnsWithValues.Remove(columnsWithValues.Length - 1, 1);


                    return $" {columnsWithValues} ";
                }
                catch {
                    return $" {columnsWithValues} ";
                }
            }
        }
        private string columnsNames;

        public string ColumnsNames
        {
            get
            {
                columnsNames = "";

                this.GetType().GetProperties().ToList().ForEach(p =>
                {
                    MDTBProperty MDTBProperty = (MDTBProperty)Attribute.GetCustomAttribute(p, typeof(MDTBProperty));
                    if (MDTBProperty != null && MDTBProperty.IsdbReadWrite)
                    {
                        columnsNames += p.Name + ",";
                    }
                });
                try
                {
                    columnsNames = $"({ columnsNames.Remove(columnsNames.Length - 1, 1)})";


                    return $" {columnsNames} ";
                }
                catch
                {
                    return $" {columnsNames} ";
                }

             


            }

        }

        private string values;

        public string Values
        {
            get
            {
                values = "";
                SqlParameters = new List<SqlParameter>();
                int parCount = 1;
                this.GetType().GetProperties().ToList().ForEach(p =>
                {
                    MDTBProperty MDTBProperty = (MDTBProperty)Attribute.GetCustomAttribute(p, typeof(MDTBProperty));

                    if (MDTBProperty != null && MDTBProperty.IsdbReadWrite)
                    {
                        values += $"@param{parCount}" + ",";

                        if (p.PropertyType == typeof(int))
                        {
                            SqlParameter sqlParameter = new SqlParameter($"@param{parCount}", System.Data.SqlDbType.Int);
                            if (p.GetValue(this) == null)
                                sqlParameter.Value = 0;
                            else
                                sqlParameter.Value = (int)p.GetValue(this);
                            SqlParameters.Add(sqlParameter);
                        }
                        if (p.PropertyType == typeof(bool))
                        {
                            SqlParameter sqlParameter = new SqlParameter($"@param{parCount}", System.Data.SqlDbType.Bit);
                            sqlParameter.Value = (bool)p.GetValue(this);
                            SqlParameters.Add(sqlParameter);
                        }
                        if (p.PropertyType == typeof(byte))
                        {
                            SqlParameter sqlParameter = new SqlParameter($"@param{parCount}", System.Data.SqlDbType.NVarChar);
                            sqlParameter.Value = (string)p.GetValue(this);
                            SqlParameters.Add(sqlParameter);
                        }
                        else if (p.PropertyType == typeof(double))
                        {
                            SqlParameter sqlParameter = new SqlParameter($"@param{parCount}", System.Data.SqlDbType.Float);
                            if (p.GetValue(this) == null)
                                sqlParameter.Value = 0;
                            else
                                sqlParameter.Value = (float)p.GetValue(this);
                            SqlParameters.Add(sqlParameter);
                        }
                        else if (p.PropertyType == typeof(string))
                        {
                            SqlParameter sqlParameter = new SqlParameter($"@param{parCount}", System.Data.SqlDbType.NVarChar);
                            if (p.GetValue(this) == null)
                                sqlParameter.Value = "";
                            else
                                sqlParameter.Value = p.GetValue(this).ToString();
                            SqlParameters.Add(sqlParameter);
                        }
                        else if (p.PropertyType == typeof(DateTime?))
                        {
                            SqlParameter sqlParameter = new SqlParameter($"@param{parCount}", System.Data.SqlDbType.DateTime);
                            if (p.GetValue(this) == null)
                                sqlParameter.Value =DBNull.Value;
                            else
                                sqlParameter.Value = (DateTime)p.GetValue(this);
                            SqlParameters.Add(sqlParameter);
                        }



                        parCount++;
                    }
                });
                try
                {
                    values = $"({ values.Remove(values.Length - 1, 1)})";

                    return values;
                }
                catch
                {
                    return values;
                }
            }

        }

        public List<SqlParameter> SqlParameters { get; set; } = new List<SqlParameter>();

        private string queryToAdd;

        public string QueryToAdd
        {
            get
            {

                queryToAdd = "";
                this.GetType().GetProperties().ToList().ForEach(p =>
                {
                    MDTBProperty MDTBProperty = (MDTBProperty)Attribute.GetCustomAttribute(p, typeof(MDTBProperty));
                    if (MDTBProperty != null)
                    {
                        columnsNames += p.Name + ",";
                        values += p.GetValue(this)?.ToString() + ",";
                    }
                });

                queryToAdd = $"INSERT INTO {GetTableName()}  {columnsNames}  VALUES  {values} ";

                return queryToAdd;
            }

        }
        [JsonProperty]
        [MDTBProperty(IsdbReadWrite = false)]
        public int Id { get; set; }

        public TBModelBase()
        {
            ConnectionString= ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString;
        }

        public void Map(SqlDataReader sqlDataReader)
        {
            this.GetType().GetProperties().ToList().ForEach(p =>
            {
                
                MDTBProperty MDTBProperty = (MDTBProperty)Attribute.GetCustomAttribute(p, typeof(MDTBProperty));
                
                if (MDTBProperty != null)
                {
                    if (p.PropertyType == typeof(int))
                    {
                        p.SetValue(this, Convert.ToInt32(sqlDataReader[p.Name]));
                    }
                    else if (p.PropertyType == typeof(double))
                    {
                        p.SetValue(this, Convert.ToDouble(sqlDataReader[p.Name]));
                    }
                    else if (p.PropertyType == typeof(bool))
                    {
                        p.SetValue(this, Convert.ToBoolean(sqlDataReader[p.Name]));
                    }
                    else if (p.PropertyType == typeof(string))
                    {
                        p.SetValue(this, sqlDataReader[p.Name].ToString());
                    }
                    else if (p.PropertyType == typeof(byte))
                    {
                        p.SetValue(this, sqlDataReader[p.Name].ToString());
                    }
                    else if (p.PropertyType == typeof(DateTime?))
                    {
                        if (sqlDataReader[p.Name] == DBNull.Value) {
                            p.SetValue(this, null);
                         }
                        else
                        {
                            p.SetValue(this, Convert.ToDateTime(sqlDataReader[p.Name]));
                        }
                    }
               }
            });

        }

        public abstract string GetTableName();

        public static string ConnectionString { get; set; }

        public T GetById<T>(out string msg) where T : TBModelBase, new()
        {
            return GetByParameter<T>("Id", Id.ToString(), out msg).First();
        }
        public List<T> GetAll<T>(string orderby,out string returnMsg) where T : TBModelBase, new()
        {
            if (string.IsNullOrEmpty(TBModelBase.ConnectionString))
                throw new Exception("Set the connection string first!");

            SqlConnection connection = new SqlConnection(TBModelBase.ConnectionString);
            SqlCommand command = null;
            List<T> lst = new List<T>();
            returnMsg = "Success";
            connection.Open();
            command = new SqlCommand($"SELECT * FROM {GetTableName()} order by {orderby}; ", connection);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        T ent = new T();
                        ent.Map(reader);
                        lst.Add(ent);
                    }
                }
                else
                    throw new Exception($"Couldn't find entity");
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
            }
            command.Dispose();
            connection.Close();
            return lst;
        }

        public List<T> GetAll<T>(int offset, int nextrowscount, string orderby, out string returnMsg) where T : TBModelBase, new()
        {
            if (string.IsNullOrEmpty(TBModelBase.ConnectionString))
                throw new Exception("Set the connection string first!");

            SqlConnection connection = new SqlConnection(TBModelBase.ConnectionString);
            SqlCommand command = null;
            List<T> lst = new List<T>();
            returnMsg = "Success";
            connection.Open();

            //SELECT * FROM   {GetTableName()} order by Id  OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY;
            command = new SqlCommand($"SELECT * FROM   {GetTableName()} order by {orderby}  OFFSET {offset} ROWS FETCH NEXT {nextrowscount} ROWS ONLY;", connection);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        T ent = new T();
                        ent.Map(reader);
                        lst.Add(ent);
                    }
                }
                else
                    throw new Exception($"Couldn't find entity");
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
            }
            command.Dispose();
            connection.Close();
            return lst;
        }
        public List<T>GetByQuery<T>(string query, out string returnMsg) where T:TBModelBase,new()
        {
            SqlConnection connection = new SqlConnection(TBModelBase.ConnectionString);
            SqlCommand command = null;
            List<T> lst = new List<T>();
            returnMsg = "Success";
            connection.Open();
            command = new SqlCommand(query, connection);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        T ent = new T();
                        ent.Map(reader);
                        lst.Add(ent);
                    }
                }
                else
                    throw new Exception($"Couldn't find entity");
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
            }
            command.Dispose();
            connection.Close();
            return lst;
        }
        public List<T> GetByParameter<T>(string parName, string parValue, out string returnMsg) where T : TBModelBase, new()
        {
            SqlConnection connection = new SqlConnection(TBModelBase.ConnectionString);
            SqlCommand command = null;
            List<T> lst = new List<T>();
            returnMsg = "Success";
            connection.Open();
            command = new SqlCommand($"SELECT * FROM {GetTableName()} where {parName}='{parValue}'; ", connection);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        T ent = new T();
                        ent.Map(reader);
                        lst.Add(ent);
                    }
                }
                else
                    throw new Exception($"Couldn't find entity");
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
            }
            command.Dispose();
            connection.Close();
            return lst;
        }


        public List<T> GetByParameter<T>(string parname, string parvalue,int offset,int nextrowscount,string orderby, out string returnMsg) where T : TBModelBase, new()
        {
            SqlConnection connection = new SqlConnection(TBModelBase.ConnectionString);
            SqlCommand command = null;
            List<T> lst = new List<T>();
            returnMsg = "Success";
            connection.Open();
            command = new SqlCommand($"SELECT * FROM {GetTableName()} where {parname}='{parvalue}' order by {orderby}  OFFSET {offset} ROWS FETCH NEXT {nextrowscount} ROWS ONLY; ", connection);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        T ent = new T();
                        ent.Map(reader);
                        lst.Add(ent);
                    }
                }
                else
                    throw new Exception($"Couldn't find entity");
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
            }
            command.Dispose();
            connection.Close();
            return lst;
        }
        public bool Delete()
        {
            SqlConnection connection = new SqlConnection(TBModelBase.ConnectionString);
            SqlCommand cmd = null;

            connection.Open();
            string query = $"DELETE FROM {GetTableName()} WHERE Id={Id} ";

            Console.WriteLine(query);

            try
            {
                cmd = new SqlCommand(query, connection);
                cmd.CommandType = CommandType.Text;
                int res = cmd.ExecuteNonQuery();
                cmd.Dispose();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                connection.Close();
                return false;
            }
           

        }

        public T Add<T>(out string returnMsg) where T : TBModelBase, new()
        {
            SqlConnection connection = new SqlConnection(TBModelBase.ConnectionString);
            SqlCommand cmd = null;
            List<T> lst = new List<T>();
            returnMsg = "Success";
            connection.Open();
            string query = $"INSERT INTO {this.GetTableName()} {this.ColumnsNames} VALUES ";

            query += this.Values;

            if (query.ToCharArray().ToList().Last() == ',')
                query = query.Remove(query.Length - 1, 1);

            query += " SELECT @@Identity";
            Console.WriteLine(query);
            object res = null;
            try
            {
                cmd = new SqlCommand(query, connection);
                cmd.CommandType = CommandType.Text;
                foreach (SqlParameter par in this.SqlParameters)
                    cmd.Parameters.AddWithValue(par.ParameterName, par.SqlDbType).Value = par.Value;

                res = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
            }
            cmd.Dispose();
            connection.Close();
            T addedItem = null;
            if (res != null)
            {
                int addedId = Convert.ToInt32(res);
                addedItem = this.GetByParameter<T>("Id", addedId.ToString(), out string ss).First();
            }
            return addedItem;
        }

        public void Update<T>(out string returnMsg) where T : TBModelBase, new()
        {
            SqlConnection connection = new SqlConnection(TBModelBase.ConnectionString);
            SqlCommand cmd = null;
            List<T> lst = new List<T>();
            returnMsg = "Success";
            connection.Open();
            string query = $"UPDATE {this.GetTableName()} SET {this.ColumnsWithValues}  WHERE Id={this.Id} ;";

            if (query.ToCharArray().ToList().Last() == ',')
                query = query.Remove(query.Length - 1, 1);

            Console.WriteLine(query);

            try
            {
                string values = this.Values;
                cmd = new SqlCommand(query, connection);
                cmd.CommandType = CommandType.Text;
                foreach (SqlParameter par in this.SqlParameters)
                    cmd.Parameters.AddWithValue(par.ParameterName, par.SqlDbType).Value = par.Value;
                int res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
            }
            cmd.Dispose();
            connection.Close();

        }


        //{"employees":[
        //{ "firstName":"John", "lastName":"Doe" },
        //{ "firstName":"Anna", "lastName":"Smith" },
        //{ "firstName":"Peter", "lastName":"Jones" }
        //]}


        public string ToJson()
        {
            string js = "{";

            this.GetType().GetProperties().ToList().ForEach(p =>
            {
                JsonProperty MDTBProperty = (JsonProperty)Attribute.GetCustomAttribute(p, typeof(JsonProperty));
                if (MDTBProperty != null)
                {
                    Type type = p.PropertyType;
                    if (p.PropertyType == typeof(int))
                    {
                        js += $@"""{p.Name}"":""{p.GetValue(this).ToString()}"",";
                    }
                    else if (p.PropertyType == typeof(double))
                    {
                        js += $@"""{p.Name}"":""{p.GetValue(this).ToString()}"",";
                    }
                    else if (p.PropertyType == typeof(string))
                    { 
                        js += $@"""{p.Name}"":""{p.GetValue(this)}"",";
                    }
                    else if (p.PropertyType == typeof(DateTime))
                    {
                        DateTime dateTime = (DateTime)p.GetValue(this); 
                        js += $@"""{p.Name}"":""{dateTime.ToString("dd/MM/yyyy")}"",";
                    }
                    else if (typeof(IEnumerable<TBModelBase>).IsAssignableFrom(p.PropertyType))
                    {
                        js += $@"""{p.Name}"":[";

                        List<TBModelBase> tBModelBases = (p.GetValue(this) as IEnumerable<TBModelBase>).ToList();

                        if (tBModelBases != null)
                        {
                            tBModelBases.ToList().ForEach(t =>
                          {
                              js += $"{t.ToJson()},";
                          });
                            js = js.Remove(js.Length - 1, 1);
                            js += "],";
                        }
                    }
                }
            });
            js = js.Remove(js.Length - 1, 1);
            js += "}";
            return js;
        }

    }
}
