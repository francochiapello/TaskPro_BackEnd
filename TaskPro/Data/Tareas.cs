using MongoDB.Bson.Serialization.Attributes;

namespace TaskPro.Data
{
    public class Tareas
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("nombre")]
        public string Nombre { get; set; }
        [BsonElement("descripcion")]
        public string Descripcion { get; set; }
        [BsonElement("proyecto_id")]
        public int ProyectoId { get; set; }
        [BsonElement("asignado_a")]
        public int AsignadoA { get; set; }
        [BsonElement("estado")]
        public string Estado { get; set; }
        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }
        [BsonElement("updated_at")]

        public DateTime? UpdatedAt { get; set; }
    }
}
