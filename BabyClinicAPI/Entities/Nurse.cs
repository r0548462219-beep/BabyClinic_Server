namespace BabyClinicAPI.Entities
{
    public class Nurse
    {
        public Nurse()
        {
        }

        public Nurse(int id, string name, string specialty, string phone, string status)
        {
            Id = id;
            Name = name;
            Specialty = specialty;
            Phone = phone;
            Status = status;
        }

        public int Id { get; set; }
        // שם
        public string Name { get; set; }
        // התמחות / סוג שירות
        public string Specialty { get; set; }
        // טלפון
        public string Phone { get; set; }
        // סטטוס (פעילה / בחופשה / מושבתת)
        public string Status { get; set; }
    }
}
