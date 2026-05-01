using Rootfly.Mobile.Core;
using EduTeacher.Shared;
using Volo.Abp.Modularity;

namespace EduTeacher;

[DependsOn(
    typeof(RootflyCoreModule),
    typeof(EduTeacherSharedModule)
)]
public class EduTeacherModule : AbpModule
{
}
