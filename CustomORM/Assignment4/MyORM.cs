using Assignment4.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

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

        public void Update(T item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                var setClauses = new StringBuilder();

                foreach (var prop in typeof(T).GetProperties())
                {
                    var propValue = prop.GetValue(item);
                    if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string) || prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(double))
                    {
                        setClauses.Append($"{prop.Name} = @{prop.Name}, ");
                        command.Parameters.AddWithValue($"@{prop.Name}", propValue ?? DBNull.Value);
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

                command.CommandText = $"UPDATE {typeof(T).Name} SET {setClauses} WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", item.Id);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(T item)
        {
            Delete(item.Id);
        }

        public void Delete(G id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM {typeof(T).Name} WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
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

        private T MapReaderToEntity(IDataRecord record)
        {
            var item = new T();
            foreach (var prop in typeof(T).GetProperties())
            {
                if (record[prop.Name] != DBNull.Value)
                {
                    prop.SetValue(item, record[prop.Name]);
                }
            }
            return item;
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
