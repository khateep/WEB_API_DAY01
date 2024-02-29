namespace BaniSeuifD01.DTOs
{
    public class DeptWithStdntName
    {
        public int Department_Number { get; set; }
        public string Department_Name { get; set; }
        public string Department_Manger { get; set; }
        public string Department_Location { get; set; }

        public List<string> Student_Name { get; set; } = new List<string>();
    }
}
