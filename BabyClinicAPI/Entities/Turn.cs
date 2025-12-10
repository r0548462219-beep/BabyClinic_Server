namespace BabyClinicAPI.Entities
{
    public class Turn
    {
        public Turn()
        {
        }

        public Turn(int id, int babyId, int nurseId, DateTime dateTime, string status)
        {
            Id = id;
            BabyId = babyId;
            NurseId = nurseId;
            DateTime = dateTime;
            Status = status;
        }


        // מזהה תור
        public int Id { get; set; }
        // מזהה תינוק (מפתח זר)
        public int BabyId { get; set; }
        // מזהה אחות (מפתח זר)
        public int NurseId { get; set; }
        // תאריך ושעה
        public DateTime DateTime { get; set; }
        // סטטוס (נקבע / בוצע / בוטל)
        public string Status { get; set; }
    }
}
