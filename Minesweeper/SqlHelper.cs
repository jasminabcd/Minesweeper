using Microsoft.Data.SqlClient;
using Minesweeper;

namespace Personen
{
    public class SqlHelper
    {
        private string _connectionString;
        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<object[]> ExecuteReader(string sql, List<SqlParameter> parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var sqlCommand = new SqlCommand(sql, connection))
                {
                    foreach (var parameter in parameters)
                    {
                        sqlCommand.Parameters.Add(parameter);
                    }
                    var resultList = new List<object[]>();

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var values = new object[reader.FieldCount];
                            reader.GetValues(values);
                            resultList.Add(values);
                        }

                        return resultList;
                    }
                }
            }
        }

        public class Score
        {
            public Score(int id, int duration, string playerName, DateTime playDate, string difficulty)
            {

                ID = id;
                Duration = duration;
                PlayerName = playerName;
                PlayDate = playDate;
                Difficulty = difficulty;

            }

            public int Duration { get; set; }
            public string PlayerName { get; set; }
            public DateTime PlayDate { get; set; }
            public string Difficulty { get; set; }
            public int ID { get; set; }

            public override string ToString()
            {
                return $"{Duration} {PlayerName} {PlayDate}";
            }

        }

        
        public List<Score> GetHighscores(string difficulty)
        {
            var sql = "SELECT Id, Duration, PlayerName, PlayDate, Difficulty FROM Highscore  WHERE Difficulty = @p1 ORDER BY Duration ASC";

            var list = new List<SqlParameter>
            {
                new SqlParameter()
                {
                    ParameterName = "@p1",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = difficulty
                }
            };

            var result = ExecuteReader(sql, list);

            //Console.WriteLine("Id, Duration, Player_Id, PlayDate, Difficulty");

            var HighscoreList = new List<Score>();

            foreach (var listrow in result)
            {
                var id = (int)listrow[0];
                var duration = (int)listrow[1];
                var playerName = (string)listrow[2];
                var playDate = (DateTime)listrow[3];
                var dbDifficulty = (string)listrow[4];

                var newScore = new Score(id, duration, playerName, playDate, dbDifficulty);

                HighscoreList.Add(newScore);
            }

            return HighscoreList;
        }

        public void AddHighscore(int duration, string playerName, DateTime playDate, string difficulty)
        {
            var sql = "INSERT INTO dbo.Highscore(Duration, PlayerName, PlayDate, Difficulty) VALUES(@p1, @p2 , @p3 , @p4)";

            var parameterList = new List<SqlParameter>()
            {
                new SqlParameter
                {
                    ParameterName = "@p1",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = duration
                },
                new SqlParameter
                {
                    ParameterName = "@p2",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = playerName
                },
                new SqlParameter
                {
                    ParameterName = "@p3",
                    SqlDbType = System.Data.SqlDbType.DateTime2,
                    Value = playDate
                },
                new SqlParameter
                {
                    ParameterName = "@p4",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = difficulty
                }
            };

            ExecuteReader(sql, parameterList);
        }
    }
}