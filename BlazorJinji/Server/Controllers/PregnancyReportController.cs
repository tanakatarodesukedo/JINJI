using BlazorJinji.Server.Service;
using BlazorJinji.Shared;
using BlazorJinji.Shared.Condition;
using BlazorJinji.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorJinji.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PregnancyReportController : ControllerBase
    {
        private readonly PregnancyReportService pregnancyReportService;

        // コンストラクタインジェクション
        public PregnancyReportController(PregnancyReportService pregnancyReportService)
        {
            this.pregnancyReportService = pregnancyReportService;
        }

        [HttpPost]
        public async Task<IList<PregnancyReportModel>> GetListAsync([FromBody] PregnancyReportCondition condition)
        {
            // 依存関係を直接使用
            return await pregnancyReportService.GetListAsync(condition);
        }

        [HttpPost("Insert")]
        public async Task InsertAsync([FromBody] PregnancyReportModel model)
        {
            // 依存関係を直接使用
            await pregnancyReportService.InsertAsync(model);
        }
    }
}
