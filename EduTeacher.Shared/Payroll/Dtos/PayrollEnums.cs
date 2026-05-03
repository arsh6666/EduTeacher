namespace EduTeacher.Shared.Payroll.Dtos;

// ─── Payroll Enums (matching backend Rootfly.Cloud.Payroll.Enums) ───

public enum PayrollFrequency { Monthly = 0, BiWeekly = 1, Weekly = 2, SemiMonthly = 3, Quarterly = 4, Annual = 5 }
public enum SalarySlipStatus { Draft = 0, Computed = 1, OnHold = 2, Approved = 3, Paid = 4, Cancelled = 5 }
public enum SalaryComponentType { Earning = 0, Deduction = 1, EmployerContribution = 2 }
public enum ComponentCalculationType { Fixed = 0, Percentage = 1, Formula = 2 }
public enum PaymentMethodType { BankTransfer = 0, Check = 1, Cash = 2, DirectDeposit = 3, Wire = 4 }
public enum AdvanceStatus { Requested = 0, Approved = 1, Rejected = 2, Disbursed = 3, Repaying = 4, FullyRepaid = 5, WrittenOff = 6 }
public enum AdvanceRepaymentStatus { Pending = 0, Deducted = 1, Skipped = 2, Adjusted = 3 }
public enum ReimbursementStatus { Draft = 0, Submitted = 1, Approved = 2, Rejected = 3, Paid = 4 }
public enum OvertimeEntryStatus { Pending = 0, Approved = 1, Rejected = 2, Processed = 3 }
public enum OvertimeDayType { Regular = 0, Weekend = 1, Holiday = 2, SpecialHoliday = 3 }
public enum TaxDeclarationStatus { Draft = 0, Submitted = 1, Verified = 2, Locked = 3 }
public enum TaxRegimeType { Old = 0, New = 1, Default = 2 }
public enum FBPDeclarationStatus { Draft = 0, Submitted = 1, Approved = 2, Rejected = 3, Locked = 4 }
public enum RoundingMethod { None = 0, RoundUp = 1, RoundDown = 2, RoundNearest = 3 }
