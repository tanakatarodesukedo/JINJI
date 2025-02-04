﻿using BlazorJinji.Server.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace BlazorJinji.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices((context, services) =>
                {
                    // appsettings.json から接続文字列を読み込み
                    var configuration = context.Configuration;
                    var connectionString = configuration.GetConnectionString("PostgreSqlConnection");

                    // NpgsqlConnection を依存性注入コンテナに登録（リクエストごとに新しい接続が作成され、リクエスト終了時に接続が自動的にクローズされる）
                    services.AddScoped(sp =>
                    {
                        var connection = new NpgsqlConnection(connectionString);
                        connection.Open();
                        return connection;
                    });

                    // PregnancyReportService の登録
                    services.AddTransient<PregnancyReportService>();
                });
    }
}