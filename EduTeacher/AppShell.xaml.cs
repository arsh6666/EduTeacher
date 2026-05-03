using EduTeacher.Views.Phone;

namespace EduTeacher;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Detail routes (push navigation)
        Routing.RegisterRoute("CreateAssessment", typeof(CreateAssessmentPhonePage));
        Routing.RegisterRoute("GradeSubmissions", typeof(GradeSubmissionsPhonePage));
        Routing.RegisterRoute("PayslipList", typeof(PayslipListPhonePage));
        Routing.RegisterRoute("PayslipDetail", typeof(PayslipDetailPhonePage));
        Routing.RegisterRoute("TaxDeclaration", typeof(TaxDeclarationPhonePage));
        Routing.RegisterRoute("Reimbursements", typeof(ReimbursementsPhonePage));
        Routing.RegisterRoute("SalaryAdvance", typeof(SalaryAdvancePhonePage));
        Routing.RegisterRoute("Overtime", typeof(OvertimePhonePage));
        Routing.RegisterRoute("Conversation", typeof(ConversationPhonePage));

        // Announcements & Notifications
        Routing.RegisterRoute("Announcements", typeof(AnnouncementsManagePhonePage));
        Routing.RegisterRoute("CreateAnnouncement", typeof(CreateAnnouncementPhonePage));
        Routing.RegisterRoute("NotificationPreferences", typeof(NotificationPreferencesPhonePage));
    }
}
