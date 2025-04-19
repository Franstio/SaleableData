using Blazorise;
using Dapper;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1;
using SaleableData.Models;

namespace SaleableData.Data
{
    public class Step2DataService
    {
        private IConfiguration config;
        public string Database { private get; set; } = string.Empty;

        public Step2DataService(IConfiguration config)
        {
            this.config = config;
        }
        private MySqlConnection GetConnection()
        {
            string connection = $"{config.GetConnectionString("DevDB")!};database={Database}";
            
            var con = new MySqlConnection(connection);
            con.Open();
            return con;

        }

        public async Task<List<TransactionModel>> GetTransaction(string fromContainer,DateTime from,DateTime to)
        {
            using (var con = GetConnection())
            {
                string f = from.ToString("yyyy-MM-dd"), t = to.ToString("yyyy-MM-dd");
                string query = "Select t.Id,FromContainer,ToBin,RecordDate,Status,Weight,w.Scales,Type,ROW_NUMBER() OVER (PARTITION BY cast(recorddate as date) ORDER BY weight desc) AS Group_Id From Transaction t inner join waste w on t.idWaste=w.id Where fromContainer=@fromContainer and RecordDate between @from and @to and type='Dispose';";
                var result = await con.QueryAsync<TransactionModel>(query,new { fromContainer,from=f,to=t});
                return result.ToList();
            }
        }
        public Dictionary<string, string> GetStation()
        {
            var s =config.GetSection("Saleable:Station").GetChildren();
            Dictionary<string, string> Stations = new Dictionary<string, string>();
            foreach (var i in s)
            {
                Stations.Add(i.GetChildren().First().Key, i.GetChildren().First().Value!);
            }
            return Stations;
        }
        public async Task<List<ContainerModel>> GetContainers()
        {

            using (var con = GetConnection())
            {
                string query = "Select Name from Container where type='Dispose';";
                var result = await con.QueryAsync<ContainerModel>(query);
                return result.ToList();
            }
        }
    }
}
