namespace BPI_BillOfLading.Models.HolidayModel
{
    public class SetHolidayModel
    {
        public int Year { get; set; }
        public string Company { get; set; }
        public List<TableDataModel> TableData { get; set; }
        public string Username { get; set; }
    }
}
