using MongoDB.Bson.Serialization.Attributes;

namespace TaskPro.Data
{
    public class Comentarios
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("contenido")]
        public string Contenido { get; set; }

        [BsonElement("usuario_id")]
        public int UsuarioId { get; set; }

        [BsonElement("tarea_id")]
        public string TareaId { get; set; }

        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
