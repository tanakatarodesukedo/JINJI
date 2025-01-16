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

namespace BlazorJinji.Server.Service
{
    public class PregnancyReportService
    {
        private readonly NpgsqlConnection _connection;

        // コンストラクタインジェクションで接続文字列を受け取る（複数個所で利用する場合、基底クラスにまとめる）
        public PregnancyReportService(NpgsqlConnection connection)
        {
            _connection = connection;

            // アンダースコア区切りをパスカルケースに変換してマッピングしてくれる
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<IList<PregnancyReportModel>> GetListAsync(PregnancyReportCondition condition)
        {
            await _connection.OpenAsync();

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

            var result = await _connection.QueryAsync<PregnancyReportModel>(sql.ToString(), condition);

            return result.AsList();
        }
    }
}
