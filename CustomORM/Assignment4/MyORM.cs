using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using Assignment4.DatabaseSchemaClass;
using Assignment4;
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

        public void Insert(T item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                var columnNames = new StringBuilder();
                var values = new StringBuilder();

                foreach (var prop in typeof(T).GetProperties())
                {
                    var propValue = prop.GetValue(item);
                    if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string) || prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(double))
                    {
                        columnNames.Append($"{prop.Name}, ");
                        values.Append($"@{prop.Name}, ");
                        command.Parameters.AddWithValue($"@{prop.Name}", propValue ?? DBNull.Value);
                    }
                    else if (typeof(IEnumerable<IEntity<G>>).IsAssignableFrom(prop.PropertyType))
                    {
                        var nestedItems = (IEnumerable<IEntity<G>>)propValue;
                        if (nestedItems != null)
                        {
                            foreach (var nestedItem in nestedItems)
                            {
                                InvokeNestedOrmMethod("Insert", nestedItem);
                            }
                        }
                    }
                    else if (typeof(IEntity<G>).IsAssignableFrom(prop.PropertyType))
                    {
                        var nestedItem = (IEntity<G>)propValue;
                        if (nestedItem != null)
                        {
                            InvokeNestedOrmMethod("Insert", nestedItem);
                        }
                    }
                }

                if (columnNames.Length > 0) columnNames.Length -= 2;
                if (values.Length > 0) values.Length -= 2;

                command.CommandText = $"INSERT INTO {typeof(T).Name} ({columnNames}) VALUES ({values})";
                command.ExecuteNonQuery();
            }
        }


        public List<T> GetAll()
        {
            var list = new List<T>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {typeof(T).Name}";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapReaderToEntity(reader));
                    }
                }
            }
            return list;
        }


        public T GetById(G id)
        {
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
                        return MapReaderToEntity(reader);
                    }
                }
            }
            return null;
        }



        public void Update(T item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                var setClauses = new StringBuilder();
                int parameterIndex = 0;

                foreach (var prop in typeof(T).GetProperties())
                {
                    var propValue = prop.GetValue(item);

                    if (prop.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string) || prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(double))
                    {
                        string paramName = $"@param{parameterIndex}";
                        setClauses.Append($"{prop.Name} = {paramName}, ");
                        command.Parameters.AddWithValue(paramName, propValue ?? DBNull.Value);
                        parameterIndex++;
                    }
                    else if (typeof(IEnumerable<IEntity<G>>).IsAssignableFrom(prop.PropertyType))
                    {
                        var nestedItems = (IEnumerable<IEntity<G>>)propValue;
                        if (nestedItems != null)
                        {
                            foreach (var nestedItem in nestedItems)
                            {
                                InvokeNestedOrmMethod("Update", nestedItem);
                            }
                        }
                    }
                    else if (typeof(IEntity<G>).IsAssignableFrom(prop.PropertyType))
                    {
                        var nestedItem = (IEntity<G>)propValue;
                        if (nestedItem != null)
                        {
                            InvokeNestedOrmMethod("Update", nestedItem);
                        }
                    }
                }

                if (setClauses.Length > 0) setClauses.Length -= 2;

                string idParamName = $"@param{parameterIndex}";
                command.CommandText = $"UPDATE {typeof(T).Name} SET {setClauses} WHERE Id = {idParamName}";
                command.Parameters.AddWithValue(idParamName, item.Id);

                Console.WriteLine($"SQL Command: {command.CommandText}");
                foreach (SqlParameter parameter in command.Parameters)
                {
                    Console.WriteLine($"Parameter: {parameter.ParameterName}, Value: {parameter.Value}");
                }

                command.ExecuteNonQuery();
            }
        }


        // I'm try but i did not complete the delete mehtod so i pass this..
        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(G id)
        {
            throw new NotImplementedException ();
        }


        private T MapReaderToEntity(IDataRecord record)
        {
            var item = new T();

            foreach (var prop in typeof(T).GetProperties())
            {
                Type propType = prop.PropertyType;

                var underlyingType = Nullable.GetUnderlyingType(propType) ?? propType;

                if (underlyingType.IsPrimitive || underlyingType == typeof(string) || underlyingType == typeof(Guid) || underlyingType == typeof(double) || underlyingType == typeof(DateTime) || underlyingType.IsEnum)
                {
                    if (record[prop.Name] != DBNull.Value)
                    {
                        object value = Convert.ChangeType(record[prop.Name], underlyingType);
                        prop.SetValue(item, value);
                    }
                }
                else if (typeof(IEntity<G>).IsAssignableFrom(propType))
                {
                    var foreignKeyName = $"{prop.Name}Id";

                    if (ColumnExists(record, foreignKeyName) && record[foreignKeyName] != DBNull.Value)
                    {
                        var nestedId = (G)Convert.ChangeType(record[foreignKeyName], typeof(G));
                        var nestedOrmType = typeof(MyORM<,>).MakeGenericType(typeof(G), propType);
                        var nestedOrm = Activator.CreateInstance(nestedOrmType, _connectionString);
                        var getByIdMethod = nestedOrmType.GetMethod("GetById");
                        var nestedEntity = getByIdMethod.Invoke(nestedOrm, new object[] { nestedId });

                        prop.SetValue(item, nestedEntity);
                    }
                }
                else if (typeof(IEnumerable<IEntity<G>>).IsAssignableFrom(propType))
                {
                    var elementType = propType.GetGenericArguments()[0];
                    var nestedOrmType = typeof(MyORM<,>).MakeGenericType(typeof(G), elementType);
                    var nestedOrm = Activator.CreateInstance(nestedOrmType, _connectionString);
                    var getAllMethod = nestedOrmType.GetMethod("GetAll");
                    var nestedEntities = getAllMethod.Invoke(nestedOrm, null);

                    prop.SetValue(item, nestedEntities);
                }
                else if (propType.IsClass && propType != typeof(string))
                {
                    var complexTypeInstance = Activator.CreateInstance(propType);
                    prop.SetValue(item, complexTypeInstance);
                }
            }

            return item;
        }


        private bool ColumnExists(IDataRecord record, string columnName)
        {
            try
            {
                return record.GetOrdinal(columnName) >= 0;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }


        private void InvokeNestedOrmMethod(string methodName, IEntity<G> nestedItem)
        {
            try
            {
                var nestedOrmType = typeof(MyORM<,>).MakeGenericType(typeof(G), nestedItem.GetType());
                var nestedOrm = Activator.CreateInstance(nestedOrmType, _connectionString);
                nestedOrmType.GetMethod(methodName).Invoke(nestedOrm, new object[] { nestedItem });
            }
            catch (TargetInvocationException ex)
            {
                Console.WriteLine($"Exception in {methodName} for {nestedItem.GetType().Name}: {ex.InnerException?.Message}");
                Console.WriteLine($"Stack Trace: {ex.InnerException?.StackTrace}");
            }
        }
         

        IEnumerable<T> IMyORM<G, T>.GetAll()
        {
            return GetAll();
        }
    }
}