namespace RoomRental.Models;

public enum RoomStatus { Available, UnderMaintenance, Reserved }
public enum RoomType { Regular, Lecture, VIP }
public enum BuildingStatus { Open, Closed, UnderConstruction }
public enum UserRole { RegularUser, Moderator, Administrator }
public enum ReservationStatus { Pending, Confirmed, Cancelled }
public enum EquipmentStatus { Working, UnderRepair, RequiresReplacement }
public enum MenuCategory { MainCourse, Appetizer, Dessert, Beverage }
public enum DocumentStatus { Issued, Paid, Overdue }
public enum ReportType { RoomUtilization, DepartmentCosts, ReservationStatistics }
