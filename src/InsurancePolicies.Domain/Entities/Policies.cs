using InsurancePolicies.Domain.Entities.Document;
using MongoDB.Bson.Serialization.Attributes;

namespace src.InsurancePolicies.Domain.Entities
{
    [BsonCollection("Policies"), BsonIgnoreExtraElements]
    public class Policies : Document
    {
        [BsonElement("policyNumber")]
        public string? PolicyNumber { get; set; }

        [BsonElement("clientName")]
        public string? ClientName { get; set; }

        [BsonElement("clientId")]
        public string? ClientId { get; set; }

        [BsonElement("clientBirthDate")]
        public DateTime ClientBirthDate { get; set; }

        [BsonElement("policyTakenDate")]
        public DateTime PolicyTakenDate { get; set; }

        [BsonElement("policyCoverage")]
        public string? PolicyCoverage { get; set; }

        [BsonElement("maximumCoverageValue")]
        public decimal MaximumCoverageValue { get; set; }

        [BsonElement("policyPlanName")]
        public string? PolicyPlanName { get; set; }

        [BsonElement("clientResidenceCity")]
        public string? ClientResidenceCity { get; set; }

        [BsonElement("clientResidenceAddress")]
        public string? ClientResidenceAddress { get; set; }

        [BsonElement("vehiclePlateNumber")]
        private string _vehiclePlateNumber;
        public string VehiclePlateNumber
        {
            get { return _vehiclePlateNumber; }
            set { _vehiclePlateNumber = value.ToUpper(); }
        }

        [BsonElement("vehicleModel")]
        public string? VehicleModel { get; set; }

        [BsonElement("hasVehicleInspection")]
        public bool? HasVehicleInspection { get; set; }

        [BsonElement("policyStartDate")]
        public DateTime PolicyStartDate { get; set; }

        [BsonElement("policyEndDate")]
        public DateTime PolicyEndDate { get; set; }
    }
}