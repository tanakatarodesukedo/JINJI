using Npgsql;
using System.Threading.Tasks;
using System;
using Dapper;
using BlazorJinji.Shared.Model;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using BlazorJinji.Shared.Condition;
using System.Text;
using System.Reflection;

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
    }
}
