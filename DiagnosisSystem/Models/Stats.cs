using System.Runtime.Serialization;

namespace DiagnosisSystem.Models
{
    [DataContract]
    public class Stats
    {
        [DataMember]
        public int numOfIRequests { get; set; }

        [DataMember]
        public int numOfDoctors { get; set; }

        [DataMember]
        public int numOfPatients { get; set; }

        [DataMember]
        public int numOfAdmins { get; set; }
    }
}
