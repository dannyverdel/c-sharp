﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DependencyInjectionDemo.Logic;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddTransient<IDemoLogic, DemoLogic>();     // instantiates new object every time
//builder.Services.AddScoped<IDemoLogic, DemoLogic>();      // singleton per person
//builder.Services.AddSingleton<IDemoLogic, DemoLogic>();   // generic singleton for everyone

builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

