using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using EduTeacher.Web;
using EduTeacher.Web.Services;
using EduTeacher.Shared.ViewModels;
using EduTeacher.Shared.Education.Services;
using EduTeacher.Shared.Payroll.Services;
using EduTeacher.Shared.Chat.Services;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Security.Interfaces;
using Rootfly.Mobile.Core.Networking.REST;
using Rootfly.Mobile.Core.Networking.REST.ApplicationConfiguration;
using Rootfly.Mobile.Core.Networking.SignalR;
using IDialogService = Rootfly.Mobile.Core.Common.Abstractions.IDialogService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// MudBlazor
builder.Services.AddMudServices();

// HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Core abstractions
builder.Services.AddSingleton<INavigationService, BlazorNavigationService>();
builder.Services.AddSingleton<IDialogService, BlazorDialogService>();
builder.Services.AddSingleton<IDeviceInfoService, BlazorDeviceInfoService>();
builder.Services.AddSingleton<IAuthService, BlazorAuthService>();
builder.Services.AddSingleton<ILocalizationService, BlazorLocalizationService>();
builder.Services.AddSingleton<IAbpApplicationConfigurationService, BlazorAppConfigurationService>();
builder.Services.AddSingleton<IHubConnectionManager, BlazorHubConnectionManager>();
builder.Services.AddScoped<IApiClient, BlazorApiClient>();

// Shared services
builder.Services.AddScoped<ITeacherEducationApiService, TeacherEducationApiService>();
builder.Services.AddScoped<IPayrollApiService, PayrollApiService>();
builder.Services.AddScoped<IChatApiService, ChatApiService>();

// ViewModels
builder.Services.AddTransient<LoginViewModel>();
builder.Services.AddTransient<DashboardViewModel>();
builder.Services.AddTransient<ScheduleViewModel>();
builder.Services.AddTransient<MarkAttendanceViewModel>();
builder.Services.AddTransient<AttendanceHistoryViewModel>();
builder.Services.AddTransient<AssessmentListViewModel>();
builder.Services.AddTransient<CreateAssessmentViewModel>();
builder.Services.AddTransient<GradeBookViewModel>();
builder.Services.AddTransient<GradeSubmissionsViewModel>();
builder.Services.AddTransient<PayrollDashboardViewModel>();
builder.Services.AddTransient<PayslipListViewModel>();
builder.Services.AddTransient<PayslipDetailViewModel>();
builder.Services.AddTransient<TaxDeclarationViewModel>();
builder.Services.AddTransient<ReimbursementsViewModel>();
builder.Services.AddTransient<AnnouncementManageViewModel>();
builder.Services.AddTransient<CreateAnnouncementViewModel>();
builder.Services.AddTransient<NotificationPreferencesViewModel>();
builder.Services.AddTransient<ChatListViewModel>();
builder.Services.AddTransient<ConversationViewModel>();

await builder.Build().RunAsync();
