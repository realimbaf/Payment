using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using CarParts.Data.Componets;
using Wiki.Payment.Common.POCO.DTO;

namespace Wiki.Payment.Repository
{
    public class ExportRepository : IExportRepository
    {
        private readonly string _connectionString;
        public ExportRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IWikiDbCommand GetCommand(string sql)
        {
            var cmd = new LafSqlCommand(sql, new SqlConnection(_connectionString))
            {
                CommandType = CommandType.StoredProcedure,
                UseTransaction = true,
                IsolationLevel = IsolationLevel.ReadCommitted
            };
            return cmd;
        }
        public void SendIdsToErp(DTOErp ids)
        {
            using (var cmd = GetCommand("Payments_to_erp"))
            {
                cmd.AddParameter("Ids", ConvertIdsLongToDataTable(ids.Ids));
                cmd.Execute();
            }
        }
        internal static DataTable ConvertIdsLongToDataTable(ICollection<long> ids)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("id", typeof(long)));
            foreach (var id in ids)
            {
                var row = dt.NewRow();
                row["id"] = id;
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
