using System.Collections;
using System.Reflection;
using Assignment4.DatabaseConnection;
using Assignment4.Interface;
using Microsoft.Data.SqlClient;

namespace Assignment4
{
    public class MyORM<G, T> : IMyORM<G, T> where T : class, IEntity<G>, new()
    {
        private readonly string _connectionString;

        public MyORM(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Insert Method
        public void Insert(T item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        InsertRecursively(item, connection, transaction);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void InsertRecursively(object item, SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            Type type = item.GetType();
            string tableName = type.Name;
            PropertyInfo[] properties = type.GetProperties();

            List<string> columns = new List<string>();
            List<string> values = new List<string>();

            foreach (var property in properties)
            {
                if (IsSimpleType(property.PropertyType))
                {
                    if (property.PropertyType == typeof(Guid) && (Guid)property.GetValue(item) == Guid.Empty)
                    {
                        property.SetValue(item, Guid.NewGuid());
                    }
                    columns.Add(property.Name);
                    values.Add($"@{property.Name}");
                }
            }
            string sqlStatement = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)})";

            using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection, sqlTransaction))
            {
                foreach (var property in properties)
                {
                    if (IsSimpleType(property.PropertyType))
                    {
                        sqlCommand.Parameters.AddWithValue($"@{property.Name}", property.GetValue(item) != null ? property.GetValue(item) : DBNull.Value);
                    }
                }
                sqlCommand.ExecuteNonQuery();
            }

            foreach (var prop in properties)
            {
                if (typeof(IEnumerable<object>).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                {
                    var list = prop.GetValue(item) as IEnumerable<object>;
                    if (list != null)
                    {
                        foreach (var obj in list)
                        {
                            InsertRecursively(obj, sqlConnection, sqlTransaction);
                        }
                    }
                }
                else if (!IsSimpleType(prop.PropertyType) && prop.PropertyType != typeof(string))
                {
                    var nestedObject = prop.GetValue(item);
                    if (nestedObject != null)
                    {
                        InsertRecursively(nestedObject, sqlConnection, sqlTransaction);
                    }
                }
            }
        }
        #endregion


        //This table work for just parent table not work for foreign key
        public IList<T> GetAll()
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {typeof(T).Name}";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(MapReaderToEntity(reader));
                    }
                }
            }
            return result;
        }

        //This table work for just parent table not work for foreign key
        public T GetById(G id)
        {
            T result = new();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {typeof(T).Name} WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = MapReaderToEntity(reader);
                    }
                }
            }

            return result;
        }



        public void Update(T item)
        {
            throw new NotImplementedException();
        }


        public void Delete(T item)
        {
            throw new NotImplementedException();
        }


        public void Delete(G id)
        {
            throw new NotImplementedException ();
        }



        //Common helping Mehtods
        private bool IsSimpleType(Type type)
        {
            return
                type.IsPrimitive ||
                new Type[] {
                typeof(string),
                typeof(decimal),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }

        private T MapReaderToEntity(SqlDataReader reader)
        {
            var entity = new T();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (reader.HasColumn(property.Name))
                {
                    var value = reader[property.Name];
                    if (value != DBNull.Value)
                    {
                        property.SetValue(entity, value);
                    }
                }
            }
            return entity;
        }

    }
}