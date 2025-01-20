using BlazorJinji.Shared.Condition;
using BlazorJinji.Shared.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorJinji.Server.Service
{
    public class PregnancyReportService
    {
        private readonly NpgsqlConnection connection;

        // コンストラクタインジェクションで接続文字列を受け取る（複数個所で利用する場合、基底クラスにまとめる）
        public PregnancyReportService(NpgsqlConnection connection)
        {
            this.connection = connection;

            // アンダースコア区切りをパスカルケースに変換してマッピングしてくれる
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<IList<PregnancyReportModel>> GetListAsync(PregnancyReportCondition condition)
        {
            var sql = new StringBuilder();
            sql.AppendLine(" SELECT staff_no, appli_date, multiple_flg FROM tb_pregnancy_report WHERE 1 = 1 ");

            if (!string.IsNullOrEmpty(condition.AppliDate))
            {
                sql.AppendLine(" AND appli_date = @AppliDate ");
            }
            if (condition.MultipleFlg)
            {
                sql.AppendLine(" AND multiple_flg = '1' ");
            }

            sql.AppendLine(" ORDER BY appli_date DESC, staff_no ");
            sql.AppendLine(" FETCH FIRST 1000 ROWS ONLY ");

            var result = await connection.QueryAsync<PregnancyReportModel>(sql.ToString(), condition);

            return result.AsList();
        }

        public async Task<int> InsertAsync(PregnancyReportModel model)
        {
            // トランザクションの開始
            using var transaction = await connection.BeginTransactionAsync();
            try
            {
                // INSERTクエリの実行
                string sql = "INSERT INTO tb_pregnancy_report (staff_no, appli_date, multiple_flg) VALUES (@StaffNo, @AppliDate, @MultipleFlg)";

                // Dapperを使ってデータを挿入
                int count = await connection.ExecuteAsync(sql, model, transaction);

                // コミット
                await transaction.CommitAsync();

                return count;
            }
            catch (Exception ex)
            {
                // エラー発生時はロールバック
                await transaction.RollbackAsync();
                // エラーハンドリングを追加
                throw new Exception("データ挿入に失敗しました", ex);
            }
        }

        public async Task<string> BulkUpdateAsync(PregnancyReportModel model)
        {
            // トランザクションの開始
            using var transaction = await connection.BeginTransactionAsync();
            try
            {
                // ストアドプロシージャ
                using var cmd = new NpgsqlCommand("refresh_pregnancy_report", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // INパラメータの設定
                cmd.Parameters.AddWithValue("loop_count", 100000);

                // OUTパラメータの設定
                var outParam = new NpgsqlParameter("result_cd", NpgsqlTypes.NpgsqlDbType.Char)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                cmd.Parameters.Add(outParam);

                // ストアドプロシージャを実行
                cmd.ExecuteNonQuery();

                // コミット
                await transaction.CommitAsync();

                return $"{outParam.Value}";
            }
            catch (Exception ex)
            {
                // エラー発生時はロールバック
                await transaction.RollbackAsync();
                // エラーハンドリングを追加
                throw new Exception("データ更新に失敗しました", ex);
            }
        }
    }
}