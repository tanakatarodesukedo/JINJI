// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BlazorJinji.Client.Pages.PregnancyReport
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\_Imports.razor"
using System.Net.Http

#nullable disable
    ;
#nullable restore
#line 2 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\_Imports.razor"
using System.Net.Http.Json

#nullable disable
    ;
#nullable restore
#line 3 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms

#nullable disable
    ;
#nullable restore
#line 4 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing

#nullable disable
    ;
#nullable restore
#line 5 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web

#nullable disable
    ;
#nullable restore
#line 6 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http

#nullable disable
    ;
#nullable restore
#line 7 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\_Imports.razor"
using Microsoft.JSInterop

#nullable disable
    ;
#nullable restore
#line 8 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\_Imports.razor"
using BlazorJinji.Client

#nullable disable
    ;
#nullable restore
#line 9 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\_Imports.razor"
using BlazorJinji.Client.Shared

#nullable disable
    ;
#nullable restore
#line 2 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\Pages\PregnancyReport\List.razor"
 using BlazorJinji.Shared

#nullable disable
    ;
#nullable restore
#line 3 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\Pages\PregnancyReport\List.razor"
 using BlazorJinji.Shared.Condition

#nullable disable
    ;
#nullable restore
#line 4 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\Pages\PregnancyReport\List.razor"
 using BlazorJinji.Shared.Model

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Components.RouteAttribute(
    // language=Route,Component
#nullable restore
#line 1 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\Pages\PregnancyReport\List.razor"
      "/pregnancyReport/list"

#line default
#line hidden
#nullable disable
    )]
    #nullable restore
    public partial class List : global::Microsoft.AspNetCore.Components.ComponentBase
    #nullable disable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 57 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\Pages\PregnancyReport\List.razor"
       
    private PregnancyReportCondition condition = new PregnancyReportCondition();

    private PregnancyReportModel[] pregnancyReports;

    protected override async Task OnInitializedAsync()
    {
        await SearchAsync();
    }

    private async Task SearchAsync()
    {
        if (condition.SelectedDate.HasValue)
        {
            condition.AppliDate = condition.SelectedDate.Value.ToString("yyyyMMdd");
        }
        else
        {
            condition.AppliDate = string.Empty;
        }

        var response = await Http.PostAsJsonAsync<PregnancyReportCondition>("PregnancyReport", condition);

        // レスポンスから JSON をデシリアライズしてリストを取得
        pregnancyReports = await response.Content.ReadFromJsonAsync<PregnancyReportModel[]>();
    }

    private void MoveDetailPage()
    {
        string staffNo = $"{new Random().Next(0, 999999):D6}";

        // 登録画面へ遷移
        Navi.NavigateTo($"pregnancyReport/detail/{staffNo}");
    }

#line default
#line hidden
#nullable disable

        [global::Microsoft.AspNetCore.Components.InjectAttribute] private 
#nullable restore
#line 6 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\Pages\PregnancyReport\List.razor"
        NavigationManager

#line default
#line hidden
#nullable disable
         
#nullable restore
#line 6 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\Pages\PregnancyReport\List.razor"
                          Navi

#line default
#line hidden
#nullable disable
         { get; set; }
         = default!;
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private 
#nullable restore
#line 5 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\Pages\PregnancyReport\List.razor"
        HttpClient

#line default
#line hidden
#nullable disable
         
#nullable restore
#line 5 "C:\Users\cic44\source\repos\BlazorJinji\BlazorJinji\Client\Pages\PregnancyReport\List.razor"
                   Http

#line default
#line hidden
#nullable disable
         { get; set; }
         = default!;
    }
}
#pragma warning restore 1591
